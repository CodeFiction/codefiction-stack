using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CfCommerce.Common.ExceptionHandling.Handlers
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
