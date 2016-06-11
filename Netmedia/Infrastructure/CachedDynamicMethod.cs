using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Netmedia.Infrastructure
{
    public static class CachedDynamicMethod
    {
        private static IDictionary<Type, IDictionary<string, MethodInfo>> _typeMethods = new Dictionary<Type, IDictionary<string, MethodInfo>>();

        public static object Call(object onObject, string methodName, params object[] withValues)
        {
            var typeOfObject = onObject.GetType();
            MethodInfo objectMethod = null;
            if (_typeMethods.ContainsKey(typeOfObject))
            {
                var typeCachedMethods = _typeMethods[typeOfObject];
                if (typeCachedMethods.ContainsKey(methodName))
                {
                    var method = typeCachedMethods[methodName];
                    if (method != null) objectMethod = method;
                }
            }
            else
            {
                _typeMethods.Add(typeOfObject, new Dictionary<string, MethodInfo>());
            }


            if (objectMethod == null)
            {
                objectMethod = onObject.GetType().GetMethod(methodName);
                
                if (objectMethod != null) _typeMethods[typeOfObject].Add(methodName, objectMethod);
            }


            return objectMethod != null ? objectMethod.Invoke(onObject, withValues) : null;
        }
    }
}
