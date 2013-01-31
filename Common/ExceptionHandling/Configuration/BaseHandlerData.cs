using System;
using System.Collections.Generic;

namespace CodeFiction.Stack.Common.ExceptionHandling.Configuration
{
    internal class BaseHandlerData    
    {
        public Type HandlerType { get; set; }
        public Dictionary<string,object> HandlerData { get; set; }
    }
}