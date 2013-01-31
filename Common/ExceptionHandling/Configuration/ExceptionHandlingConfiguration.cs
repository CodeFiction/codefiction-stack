using System.Collections.Generic;
using CodeFiction.Stack.Common.ExceptionHandling.Configuration.Fluent;

namespace CodeFiction.Stack.Common.ExceptionHandling.Configuration
{
    public class ExceptionHandlingConfiguration : IExceptionHandlingConfiguration
    {
        private readonly List<Policy> _policies;

        public Policy[] Policies { get { return _policies.ToArray(); } }

        public ExceptionHandlingConfiguration()
        {
            _policies = new List<Policy>();
        }

        //public void AddPolicy(Policy[] exceptionPolicies)
        //{
        //    // TODO : throw libraray specific Exception

        //    if (exceptionPolicies.IsNullOrEmpty())
        //    {
        //        throw new InvalidOperationException("Must be at least one policy");
        //    }

        //    if (exceptionPolicies.Any(registration => registration == null))
        //    {
        //        throw new InvalidOperationException("Policies can not be null");
        //    }

        //    foreach (var policyRegistration in exceptionPolicies)
        //    {
        //        ExceptionHandlingPolicyContainer container = policyRegistration.ExceptionHandlingPolicyContainer;

        //        if (container.Name.IsNullOrEmpty())
        //        {
        //            throw new InvalidOperationException("Policy must have name");
        //        }

        //        if (container.ExceptionHandlingPolicies.IsNullOrEmpty())
        //        {
        //            throw new InvalidOperationException(String.Format("Must be at least one exception handling policy. Policy Name {0}",policyRegistration.ExceptionHandlingPolicyContainer.Name));
        //        }

        //        if (container.ExceptionHandlingPolicies.Select(policy => policy.ExceptionType.FullName).Count() 
        //            != container.ExceptionHandlingPolicies.Select(policy => policy.ExceptionType.FullName).Distinct().Count())
        //        {
        //            throw new InvalidOperationException(String.Format("Policy can not contain more than one same type of exception.  Policy Name {0}", policyRegistration.ExceptionHandlingPolicyContainer.Name));
        //        }
        //    }

        //    if (exceptionPolicies.Select(registration => registration.ExceptionHandlingPolicyContainer.Name).Count()
        //            != exceptionPolicies.Select(registration => registration.ExceptionHandlingPolicyContainer.Name).Distinct().Count())
        //    {
        //        throw new InvalidOperationException("Can not have more than one policy with the same name.");
        //    }

        //    _policies = exceptionPolicies;
        //}

        public void AddPolicy(Policy policy)
        {
            _policies.Add(policy);
        }

        public IPolicyRegistration BuildPolicies()
        {
            return new ExceptionPolicyBuilder(this);
        }
    }
}
