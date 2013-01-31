namespace CodeFiction.Stack.Common.Utilities.Extensions
{
    public static class ArrayExtensions
    {
        public static string JoinToString<T>(this T[] array, string seperator)
        {
            return EnumerableUtils.JoinToString(array, seperator, item => item.ToString());
        }

        public static bool Contains<T>(this T[] array, T item)
        {
            return ArrayUtils.Contains(array, item);
        }

        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }
    }
}
