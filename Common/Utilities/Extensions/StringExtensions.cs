using CodeFiction.Stack.Common.Utilities.Resharper;

namespace System
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
