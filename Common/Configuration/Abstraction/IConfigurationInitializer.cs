namespace CodeFiction.Common.Configuration.Abstraction
{

    public interface IConfigurationInitializer<out TConfigurationType>
    {
        TConfigurationType Load();
    }
}
