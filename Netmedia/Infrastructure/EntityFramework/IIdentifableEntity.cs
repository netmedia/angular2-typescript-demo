using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netmedia.Infrastructure.EntityFramework
{
    public interface IIdentifableEntity
    {
        int Id { get; set; }
    }
}
