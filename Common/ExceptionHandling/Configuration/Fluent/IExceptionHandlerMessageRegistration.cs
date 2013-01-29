namespace CfCommerce.Common.ExceptionHandling.Configuration.Fluent
{
    public interface IExceptionHandlerMessageRegistration : IExceptionHandlerRegistrationForPolicyAndHandler
    {
        IExceptionHandlerRegistrationForPolicyAndHandler UsingMessage(string message);
    }
}