using System;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CodeFiction.Stack.Common.ExceptionHandling.Handlers
{
    internal class CfWrapExceptionHandler : ICfExceptionHandler, IExceptionHandler
    {
        private readonly IExceptionHandler _wrapHandler;

        public CfWrapExceptionHandler(string message, Type replaceExceptionType)
        {
            _wrapHandler = new WrapHandler(message, replaceExceptionType);
        }

        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
            return _wrapHandler.HandleException(exception, handlingInstanceId);
        }
    }
}
