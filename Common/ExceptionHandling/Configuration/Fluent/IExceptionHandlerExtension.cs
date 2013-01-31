using CodeFiction.Stack.Common.Configuration.Fluent;

namespace CodeFiction.Stack.Common.ExceptionHandling.Configuration.Fluent
{
    public interface IExceptionHandlerExtension : IFluentInterface
    {
        ExceptionPolicy CurrentExceptionPolicy { get; }
    }
}