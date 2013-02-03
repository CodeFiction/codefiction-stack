using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeFiction.Stack.Common.Exceptions;

namespace CodeFiction.Stack.Library.Core.Exceptions
{
    public class InvalidInterceptorException : CoreLevelException 
    {
        public InvalidInterceptorException(string message = null, Exception innerException = null)
            : base(message, innerException)
        {

        }
    }
}
