using System;
using System.Reflection;
using CFCommerce.Library.CoreContracts;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using IDependencyResolver = CFCommerce.Library.CoreContracts.IDependencyResolver;

namespace CfCommerce.Library.Core.DependencyResolvers
{
    internal class CastleDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer _container = new WindsorContainer();

        public IDependencyResolver RegisterAssembly(Assembly assembly)
        {
            _container.Register(AllTypes.FromAssembly(assembly));
            return this;
        }

        public IDependencyResolver RegisterInstance<TInterface>(TInterface instance)
            where TInterface : class
        {
            return RegisterInstance(typeof(TInterface), instance);
        }

        public IDependencyResolver RegisterInstance(Type type, object instance)
        {
            _container.Register(Component.For(type).Instance(instance));
            return this;
        }

        public IDependencyResolver Register<TInterface, TService>(InstanceMode mode = InstanceMode.Transient)
            where TService : TInterface
        {
            return Register(typeof(TInterface), typeof(TService), mode);
        }

        public IDependencyResolver Register(Type interfaceType, Type serviceType, InstanceMode mode = InstanceMode.Transient)
        {
            ComponentRegistration<object> componentRegistration = Component.For(interfaceType).ImplementedBy(serviceType);

            if (mode == InstanceMode.Transient)
            {
                componentRegistration = componentRegistration.LifestyleTransient();
            }
            _container.Register(componentRegistration);
            return this;
        }

        public TInterface Resolve<TInterface>()
        {
            return _container.Resolve<TInterface>();
        }

        public TInterface Resolve<TInterface>(string name)
        {
            return _container.Resolve<TInterface>(name);
        }

        public IDependencyResolver TearDown(object instance)
        {
            _container.Release(instance);
            return this;
        }
    }
}
