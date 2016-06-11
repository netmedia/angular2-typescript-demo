using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netmedia.Infrastructure.Excel
{
    public interface IGroupsMapping<BaseObjectType, ChildObjectType>
    {
        IDictionary<string, GroupSource<ChildObjectType>> GetTables(BaseObjectType values);
    }
}
