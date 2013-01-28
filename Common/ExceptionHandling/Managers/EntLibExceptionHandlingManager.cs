using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using CfCommerce.Common.ExceptionHandling.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CfCommerce.Common.ExceptionHandling.Managers
{
    public class EntLibExceptionHandlingManager : ICfExceptionManager
    {
        private Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionManager _exceptionManager;

        public ICfExceptionManager Configure(IExceptionHandlingConfiguration configuration)
        {
            PolicyRegistration[] policyRegistrations = configuration.PolicyRegistrations;

            Dictionary<string, ExceptionPolicyImpl> policyEntries = new Dictionary<string, ExceptionPolicyImpl>();

            foreach (var policyRegistration in policyRegistrations)
            {
                ExceptionHandlingPolicyContainer container = policyRegistration.ExceptionHandlingPolicyContainer;

                policyEntries.Add(container.Name, new ExceptionPolicyImpl(container.Name,
                                                                          container.ExceptionHandlingPolicies.Select(
                                                                              policy => new ExceptionPolicyEntry(
                                                                                  policy.ExceptionType,
                                                                                  GetPostHandlingAction(policy.PostHandlingAction),
                                                                                  policy.ExceptionHandlers.Select(o => o as IExceptionHandler)))));

            }

            _exceptionManager = new ExceptionManagerImpl(policyEntries);

            return this;
        }

        public bool HandleException(Exception exceptionToHandle, string policyName, out Exception exceptionToThrow)
        {
          return _exceptionManager.HandleException(exceptionToHandle, policyName, out exceptionToThrow);
        }

        public bool HandleException(Exception exceptionToHandle, string policyName)
        {
            return _exceptionManager.HandleException(exceptionToHandle, policyName);
        }

        public void Process(Action action, string policyName)
        {
            _exceptionManager.Process(action, policyName);
        }

        public TResult Process<TResult>(Func<TResult> action, TResult defaultResult, string policyName)
        {
           return _exceptionManager.Process(action, defaultResult, policyName);
        }

        public TResult Process<TResult>(Func<TResult> action, string policyName)
        {
            return _exceptionManager.Process(action, policyName);
        }

        private Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.PostHandlingAction GetPostHandlingAction(PostHandlingAction postHandlingAction)
        {
            if (postHandlingAction == PostHandlingAction.None)
            {
                return Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.PostHandlingAction.None;
            }

            if (postHandlingAction == PostHandlingAction.ThrowNewException)
            {
                return Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.PostHandlingAction.ThrowNewException;
            }

            if (postHandlingAction == PostHandlingAction.NotifyRethrow)
            {
                return Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.PostHandlingAction.NotifyRethrow;
            }

            throw new NotSupportedException(String.Format("Enterprise library post handling action not supported. Action : {0}", postHandlingAction));
        }
    }
}
