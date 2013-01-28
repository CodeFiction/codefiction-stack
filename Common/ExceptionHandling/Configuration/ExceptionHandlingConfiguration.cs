using System;
using System.Collections.Generic;
using System.Linq;

namespace CfCommerce.Common.ExceptionHandling.Configuration
{
    public static class ExceptionHandlerBuilder
    {
        
    }

    public static class Policy
    {
        public static PolicyRegistration Add(string name)
        {
            return new PolicyRegistration(name);
        }
    }

    public static class ExceptionPolicy
    {
        public static ExceptionPolicyRegistration Add(Type exceptionType, PostHandlingAction handlingAction = PostHandlingAction.ThrowNewException)
        {
            return new ExceptionPolicyRegistration(exceptionType, handlingAction);
        }

        public static ExceptionPolicyRegistration Add<T>(PostHandlingAction handlingAction = PostHandlingAction.ThrowNewException)
            where T : Exception
        {
            return new ExceptionPolicyRegistration(typeof(T), handlingAction);
        }
    }

    public class ExceptionHandlingConfiguration : IExceptionHandlingConfiguration
    {
        private PolicyRegistration[] _policyRegistrations;

        public PolicyRegistration[] PolicyRegistrations { get { return _policyRegistrations; } }

        public IExceptionHandlingConfiguration AddPolicy(PolicyRegistration[] exceptionPolicies)
        {
            // TODO : throw libraray specific Exception

            if (exceptionPolicies.IsNullOrEmpty())
            {
                throw new InvalidOperationException("Must be at least one policy");
            }

            if (exceptionPolicies.Any(registration => registration == null))
            {
                throw new InvalidOperationException("Policies can not be null");
            }

            foreach (var policyRegistration in exceptionPolicies)
            {
                ExceptionHandlingPolicyContainer container = policyRegistration.ExceptionHandlingPolicyContainer;

                if (container.Name.IsNullOrEmpty())
                {
                    throw new InvalidOperationException("Policy must have name");
                }

                if (container.ExceptionHandlingPolicies.IsNullOrEmpty())
                {
                    throw new InvalidOperationException(String.Format("Must be at least one exception handling policy. Policy Name {0}",policyRegistration.ExceptionHandlingPolicyContainer.Name));
                }

                if (container.ExceptionHandlingPolicies.Select(policy => policy.ExceptionType.FullName).Count() 
                    != container.ExceptionHandlingPolicies.Select(policy => policy.ExceptionType.FullName).Distinct().Count())
                {
                    throw new InvalidOperationException(String.Format("Policy can not contain more than one same type of exception.  Policy Name {0}", policyRegistration.ExceptionHandlingPolicyContainer.Name));
                }
            }

            if (exceptionPolicies.Select(registration => registration.ExceptionHandlingPolicyContainer.Name).Count()
                    != exceptionPolicies.Select(registration => registration.ExceptionHandlingPolicyContainer.Name).Distinct().Count())
            {
                throw new InvalidOperationException("Can not have more than one policy with the same name.");
            }

            _policyRegistrations = exceptionPolicies;

            return this;
        }
    }


    public interface IExceptionHandlingConfiguration    
    {
        IExceptionHandlingConfiguration AddPolicy(PolicyRegistration[] exceptionPolicies);
        PolicyRegistration[] PolicyRegistrations { get; }
    }

    public class PolicyRegistration
    {
        private readonly ExceptionHandlingPolicyContainer _exceptionHandlingPolicyContainer;

        internal ExceptionHandlingPolicyContainer ExceptionHandlingPolicyContainer
        {
            get { return _exceptionHandlingPolicyContainer; }
        }

        public PolicyRegistration(string name)
        {
            _exceptionHandlingPolicyContainer = new ExceptionHandlingPolicyContainer(name); 
        }

        public PolicyRegistration AddExceptionHandlingPolicy(ExceptionPolicyRegistration policyRegistration)
        {
            _exceptionHandlingPolicyContainer.AddPolicy(policyRegistration.ExceptionHandlingPolicy);
            return this;
        }
    }

    public class ExceptionPolicyRegistration
    {
        private readonly ExceptionHandlingPolicy _exceptionHandlingPolicy;
        
        internal ExceptionHandlingPolicy ExceptionHandlingPolicy
        {
            get { return _exceptionHandlingPolicy; }
        }

        public ExceptionPolicyRegistration(Type exceptionType, PostHandlingAction handlingAction = PostHandlingAction.ThrowNewException)
        {
            _exceptionHandlingPolicy = new ExceptionHandlingPolicy(exceptionType, handlingAction);
        }

        ExceptionPolicyRegistration AddExceptionHandler<T>() 
            where T : ICfExceptionHandler
        {
            // TODO: Could be singleton
            ICfExceptionHandler cfExceptionHandler = Activator.CreateInstance<T>() as ICfExceptionHandler;
            return AddExceptionHandler(cfExceptionHandler);
        }

        ExceptionPolicyRegistration AddExceptionHandler(ICfExceptionHandler handler)
        {
            _exceptionHandlingPolicy.AddHandler(handler);
            return this;
        }
    }
}
