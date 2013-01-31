using System;

namespace CodeFiction.Stack.Common.ExceptionHandling
{
    public interface ICfExceptionHandler
    {
        Exception HandleException(Exception exception, Guid handlingInstanceId);
    }
}
