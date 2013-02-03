using CodeFiction.Common.Configuration;
using CodeFiction.Common.Configuration.Abstraction;
using NUnit.Framework;

namespace CodeFiction.Stack.Sandbox.CoreSandbox
{
    [TestFixture]
    public class Configuration
    {
        public class TestConfig
        {
            public string ConnectionString { get; set; }
            public string ProviderName { get; set; }
        }
        [Test]
        public void Test()
        {
            var config = ConfigManager.LoadConfiguration(new FluentConfiguration());
        }
    }

    public class FluentConfiguration : IConfigurationInitializer<Configuration.TestConfig>
    {
        public Configuration.TestConfig Load()
        {
            return ConfigurationLoader.Init<Configuration.TestConfig>().Set(t => t.ConnectionString = "test")
                                      .Set(config => config.ProviderName = "Test").Get();
        }
    }
}
