using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CfCommerce.Common.Utilities
{
    public static class CollectionUtils
    {
        public static List<T> CreateList<T>(params T[] values)
        {
            return new List<T>(values);
        }


        public static bool IsNullOrEmpty(ICollection collection)
        {
            if (collection != null)
            {
                return collection.Count == 0;
            }

            return true;
        }

        public static bool IsNullOrEmpty<T>(ICollection<T> collection)
        {
            if (collection != null)
            {
                return collection.Count == 0;
            }

            return true;
        }

        public static bool ListEquals<T>(IList<T> a, IList<T> b)
        {
            if (a == null || b == null)
            {
                return a == null && b == null;
            }

            if (a.Count != b.Count)
            {
                return false;
            }

            return !a.Where((t, i) => !Equals(t, b[i])).Any();
        }

        public static IList<T> Divide<T>(IList<T> list, IList<T> divisor)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            List<T> result = new List<T>(list.Count);

            result.AddRange(list.Where(t => divisor == null || !divisor.Contains(t)));

            return result;
        }
    }
}
