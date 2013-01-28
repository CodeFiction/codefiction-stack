using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfCommerce.Common.ExceptionHandling
{
    internal class ExceptionHandlingPolicy
    {
        private readonly IList<object> _handlers;

        public IList<object> ExceptionHandlers
        {
            get { return _handlers; }
        }

        public Type ExceptionType { get; private set; }

        public PostHandlingAction PostHandlingAction { get; private set; }

        public ExceptionHandlingPolicy(Type exceptionType, PostHandlingAction handlingAction = PostHandlingAction.ThrowNewException)
        {
            ExceptionType = exceptionType;
            PostHandlingAction = handlingAction;

            _handlers = new List<object>();
        }

        public void AddHandler(ICfExceptionHandler handler)
        {
            ExceptionHandlers.Add(handler);
        }
    }

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
