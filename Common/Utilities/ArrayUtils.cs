using System.Linq;

namespace CodeFiction.Stack.Common.Utilities
{
    public static class ArrayUtils
    {
        public static T[] AddToArray<T>(T[] array, params T[] items)
        {
            T[] result = new T[array.Length + items.Length];

            int i;
            for (i = 0; i < array.Length; i++)
            {
                result[i] = array[i];
            }

            int j = 0;
            for (i = array.Length; i < result.Length; i++, j++)
            {
                result[i] = items[j];
            }

            return result;
        }

        public static bool Contains<T>(T[] array, T item)
        {
            return array.ToList().Contains(item);
        }
    }
}
