using System;
using System.Collections.Generic;
using System.Linq;

namespace Netmedia.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEachWhenNotNull<T>(this IList<T> value, Action<T> action)
        {
            if (value.IsNullOrEmpty()) return;

            ForEach(value, action);
        }
        public static void ForEach<T>(this IList<T> value, Action<T> action)
        {
            foreach (var item in value)
            {
                action(item);
            }
        }

        public static bool IsNullOrEmpty<T>(this IList<T> value)
        {
            return value == null || value.Count == 0;
        }

        public static bool IsNotNullOrEmpty<T>(this IList<T> value)
        {
            return IsNullOrEmpty(value) == false;
        }



        public static void ForEachWhenNotNull<T>(this ICollection<T> value, Action<T> action)
        {
            if (value.IsNullOrEmpty()) return;

            ForEach(value, action);
        }
        public static void ForEach<T>(this ICollection<T> value, Action<T> action)
        {
            foreach (var item in value)
            {
                action(item);
            }
        }

        public static bool IsNull<T>(this ICollection<T> value)
        {
            return value == null;
        }

        public static bool IsNotNull<T>(this ICollection<T> value)
        {
            return IsNull(value) == false;
        }

        public static bool IsNullOrEmpty<T>(this ICollection<T> value)
        {
            return value == null || value.Count == 0;
        }

        public static bool IsNotNullOrEmpty<T>(this ICollection<T> value)
        {
            return IsNullOrEmpty(value) == false;
        }
    
    }
}