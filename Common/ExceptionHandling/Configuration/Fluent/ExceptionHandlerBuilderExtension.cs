namespace CfCommerce.Common.ExceptionHandling.Configuration.Fluent
{
    public abstract class ExceptionHandlerBuilderExtension : IExceptionHandlerRegistrationForPolicyAndHandler, IExceptionHandlerExtension
    {
        protected IExceptionHandlerRegistrationForPolicyAndHandler Context { get; private set; }

        protected ExceptionHandlerBuilderExtension(IExceptionHandlerRegistration context)
        {
            this.Context = (IExceptionHandlerRegistrationForPolicyAndHandler)context;
        }

        public ExceptionPolicy CurrentExceptionPolicy
        {
            get
            {
                return ((IExceptionHandlerExtension)Context).CurrentExceptionPolicy;
            }
        }

        IExceptionPolicyRegistration IPolicyRegistration.AddPolicyWithName(string name)
        {
            return Context.AddPolicyWithName(name);
        }

        IExceptionPolicyRegistration IExceptionPolicyPostHandlingActionRegistration.ThenDoNothing()
        {
            return Context.ThenDoNothing();
        }

        IExceptionPolicyRegistration IExceptionPolicyPostHandlingActionRegistration.ThenNotifyRethrow()
        {
            return Context.ThenNotifyRethrow();
        }

        IExceptionPolicyRegistration IExceptionPolicyPostHandlingActionRegistration.ThenThrowNewException()
        {
            return Context.ThenThrowNewException();
        }
    }
}