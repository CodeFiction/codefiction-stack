using System;
using System.Reflection;

namespace CFCommerce.Library.CoreContracts
{
    /// <summary>
    /// Represents the instance mode for <see cref="IDependencyResolver"/>.
    /// </summary>
    public enum InstanceMode { Singleton, Transient }

    /// <summary>
    /// Represents the dependency injection container interface.
    /// </summary>
    public interface IDependencyResolver
    {
        /// <summary>
        /// Registers the types inside of the given assembly.
        /// </summary>
        IDependencyResolver RegisterAssembly(Assembly assembly);

        /// <summary>
        /// Registers the instance for given interface.
        /// </summary>
        IDependencyResolver RegisterInstance<TInterface>(TInterface instance)
            where TInterface : class;

        /// <summary>
        /// Registers the instance for given type.
        /// </summary>
        IDependencyResolver RegisterInstance(Type type, object instance);

        /// <summary>
        /// Registers the given <typeparam name="TService"></typeparam> for the type <typeparam name="TInterface"></typeparam> with the given instance mode.
        /// </summary>
        IDependencyResolver Register<TInterface, TService>(InstanceMode mode = InstanceMode.Transient) where TService : TInterface;

        /// <summary>
        /// Registers the given <typeparam name="serviceType"></typeparam> for the type <param name="interfaceType"></typeparam> with the given instance mode.
        /// </summary>
        IDependencyResolver Register(Type interfaceType, Type serviceType, InstanceMode mode = InstanceMode.Transient);

        /// <summary>
        /// Resolves and injects properties and constructors if they are able.
        /// </summary>
        TInterface Resolve<TInterface>();

        /// <summary>
        /// Resolves and injects properties and constructors if they are able with the name.
        /// </summary>
        TInterface Resolve<TInterface>(string name);

        /// <summary>
        /// Removes the given instance of type from the container.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        IDependencyResolver TearDown(object instance);
        
        /// <summary>
        /// Creates the instance of the given type and injects the constructor parameters.
        /// </summary>
        TType CreateInstanceOfType<TType>(Type type);

    }
}