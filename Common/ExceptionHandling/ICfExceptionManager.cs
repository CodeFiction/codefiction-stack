using System;
using CfCommerce.Common.ExceptionHandling.Configuration;

namespace CfCommerce.Common.ExceptionHandling
{
    public interface ICfExceptionManager
    {
        ICfExceptionManager Configure(IExceptionHandlingConfiguration configuration);

        bool HandleException(Exception exceptionToHandle, string policyName, out Exception exceptionToThrow);

        bool HandleException(Exception exceptionToHandle, string policyName);

        void Process(Action action, string policyName);

        TResult Process<TResult>(Func<TResult> action, TResult defaultResult, string policyName);

        TResult Process<TResult>(Func<TResult> action, string policyName);
    }
}