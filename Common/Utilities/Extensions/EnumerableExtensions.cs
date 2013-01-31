using System;
using System.Collections.Generic;
using System.Linq;
using CodeFiction.Stack.Common.Utilities.Resharper;

namespace CodeFiction.Stack.Common.Utilities.Extensions
{
    public static class EnumerableExtensions
    {
        public static string JoinToString<T>(this IEnumerable<T> list, string seperator, Func<T, string> func)
        {
            return EnumerableUtils.JoinToString(list, seperator, func);
        }

        public static string JoinToString<T>(this IEnumerable<T> list, string seperator)
        {
            return EnumerableUtils.JoinToString(list, seperator);
        }

        [ContractAnnotation("null=>true")]
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list == null || !list.Any();
        }
    }
}
