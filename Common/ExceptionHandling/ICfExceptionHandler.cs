using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfCommerce.Common.ExceptionHandling
{
    public interface ICfExceptionHandler
    {
        Exception HandleException(Exception exception, Guid handlingInstanceId);
    }
}
