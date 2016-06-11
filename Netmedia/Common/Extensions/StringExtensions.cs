using System;
using System.Text;
using DocumentFormat.OpenXml.Drawing;

namespace Netmedia.Common.Extensions
{
    public static class StringExtensions
    {
        public static string FormatUsing(this string template, params string[] parameters)
        {
            return string.Format(template, parameters);
        }
        public static string FormatUsing(this string template, params object[] parameters)
        {
            return string.Format(template, parameters);
        }
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        public static bool IsNotNullOrEmpty(this string value)
        {
            return IsNullOrEmpty(value) == false;
        }

        public static string Left(this string value, int length)
        {
            if (value == null) return string.Empty;
            return value.Substring(0, System.Math.Min(value.Length, length));
        }

        public static string UppercaseFirst(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            return char.ToUpper(value[0]) + value.Substring(1);
        }

        public static string RemoveLastChar(this string value){
            return value.Remove(value.Length - 1, 1);
        }
    }
}
