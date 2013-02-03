using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CodeFiction.Stack.Common.ExceptionHandling.Handlers
{
    class EntLibExceptionHandlerAdapter : IExceptionHandler
    {
        private readonly ICfExceptionHandler _cfExceptionHandler;

        public EntLibExceptionHandlerAdapter(ICfExceptionHandler cfExceptionHandler)
        {
            _cfExceptionHandler = cfExceptionHandler;
        }

        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
           return _cfExceptionHandler.HandleException(exception, handlingInstanceId);
        }
    }
}
