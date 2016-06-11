using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Netmedia.Infrastructure.Services;

namespace Netmedia.Web.Services
{
    public class CacheService : ICacheService
    {
        public bool Exists(string key)
        {
            return HttpContext.Current.Cache[key] != null;
        }

        public void Insert(string key, object value)
        {
            HttpContext.Current.Cache.Insert(key, value);
        }

        public object Retreive(string key)
        {
            return HttpContext.Current.Cache[key];
        }
    }
}
