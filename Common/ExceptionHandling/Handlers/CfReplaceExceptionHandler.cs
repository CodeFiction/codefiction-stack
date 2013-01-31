using System;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CodeFiction.Stack.Common.ExceptionHandling.Handlers
{
    public class CfReplaceExceptionHandler : ICfExceptionHandler, IExceptionHandler
    {
        private readonly IExceptionHandler _replaceHandler;

        public CfReplaceExceptionHandler(string message, Type replaceExceptionType)
        {
            _replaceHandler = new ReplaceHandler(message, replaceExceptionType);
        }

        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
            return _replaceHandler.HandleException(exception, handlingInstanceId);
        }
    }
}
