using System;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CodeFiction.Stack.Library.Core.Exceptions;
using CastleDynamicProxy = Castle.DynamicProxy;
using CodeFiction.Stack.Library.CoreContracts;

namespace CodeFiction.Stack.Library.Core.DependencyResolvers
{
    internal class CastleDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer _container = new WindsorContainer();

        public IDependencyResolver RegisterAssembly(Assembly assembly)
        {
            _container.Register(AllTypes.FromAssembly(assembly));
            return this;
        }

        public IDependencyResolver RegisterInstance<TInterface>(TInterface instance, string name = null, params Type[] interceptors)
            where TInterface : class
        {
            return RegisterInstance(typeof (TInterface), instance, name, interceptors);
        }

        public IDependencyResolver RegisterInstance(Type type, object instance, string name = null, params Type[] interceptors)
        {
            _container.Register(interceptors.IsNullOrEmpty()
                                    ? Component.For(type).Instance(instance).Named(name)
                                    : Component.For(type).Instance(instance).Named(name).Interceptors(interceptors));

            return this;
        }

        public IDependencyResolver Register<TInterface, TService>(InstanceMode mode = InstanceMode.Transient, string name = null, params Type[] interceptors) 
            where TService : TInterface
        {
            return Register(typeof (TInterface), typeof (TService), mode, name, interceptors);
        }

        public IDependencyResolver Register(Type interfaceType, Type serviceType, InstanceMode mode = InstanceMode.Transient, string name = null, params Type[] interceptors)
        {
            var componentRegistration = Component.For(interfaceType).ImplementedBy(serviceType);

            if (mode == InstanceMode.Transient)
            {
                componentRegistration = componentRegistration.LifestyleTransient();
            }

            if (!interceptors.IsNullOrEmpty())
            {
                bool valid = interceptors.All(type => typeof (CastleDynamicProxy.IInterceptor).IsAssignableFrom(type));

                if (!valid)
                {
                    string interceptorMessage = interceptors.Where(type => !typeof (CastleDynamicProxy.IInterceptor).IsAssignableFrom(type)).Select(type => type.FullName).ToList().JoinToString(",");
                    throw new InvalidInterceptorException(string.Format("Following interceptors is Invalid: \n {0}", interceptorMessage));
                }

                componentRegistration.Interceptors(interceptors);
            }

            _container.Register(componentRegistration.Named(name));
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

        public TType CreateInstanceOfType<TType>(Type type)
        {
            var constructors = type.GetConstructors();

            //TODO: given type must have one constructor with parameters or without parameters.
            return (TType)(from constructor in constructors
                           let parameters = constructor.GetParameters()
                           let parameterInstance =
                               parameters.Select(parameterInfo => _container.Resolve(parameterInfo.ParameterType)).ToList()
                           select constructor.Invoke(parameterInstance.ToArray())).FirstOrDefault();
        }
    }
}
