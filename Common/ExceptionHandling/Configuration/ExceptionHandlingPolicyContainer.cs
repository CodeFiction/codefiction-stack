using System.Collections.Generic;

namespace CodeFiction.Stack.Common.ExceptionHandling.Configuration
{
    internal class ExceptionHandlingPolicyContainer
    {
        private readonly IList<ExceptionHandlingPolicy> _exceptionHandlingPolicies;

        public IList<ExceptionHandlingPolicy> ExceptionHandlingPolicies
        {
            get { return _exceptionHandlingPolicies; }
        }

        public string Name { get; private set; }

        public ExceptionHandlingPolicyContainer(string name)
        {
            _exceptionHandlingPolicies = new List<ExceptionHandlingPolicy>();
            Name = name;
        }

        public void AddPolicy(ExceptionHandlingPolicy policy)
        {
            ExceptionHandlingPolicies.Add(policy);
        }
    }
}