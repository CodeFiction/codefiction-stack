using CodeFiction.Stack.Library.Core.Initializers.Loaders;
using CodeFiction.Stack.Library.CoreContracts;
using CodeFiction.Stack.Library.CoreContracts.Plugins;
using NUnit.Framework;

namespace CodeFiction.Stack.Sandbox.CoreSandbox
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
            CodeFiction.Stack.Library.Core.Initializers.Bootstrapper.Create()
                                .RegisterComponent(resolver => resolver.Register<IData, Data>())
                                .RegisterComponent(resolver => resolver.Register<ITest, Test>())
                                .StartApplication(AssemblyLoader.AllLoader);

        }
    }
}
