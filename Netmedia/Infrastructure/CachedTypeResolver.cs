using System;
using System.Collections.Generic;

namespace Netmedia.Infrastructure
{
    public static class CachedTypeResolver
    {
        private static IDictionary<string, Type> _typeCache = new Dictionary<string, Type>();

        public static Type GetTypeBy(string typeFullName)
        {
            Type foundType = null;

            if (_typeCache.ContainsKey(typeFullName)) foundType = _typeCache[typeFullName];
            if (foundType == null)
            {
                foundType = Type.GetType(typeFullName);
                if (foundType != null) _typeCache[typeFullName] = foundType;            
            }

            return foundType;
        }
    }
}