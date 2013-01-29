using System;
using System.Collections.Generic;

namespace CfCommerce.Common.ExceptionHandling.Configuration
{
    internal class BaseHandlerData    
    {
        public Type HandlerType { get; set; }
        public Dictionary<string,object> HandlerData { get; set; }
    }
}