using CodeFiction.Common.Configuration.Abstraction;

namespace CodeFiction.Common.Configuration
{
    public static class ConfigManager
    {
        public static TConfigurationType LoadConfiguration<TConfigurationType>(IConfigurationInitializer<TConfigurationType> configurationInitializer)
        {
            return configurationInitializer.Load();
        }
    }
}
