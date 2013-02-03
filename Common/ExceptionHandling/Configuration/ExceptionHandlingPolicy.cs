using System;
using System.Collections.Generic;

namespace CodeFiction.Stack.Common.ExceptionHandling.Configuration
{
    internal class ExceptionHandlingPolicy
    {
        private readonly IList<CfHandlerData> _handlerData;

        public IList<CfHandlerData> ExceptionHandlerData
        {
            get { return _handlerData; }
        }

        public Type ExceptionType { get; set; }

        public PostHandlingAction PostHandlingAction { get; set; }

        public ExceptionHandlingPolicy(Type exceptionType, PostHandlingAction handlingAction = PostHandlingAction.ThrowNewException)
        {
            ExceptionType = exceptionType;
            PostHandlingAction = handlingAction;

            _handlerData = new List<CfHandlerData>();
        }

        public void AddHandlerData(CfHandlerData handler)
        {
            ExceptionHandlerData.Add(handler);
        }
    }
}
