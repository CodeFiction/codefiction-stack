using System;

namespace CodeFiction.Stack.Common.ExceptionHandling.Configuration
{
    internal class CfReplaceHandlerData : BaseHandlerData
    {
        public string ExceptionMessage { get; set; }
        public Type ExceptionType { get; set; }
    }
}