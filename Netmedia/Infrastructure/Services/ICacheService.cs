using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Netmedia.Infrastructure.Services
{
    public interface ICacheService
    {
        bool Exists(string key);
        void Insert(string key, object value);
        object Retreive(string key);
    }
}
