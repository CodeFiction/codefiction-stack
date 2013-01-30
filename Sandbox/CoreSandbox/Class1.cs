using CFCommerce.Library.CoreContracts;
using CFCommerce.Library.CoreContracts.Plugins;
using CfCommerce.Library.Core;
using CfCommerce.Library.Core.Initializers;
using CfCommerce.Library.Core.Initializers.Loaders;
using NUnit.Framework;

namespace CoreSandbox
{
    public class TestPlugin : IPlugin
    {
        public string Name
        {
            get { return "test"; }
        }

        public void LoadPlugin(IDependencyResolver resolver)
        {

        }

        public TestPlugin(ITest test, SandboxCore.IData data)
        {

        }
    }

    public interface ITest
    {
        string TT { get; set; }
    }

    public class Test : ITest
    {
        public string TT { get; set; }
    }

    [TestFixture]
    public class SandboxCore
    {

        public interface IData
        {
            string Name { get; set; }
        }
        public class Data : IData
        {
            public string Name { get; set; }
        }

        [Test]
        public void Bootstrapper()
        {
            CfCommerce.Library.Core.Initializers.Bootstrapper.Create()
                                .RegisterComponent(resolver => resolver.Register<IData, Data>())
                                .RegisterComponent(resolver => resolver.Register<ITest, Test>())
                                .StartApplication(AssemblyLoader.AllLoader);

        }
    }
}
