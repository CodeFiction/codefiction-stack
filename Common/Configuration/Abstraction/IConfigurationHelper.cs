using System;
using CodeFiction.Stack.Library.CoreContracts;

namespace CodeFiction.Common.Configuration.Abstraction
{
    public interface IConfigurationHelper<out TConfigurationType> : IFluentInterface
    {
        IConfigurationHelper<TConfigurationType> Set(Action<TConfigurationType> operation);
        TConfigurationType Get();
    }
}
