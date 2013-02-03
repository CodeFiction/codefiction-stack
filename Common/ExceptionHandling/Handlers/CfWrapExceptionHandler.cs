using System;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CodeFiction.Stack.Common.ExceptionHandling.Handlers
{
    internal class CfWrapExceptionHandler : ICfExceptionHandler
    {
        private readonly IExceptionHandler _wrapHandler;

        public CfWrapExceptionHandler(string message, Type wrapExceptionType)
        {
            _wrapHandler = new WrapHandler(message, wrapExceptionType);
        }

        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
            return _wrapHandler.HandleException(exception, handlingInstanceId);
        }
    }
}
