using CodeFiction.Common.Configuration.Helpers;

namespace CodeFiction.Common.Configuration
{
    public class ConfigurationLoader 
    {
        public static ConfigurationHelper<TConfigurationType> Init<TConfigurationType>()
        {
            return new ConfigurationHelper<TConfigurationType>();
        }

        public static ConfigurationHelper<TConfigurationType> Edit<TConfigurationType>(TConfigurationType configurationModel)
            where TConfigurationType : new()
        {
            return new ConfigurationHelper<TConfigurationType>(configurationModel);
        }
    }
}
