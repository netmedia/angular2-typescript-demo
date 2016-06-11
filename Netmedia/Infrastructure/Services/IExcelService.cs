using System;
using System.Globalization;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Netmedia.Infrastructure.Services
{
    public interface IExcelService
    {
        void SetCulture(CultureInfo culture);

        Worksheet FindWorksheet(SpreadsheetDocument document, string sheetName);
        void FillCell(Worksheet worksheet, string cellReference, object value, CellValues type, string forcedFormat = "");
        Row FindRow(Worksheet worksheet, uint rowIndex);
        Cell FindCell(Worksheet worksheet, string cellReference);

        void CopyRowTo(Worksheet worksheet, uint sourceRowIndex, uint destinationRowIndex);
        void CopyRowAfter(Worksheet worksheet, uint sourceRowIndex);
        void CopyRowAfter(Worksheet worksheet, uint sourceRowIndex, uint copyCount);

        void CopyRowsTo(Worksheet worksheet, uint sourceRowFromIndex, uint sourceRowToIndex, uint destinationRowIndex);
        void CopyRowsAfter(Worksheet worksheet, uint sourceRowFromIndex, uint sourceRowToIndex);
        void CopyRowsAfter(Worksheet worksheet, uint sourceRowFromIndex, uint sourceRowToIndex, uint copyCount);

        uint GetRowIndex(string cellReference);
        string ToExcelString(decimal value);
    }
}
