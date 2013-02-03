using System;
using CodeFiction.Common.Configuration.Abstraction;

namespace CodeFiction.Common.Configuration.Helpers
{
    public class ConfigurationHelper<TConfigurationType> : IConfigurationHelper<TConfigurationType>
    {
        private readonly TConfigurationType _configurationInstance;

        public ConfigurationHelper()
            : this(Activator.CreateInstance<TConfigurationType>())
        {
        }

        public ConfigurationHelper(TConfigurationType instance)
        {
            _configurationInstance = instance;
        }

        public IConfigurationHelper<TConfigurationType> Set(Action<TConfigurationType> operation)
        {
            operation(_configurationInstance);
            return this;
        }

        public TConfigurationType Get()
        {
            return _configurationInstance;
        }
    }
}
