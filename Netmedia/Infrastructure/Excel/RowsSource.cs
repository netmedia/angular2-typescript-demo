using System;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Netmedia.Infrastructure.Excel
{
    public class RowsSource<BaseObjectType>
    {
        public Func<BaseObjectType, object> ValueGetter { get { return _valueGetter; } }
        private Func<BaseObjectType, object> _valueGetter;
        public CellValues Type { get { return _type; } }
        private CellValues _type;
        public RowsSource(Func<BaseObjectType, object> valueGetter, CellValues type)
        {
            _valueGetter = valueGetter;
            _type = type;
        }
        public RowsSource(Func<BaseObjectType, object> valueGetter)
            : this(valueGetter, CellValues.String) { }
    }

}