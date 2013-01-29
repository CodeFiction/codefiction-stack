using CfCommerce.Common.Configuration.Fluent;

namespace CfCommerce.Common.ExceptionHandling.Configuration.Fluent
{
    public interface IExceptionPolicyPostHandlingActionRegistration : IFluentInterface
    {
        /// <summary>
        /// End the current exception handling chain by doing nothing more.
        /// </summary>
        /// <returns></returns>
        IExceptionPolicyRegistration ThenDoNothing();

        /// <summary>
        /// End the current exception handling chain by notifying the caller that an exception should be rethrown.
        /// </summary>
        /// <returns></returns>
        IExceptionPolicyRegistration ThenNotifyRethrow();

        /// <summary>
        /// End the current exception handling chain by throwing a new exception.
        /// </summary>
        /// <returns></returns>
        IExceptionPolicyRegistration ThenThrowNewException();
    }
}