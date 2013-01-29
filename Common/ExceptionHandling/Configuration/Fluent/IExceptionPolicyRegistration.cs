using System;

namespace CfCommerce.Common.ExceptionHandling.Configuration.Fluent
{
    public interface IExceptionPolicyRegistration : IPolicyRegistration
    {
        IExceptionHandlerRegistration ForExceptionType(Type exceptionType);
        IExceptionHandlerRegistration ForExceptionType<T>() where T : Exception;
    }
}