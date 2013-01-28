using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CfCommerce.Common.ExceptionHandling.Handlers
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
