using System;
using CodeFiction.Stack.Common.Utilities.Extensions;

namespace CodeFiction.Stack.Common.ExceptionHandling.Configuration.Fluent
{
    public class ExceptionPolicyBuilder : 
        IPolicyRegistration, 
        IExceptionPolicyRegistration,
        IExceptionHandlerRegistration,
        IExceptionPolicyPostHandlingActionRegistration,
        IExceptionHandlerRegistrationForPolicyAndHandler,
        IExceptionHandlerExtension
    {
        private readonly IExceptionHandlingConfiguration _exceptionHandlingConfiguration;

        private Policy _currentPolicy;
        private ExceptionPolicy _currentExceptionPolicy;

        internal ExceptionPolicyBuilder(IExceptionHandlingConfiguration exceptionHandlingConfiguration)
        {
            _exceptionHandlingConfiguration = exceptionHandlingConfiguration;
        }

        IExceptionPolicyRegistration IPolicyRegistration.AddPolicyWithName(string name)
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentException("Policy name cannot be null or empty", "name");
            }

            _currentPolicy = new Policy(name);
            _exceptionHandlingConfiguration.AddPolicy(_currentPolicy);
            return this;
        }

        IExceptionHandlerRegistration IExceptionPolicyRegistration.ForExceptionType(Type exceptionType)
        {
            if (exceptionType == null)
                throw new ArgumentNullException("exceptionType");

            if (!typeof(Exception).IsAssignableFrom(exceptionType))
            {
                throw new ArgumentException("Type must be derived from Exception", "exceptionType");
            }
            _currentExceptionPolicy = new ExceptionPolicy(exceptionType);
            _currentPolicy.AddExceptionHandlingPolicy(_currentExceptionPolicy);
            return this;
        }

        IExceptionHandlerRegistration IExceptionPolicyRegistration.ForExceptionType<T>()
        {
            return ((IExceptionPolicyRegistration) this).ForExceptionType(typeof (T));
        }

        IExceptionPolicyRegistration IExceptionPolicyPostHandlingActionRegistration.ThenDoNothing()
        {
            _currentExceptionPolicy.ExceptionHandlingPolicy.PostHandlingAction = PostHandlingAction.None;

            return this;
        }

        IExceptionPolicyRegistration IExceptionPolicyPostHandlingActionRegistration.ThenNotifyRethrow()
        {
            _currentExceptionPolicy.ExceptionHandlingPolicy.PostHandlingAction = PostHandlingAction.NotifyRethrow;

            return this;
        }

        IExceptionPolicyRegistration IExceptionPolicyPostHandlingActionRegistration.ThenThrowNewException()
        {
            _currentExceptionPolicy.ExceptionHandlingPolicy.PostHandlingAction = PostHandlingAction.ThrowNewException;

            return this;
        }

        ExceptionPolicy IExceptionHandlerExtension.CurrentExceptionPolicy
        {
            get { return _currentExceptionPolicy; }
        }
    }
}