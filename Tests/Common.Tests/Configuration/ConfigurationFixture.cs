using CodeFiction.Common.Configuration;
using CodeFiction.Common.Configuration.Abstraction;
using Moq;
using NUnit.Framework;

namespace Common.Tests.Configuration
{
    [TestFixture]
    public class ConfigurationFixture
    {
        public class FooConfig
        {
            public string ConnectionFoo { get; set; }
            public string BarValue { get; set; }

        }

        [Test]
        public void Configuration_should_created_by_fluent_loader()
        {
            const string barvalue = "test";
            const string connectionFoo = "test con";
            var config = ConfigurationLoader.Init<FooConfig>()
                         .Set(t => t.ConnectionFoo = connectionFoo)
                         .Set(t => t.BarValue = barvalue).Get();

            Assert.AreEqual(barvalue, config.BarValue);
            Assert.AreEqual(connectionFoo, config.ConnectionFoo);
        }

        [Test]
        public void Configuration_should_edited_by_fluent_loader()
        {
            const string barvalue = "test";
            const string connectionFoo = "test con";

            var configFoo = new FooConfig
            {
                BarValue = "test",
                ConnectionFoo = "connection key"
            };

            var config = ConfigurationLoader.Edit(configFoo)
                         .Set(t => t.ConnectionFoo = connectionFoo)
                         .Set(t => t.BarValue = barvalue).Get();

            Assert.AreEqual(barvalue, config.BarValue);
            Assert.AreEqual(connectionFoo, config.ConnectionFoo);
        }

        [Test]
        public void Configuration_should_loaded_by_custom_loaders()
        {
            var configurationInitializer = new Mock<IConfigurationInitializer<FooConfig>>();
            configurationInitializer.Setup(initializer => initializer.Load()).Returns(new FooConfig());

            var loadedConfig = ConfigManager.LoadConfiguration(configurationInitializer.Object);
            
            configurationInitializer.Verify(initializer => initializer.Load(), Times.Once());
            Assert.IsNotNull(loadedConfig);
        }
    }
}


