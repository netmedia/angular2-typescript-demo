using System.Collections.Generic;

namespace Netmedia.Infrastructure.Excel
{
    public interface ICellMapping<BaseObjectType>
    {
        IDictionary<string, CellSource> GetCellMappings(BaseObjectType values);
    }
}