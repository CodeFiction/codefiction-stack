namespace CodeFiction.Stack.Common.ExceptionHandling.Configuration
{
    public interface IExceptionHandlingConfiguration
    {
        void AddPolicy(Policy policy);
        Policy[] Policies { get; }
    }
}