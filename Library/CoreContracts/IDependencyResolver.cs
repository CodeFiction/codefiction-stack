using System;
using System.Reflection;

namespace CFCommerce.Library.CoreContracts
{
    public enum InstanceMode { Singleton, Transient }
    public interface IDependencyResolver
    {
        IDependencyResolver RegisterAssembly(Assembly assembly);

        IDependencyResolver RegisterInstance<TInterface>(TInterface instance)
            where TInterface : class;

        IDependencyResolver RegisterInstance(Type type, object instance);

        IDependencyResolver Register<TInterface, TService>(InstanceMode mode = InstanceMode.Transient) where TService : TInterface;

        IDependencyResolver Register(Type interfaceType, Type serviceType, InstanceMode mode = InstanceMode.Transient);

        TInterface Resolve<TInterface>();

        TInterface Resolve<TInterface>(string name);

        IDependencyResolver TearDown(object instance);
    }
}