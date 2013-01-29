using CfCommerce.Common.Configuration.Fluent;

namespace CfCommerce.Common.ExceptionHandling.Configuration.Fluent
{
    public interface IExceptionHandlerExtension : IFluentInterface
    {
        ExceptionPolicy CurrentExceptionPolicy { get; }
    }
}