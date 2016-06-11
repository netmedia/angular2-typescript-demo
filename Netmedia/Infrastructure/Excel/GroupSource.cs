using System;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;

namespace Netmedia.Infrastructure.Excel
{
    public class GroupSource<BaseObjectType>
    {
        public Func<IEnumerable<BaseObjectType>> ItemsGetter { get { return _itemsGetter; } }
        private Func<IEnumerable<BaseObjectType>> _itemsGetter;

        public uint FirstLineRowIndex { get { return _firstLineRowIndex; } }
        private uint _firstLineRowIndex;

        public int PageSize { get { return _pageSize; } }//add new (Count - PageSize) rows in template
        private int _pageSize;

        public IDictionary<string, RowsSource<BaseObjectType>> ColumnMappings { get { return _columnMappings; } }
        private IDictionary<string, RowsSource<BaseObjectType>> _columnMappings;

        public GroupSource(uint firstLineRowIndex, int pageSize, Func<IEnumerable<BaseObjectType>> itemsGetter, IDictionary<string, RowsSource<BaseObjectType>> columnMappings)
        {
            _itemsGetter = itemsGetter;
            _firstLineRowIndex = firstLineRowIndex;
            _pageSize = pageSize;
            _columnMappings = columnMappings;
        }
    }
}