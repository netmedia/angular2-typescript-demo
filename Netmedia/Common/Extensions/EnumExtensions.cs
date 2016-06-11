using System;

namespace Netmedia.Common.Extensions
{
    public static class EnumExtensions
    {
        public static T AsEnumValue<T>(this string enumName)
        {
            return (T)Enum.Parse(typeof(T), enumName, true);
        }
    }
}