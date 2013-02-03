using System;

namespace CodeFiction.Stack.Common.ExceptionHandling.Configuration
{
    public class ExceptionPolicy
    {
        private readonly ExceptionHandlingPolicy _exceptionHandlingPolicy;

        internal ExceptionHandlingPolicy ExceptionHandlingPolicy
        {
            get { return _exceptionHandlingPolicy; }
        }

        public ExceptionPolicy(Type exceptionType, PostHandlingAction handlingAction = PostHandlingAction.ThrowNewException)
        {
            _exceptionHandlingPolicy = new ExceptionHandlingPolicy(exceptionType, handlingAction);
        }

        internal void AddExceptionHandlerData(CfHandlerData handler)
        {
            _exceptionHandlingPolicy.AddHandlerData(handler);
        }
    }
}