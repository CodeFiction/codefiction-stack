using CodeFiction.Stack.Common.Utilities.Resharper;

namespace CodeFiction.Stack.Common.Utilities.Extensions
{
    public static class StringExtensions
    {
        [ContractAnnotation("null=>true")]
        public static bool IsNullOrEmpty(this string text)
        {
            return string.IsNullOrEmpty(text);
        }
    }
}
