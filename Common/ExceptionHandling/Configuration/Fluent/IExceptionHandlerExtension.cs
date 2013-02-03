using CodeFiction.Stack.Library.CoreContracts;

namespace CodeFiction.Stack.Common.ExceptionHandling.Configuration.Fluent
{
    public interface IExceptionHandlerExtension : IFluentInterface
    {
        ExceptionPolicy CurrentExceptionPolicy { get; }
    }
}