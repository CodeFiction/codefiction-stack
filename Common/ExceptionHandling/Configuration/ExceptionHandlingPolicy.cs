using System;
using System.Collections.Generic;

namespace CfCommerce.Common.ExceptionHandling.Configuration
{
    internal class ExceptionHandlingPolicy
    {
        private readonly IList<BaseHandlerData> _handlerData;

        public IList<BaseHandlerData> ExceptionHandlerData
        {
            get { return _handlerData; }
        }

        public Type ExceptionType { get; set; }

        public PostHandlingAction PostHandlingAction { get; set; }

        public ExceptionHandlingPolicy(Type exceptionType, PostHandlingAction handlingAction = PostHandlingAction.ThrowNewException)
        {
            ExceptionType = exceptionType;
            PostHandlingAction = handlingAction;

            _handlerData = new List<BaseHandlerData>();
        }

        public void AddHandlerData(BaseHandlerData handler)
        {
            ExceptionHandlerData.Add(handler);
        }
    }
}
