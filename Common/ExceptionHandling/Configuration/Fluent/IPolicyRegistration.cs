using CodeFiction.Stack.Common.Configuration.Fluent;

namespace CodeFiction.Stack.Common.ExceptionHandling.Configuration.Fluent
{
    public interface IPolicyRegistration : IFluentInterface
    {
        IExceptionPolicyRegistration AddPolicyWithName(string name);
    }
}