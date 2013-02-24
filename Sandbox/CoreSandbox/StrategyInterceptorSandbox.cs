using System;
using System.Diagnostics;
using CodeFiction.Stack.Library.Core.Aspect.Attributes;
using CodeFiction.Stack.Library.Core.Castle;
using CodeFiction.Stack.Library.Core.Initializers.Loaders;
using CodeFiction.Stack.Library.CoreContracts;
using CodeFiction.Stack.Library.CoreContracts.Plugins;
using NUnit.Framework;

namespace CodeFiction.Stack.Sandbox.CoreSandbox
{
    [TestFixture]
    public class StrategyInterceptorSandbox
    {
        [Test]
        public void StrategyInterceptorTest()
        {
            Library.Core.Initializers.Bootstrapper.Create()
                    .RegisterComponent(resolver => resolver.Register<ITestService, TestService>(InstanceMode.Transient, typeof(StrategyInterceptor)))
                    .StartApplication(AssemblyLoader.AllLoader);
        }
    }

    public class StrategyTestPlugin : IPlugin
    {
        public string Name
        {
            get { return "test"; }
        }

        public void LoadPlugin(IDependencyResolver resolver)
        {

        }

        public StrategyTestPlugin(ITestService test)
        {

        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class TestAspect : AspectAttributeBase
    {
        [WorksBefore]
        public void BeforeMethodExecuted(IMethodInvocationContext context)
        {
            Debug.Write("Worked Before");
        }

        [WorksAfter]
        public void AfterMethodExecuted(IMethodInvocationContext context)
        {
            Debug.Write("Worked After");
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class TestExceptionAspect : AspectAttributeBase
    {
        [WorksOnException]
        public void OnExecuted(IMethodInvocationContext context)
        {
            Debug.Write("Exception throwed.");
        }
    }

    public interface ITestService
    {
        [TestAspect]
        void DoSomething();
        [TestExceptionAspect]
        void ThrowException();
    }

    public class TestService : ITestService
    {
        public void DoSomething()
        {

        }

        public void ThrowException()
        {
            throw new NotImplementedException();
        }
    }
}
