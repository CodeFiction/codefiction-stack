using CfCommerce.Library.Core;
using NUnit.Framework;

namespace CoreSandbox
{
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
        public void ContainerTest()
        {
            var resolver = DependencyResolver.Current;
            resolver.Register<IData, Data>();
            var data = resolver.Resolve<IData>();
            data.Name = "test";
            resolver.TearDown(data);
            data  = resolver.Resolve<IData>();

        }
        [Test]
        public void ContainerTest2()
        {
            var resolver = DependencyResolver.Current;
            resolver.RegisterInstance<IData>(new Data { Name = "test" });
            var data2 = resolver.Resolve<IData>();
        }
    }
}
