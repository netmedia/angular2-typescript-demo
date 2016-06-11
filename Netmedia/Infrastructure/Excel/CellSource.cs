using System;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;

namespace Netmedia.Infrastructure.Excel
{
    public class CellSource
    {
        public string ForcedFormat { get { return _forcedFormat;  } }
        private string _forcedFormat;

        public Func<object> ValueGetter { get { return _valueGetter; } }
        private Func<object> _valueGetter;
        public CellValues Type { get { return _type; } }
        private CellValues _type;
        public CellSource(Func<object> valueGetter, CellValues type, string forcedFormat)
        {
            _valueGetter = valueGetter;
            _type = type;
            _forcedFormat = forcedFormat;
        }
        public CellSource(Func<object> valueGetter, CellValues type) : this(valueGetter, type, null) {}
        public CellSource(Func<object> valueGetter, string forcedFormat) : this(valueGetter, CellValues.String, forcedFormat) { }
        public CellSource(Func<object> valueGetter) : this(valueGetter, CellValues.String) { }
    }

    public class FixedCellSource
    {
        public string CellReference { get { return _cellReference; } }
        private string _cellReference;

        public Func<object> ValueGetter { get { return _valueGetter; } }
        private Func<object> _valueGetter;

        public CellValues Type { get { return _type; } }
        private CellValues _type;

        public FixedCellSource(string cellReference, Func<object> valueGetter, CellValues type)
        {
            _valueGetter = valueGetter;
            _type = type;
            _cellReference = cellReference;
        }
        public FixedCellSource(string cellReference, Func<object> valueGetter)
            : this(cellReference, valueGetter, CellValues.String) { }
    }
    public class TableSource<ItemsType>
    {
        public Func<ItemsType> ItemsGetter { get { return _itemsGetter; } }
        private Func<ItemsType> _itemsGetter;

        public List<FixedCellSource> FixedCellMappings { get { return _fixedCellMappings; } }
        private List<FixedCellSource> _fixedCellMappings;

        public uint SourceFromRowIndex { get { return _sourceFromRowIndex; } }
        private uint _sourceFromRowIndex = 0;

        public uint SourceToRowIndex { get { return _sourceToRowIndex; } }
        private uint _sourceToRowIndex = 0;

        public uint PageSize { get { return _pageSize; } }
        private uint _pageSize = 0;

        public TableSource(uint sourceFromRowIndex, uint sourceToRowIndex, uint pageSize, Func<ItemsType> itemsGetter, List<FixedCellSource> fixedCellMappings)
        {
            _itemsGetter = itemsGetter;
            _fixedCellMappings = fixedCellMappings;
        }
    }

}