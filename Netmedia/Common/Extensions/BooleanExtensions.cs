using System;

namespace Netmedia.Common.Extensions
{
    public static class BooleanExtensions
    {
        public static bool AsBool(this string stringValue)
        {
            bool boolValue;
            Boolean.TryParse(stringValue, out boolValue);

            return boolValue;
        }
    }
}
