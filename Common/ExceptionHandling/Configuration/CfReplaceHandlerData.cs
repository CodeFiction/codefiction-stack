using System;

namespace CfCommerce.Common.ExceptionHandling.Configuration
{
    internal class CfReplaceHandlerData : BaseHandlerData
    {
        public string ExceptionMessage { get; set; }
        public Type ExceptionType { get; set; }
    }
}