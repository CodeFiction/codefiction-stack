using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeFiction.Stack.Common.ExceptionHandling.Configuration;
using CodeFiction.Stack.Common.ExceptionHandling.Handlers;
using CodeFiction.Stack.Common.Utilities;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CodeFiction.Stack.Common.ExceptionHandling.Managers
{
    public class EntLibExceptionHandlingManager : ICfExceptionManager
    {
        private Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionManager _exceptionManager;

        public void Configure(IExceptionHandlingConfiguration configuration)
        {
            Policy[] policies = configuration.Policies;

            Dictionary<string, ExceptionPolicyImpl> policyEntries = new Dictionary<string, ExceptionPolicyImpl>();

            foreach (Policy policy in policies)
            {
                ExceptionHandlingPolicyContainer container = policy.ExceptionHandlingPolicyContainer;

                policyEntries.Add(container.Name,
                                  new ExceptionPolicyImpl(container.Name,
                                                          container.ExceptionHandlingPolicies.Select(
                                                              handlingPolicy =>
                                                                  new ExceptionPolicyEntry(handlingPolicy.ExceptionType,
                                                                                           GetPostHandlingAction(handlingPolicy.PostHandlingAction),
                                                                                           BuildEntLibExceptionHandlers(handlingPolicy.ExceptionHandlerData)))));
            }

            _exceptionManager = new ExceptionManagerImpl(policyEntries);
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

        private IEnumerable<IExceptionHandler> BuildEntLibExceptionHandlers(IEnumerable<CfHandlerData> data)
        {
            List<IExceptionHandler> exceptionHandlers = new List<IExceptionHandler>();

            foreach (CfHandlerData baseHandlerData in data)
            {
                // TODO : Use Cache

                if (typeof(ICfExceptionHandler).IsAssignableFrom(baseHandlerData.HandlerType))
                {
                    var handler = ReflectionUtils.CreateInstanceOfType<ICfExceptionHandler>(baseHandlerData.HandlerType, baseHandlerData.HandlerData);
                    EntLibExceptionHandlerAdapter adapter = new EntLibExceptionHandlerAdapter(handler);
                    exceptionHandlers.Add(adapter);
                    continue;
                }

                // TODO : throw library spesific exception
                throw new InvalidOperationException(string.Format("Type must be derived from IExceptionHandler. Type : {0}", baseHandlerData.HandlerType.FullName));
            }

            return exceptionHandlers;
        }
    }
}
