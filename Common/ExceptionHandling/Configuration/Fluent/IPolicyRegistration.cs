using CodeFiction.Stack.Library.CoreContracts;

namespace CodeFiction.Stack.Common.ExceptionHandling.Configuration.Fluent
{
    public interface IPolicyRegistration : IFluentInterface
    {
        IExceptionPolicyRegistration AddPolicyWithName(string name);
    }
}