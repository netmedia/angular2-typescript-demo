using System.Collections.Generic;

namespace Netmedia.Infrastructure.Excel
{
    public interface ITableColumnMapping<BaseObjectType>
    {
        uint FirstLineRowIndex { get; }
        int PageSize { get; }//add new (Count - PageSize) rows in template
        IDictionary<string, CellSource> GetTableColumnMappings(BaseObjectType values);
    }
}