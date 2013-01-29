namespace CfCommerce.Common.ExceptionHandling.Configuration
{
    public class Policy
    {
        private readonly ExceptionHandlingPolicyContainer _exceptionHandlingPolicyContainer;

        internal ExceptionHandlingPolicyContainer ExceptionHandlingPolicyContainer
        {
            get { return _exceptionHandlingPolicyContainer; }
        }

        public Policy(string name)
        {
            _exceptionHandlingPolicyContainer = new ExceptionHandlingPolicyContainer(name); 
        }

        public void AddExceptionHandlingPolicy(ExceptionPolicy policy)
        {
            _exceptionHandlingPolicyContainer.AddPolicy(policy.ExceptionHandlingPolicy);
        }
    }
}