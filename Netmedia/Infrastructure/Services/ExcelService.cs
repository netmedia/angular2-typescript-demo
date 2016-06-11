using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Netmedia.Common.Extensions;

namespace Netmedia.Infrastructure.Services
{
    public class ExcelService : IExcelService
    {
        private CultureInfo _cultureForConversions { get; set; }

        public void SetCulture(CultureInfo culture)
        {
            _cultureForConversions = culture;
        }

        public Worksheet FindWorksheet(SpreadsheetDocument document, string sheetName)
        {
            var sheet = document.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>().FirstOrDefault(item => item.Name == sheetName);
            if (sheet == null) return null;

            var relationshipId = sheet.Id.Value;
            var worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(relationshipId);

            return worksheetPart.Worksheet;
        }
        public void FillCell(Worksheet worksheet, string cellReference, object value, CellValues type, string forcedFormat = "")
        {
            var strValue = "";
            if (value != null) strValue = value.ToString();

            if (value is decimal) strValue = ToExcelString((decimal)value);
            if (value is DateTime)
            {
                if ((DateTime) value == DateTime.MinValue)
                    strValue = "";
                else
                {
                    var format = forcedFormat.IsNullOrEmpty() ? _cultureForConversions.DateTimeFormat.ShortDatePattern : forcedFormat;

                    strValue = ((DateTime)value).ToString(format);
                }
            }

            var cell = FindCell(worksheet, cellReference);
            if (cell == null) throw new InvalidOperationException(string.Format("Cell '{0}' does not exist.", cellReference));

            cell.CellValue = new CellValue(strValue);
            cell.DataType = new EnumValue<CellValues>(type);
        }
        public Row FindRow(Worksheet worksheet, uint rowIndex)
        {
            return worksheet.GetFirstChild<SheetData>().Elements<Row>().FirstOrDefault(item => item.RowIndex == rowIndex);
        }
        public Cell FindCell(Worksheet worksheet, string cellReference)
        {
            var row = FindRow(worksheet, GetRowIndex(cellReference));

            return row != null
                ? row.Elements<Cell>().FirstOrDefault(item => String.Compare(item.CellReference.Value, cellReference, StringComparison.OrdinalIgnoreCase) == 0)
                : null;
        }

        public void CopyRowTo(Worksheet worksheet, uint sourceRowIndex, uint destinationRowIndex)
        {
            var sourceRow = FindRow(worksheet, sourceRowIndex);
            var oldDestinationRow = FindRow(worksheet, destinationRowIndex);
            var newRow = sourceRow.CloneNode(true) as Row;

            foreach (var row in worksheet.Descendants<Row>().Where(item => item.RowIndex >= destinationRowIndex))
            {
                row.RowIndex = row.RowIndex + 1;
            }
            newRow.RowIndex = destinationRowIndex;
            oldDestinationRow.InsertBeforeSelf<Row>(newRow);
            worksheet.Save();

            foreach (var mergeCells in worksheet.Elements<MergeCells>())
            {
                var newMergeCell = new Dictionary<string, MergeCell>();

                foreach (var mergeCell in mergeCells.Elements<MergeCell>())
                {
                    var mergeCellParts = mergeCell.Reference.Value.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    if (mergeCellParts.Length != 2) continue;

                    var mergeFromRowIndex = GetRowIndex(mergeCellParts[0]);
                    var mergeToRowIndex = GetRowIndex(mergeCellParts[1]);
                    var newFromRowIndex = mergeFromRowIndex < destinationRowIndex ? mergeFromRowIndex : mergeFromRowIndex + 1;
                    var newToRowIndex = mergeToRowIndex < destinationRowIndex ? mergeToRowIndex : mergeToRowIndex + 1;

                    if (mergeFromRowIndex != newFromRowIndex || mergeToRowIndex != newToRowIndex)
                    {
                        mergeCell.Reference = string.Format("{0}:{1}",
                            mergeCellParts[0].Replace(mergeFromRowIndex.ToString(), newFromRowIndex.ToString()),
                            mergeCellParts[1].Replace(mergeToRowIndex.ToString(), newToRowIndex.ToString()));
                    }
                    else if (mergeFromRowIndex == mergeToRowIndex && mergeFromRowIndex == sourceRow.RowIndex)
                    {
                        newMergeCell.Add(mergeCell.Reference.Value.Replace(sourceRow.RowIndex.ToString(), destinationRowIndex.ToString()), mergeCell);
                    }
                }

                foreach (var reference in newMergeCell)
                {
                    var newMerge = reference.Value.Clone() as MergeCell;
                    newMerge.Reference = reference.Key;

                    mergeCells.Append(newMerge);
                }
            }
            worksheet.Save();

            foreach (var row in worksheet.Descendants<Row>().Where(item => item.RowIndex >= destinationRowIndex))
            {
                foreach (var cell in row.Elements<Cell>())
                {
                    cell.CellReference = string.Format("{0}{1}", GetColumName(cell.CellReference), row.RowIndex);
                    //cell.CellReference.Value.Replace((row.RowIndex - (destinationRowIndex - sourceRowIndex)).ToString(), row.RowIndex.ToString());
                }
            }
            worksheet.Save();
        }
        public void CopyRowAfter(Worksheet worksheet, uint sourceRowIndex)
        {
            CopyRowTo(worksheet, sourceRowIndex, sourceRowIndex + 1);
        }
        public void CopyRowAfter(Worksheet worksheet, uint sourceRowIndex, uint copyCount)
        {
            for (uint i = 0; i < copyCount; i++)
            {
                CopyRowAfter(worksheet, sourceRowIndex);
            }
        }

        public void CopyRowsTo(Worksheet worksheet, uint sourceRowFromIndex, uint sourceRowToIndex, uint destinationRowIndex)
        {
            for (int i = 0; i <= sourceRowToIndex - sourceRowFromIndex; i++)
            {
                var sourceIndex = (uint)(sourceRowFromIndex + i);
                var destIndex = (uint)(destinationRowIndex + i);
                CopyRowTo(worksheet, sourceIndex, destIndex);
            }
        }
        public void CopyRowsAfter(Worksheet worksheet, uint sourceRowFromIndex, uint sourceRowToIndex)
        {
            CopyRowsTo(worksheet, sourceRowFromIndex, sourceRowToIndex, sourceRowToIndex + 1);
        }
        public void CopyRowsAfter(Worksheet worksheet, uint sourceRowFromIndex, uint sourceRowToIndex, uint copyCount)
        {
            //for (uint i = 0; i < copyCount; i++)
            //{
            //    //CopyRowsAfter(worksheet, sourceRowFromIndex, sourceRowToIndex, );
            //}
        }

        public uint GetRowIndex(string cellReference)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"\d+");
            var match = regex.Match(cellReference);
            return uint.Parse(match.Value);
        }
        public string GetColumName(string cellReference)
        {
            var regex = new System.Text.RegularExpressions.Regex("[A-Za-z]+");
            var match = regex.Match(cellReference);
            return match.Value;
        }
        public string ToExcelString(decimal value)
        {
            return value.ToString("F2").Replace(_cultureForConversions.NumberFormat.CurrencyDecimalSeparator, ".");
        }

    }
}
