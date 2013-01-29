namespace CfCommerce.Common.ExceptionHandling.Configuration.Fluent
{
    public interface IExceptionHandlerRegistrationForPolicyAndHandler : 
        IPolicyRegistration,
        IExceptionHandlerRegistration, 
        IExceptionPolicyPostHandlingActionRegistration
    {

    }
}