using System;
using System.Collections.Generic;

namespace CfCommerce.Common.ExceptionHandling.Configuration
{
    internal class ExceptionHandlingPolicy
    {
        private readonly IList<BaseHandlerData> _handlers;

        public IList<BaseHandlerData> ExceptionHandlers
        {
            get { return _handlers; }
        }

        public Type ExceptionType { get; set; }

        public PostHandlingAction PostHandlingAction { get; set; }

        public ExceptionHandlingPolicy(Type exceptionType, PostHandlingAction handlingAction = PostHandlingAction.ThrowNewException)
        {
            ExceptionType = exceptionType;
            PostHandlingAction = handlingAction;

            _handlers = new List<BaseHandlerData>();
        }

        public void AddHandler(BaseHandlerData handler)
        {
            ExceptionHandlers.Add(handler);
        }
    }
}
