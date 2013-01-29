using CfCommerce.Common.Configuration.Fluent;

namespace CfCommerce.Common.ExceptionHandling.Configuration.Fluent
{
    public interface IPolicyRegistration : IFluentInterface
    {
        IExceptionPolicyRegistration AddPolicyWithName(string name);
    }
}