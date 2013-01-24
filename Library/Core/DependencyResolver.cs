using CFCommerce.Library.CoreContracts;
using System;
using CfCommerce.Library.Core.DependencyResolvers;
using CfCommerce.Library.Core.Exceptions;


namespace CfCommerce.Library.Core
{
    /// <summary>
    /// Activates the default Dependency Resolver.
    /// </summary>
    public class DependencyResolver
    {
        internal enum DependencyResolvers { Castle, Unity, Funq, NInject }

        private static readonly Lazy<IDependencyResolver> _current = new Lazy<IDependencyResolver>(() => DependencyResolverSelector.GetResolver(DependencyResolvers.Castle), true);

        /// <summary>
        /// Gets the default dependency injection resolver.
        /// <remarks>Dependency injection containers are constructed with the DependencyResolverSelector.</remarks>
        /// </summary>
        public static IDependencyResolver Current
        {
            get { return _current.Value; }
        }



        internal static class DependencyResolverSelector
        {
            public static IDependencyResolver GetResolver(DependencyResolvers resolver)
            {
                switch (resolver)
                {
                    case DependencyResolvers.Castle:
                        return new CastleDependencyResolver();
                        break;
                    case DependencyResolvers.Funq:
                        break;
                    case DependencyResolvers.NInject:
                        break;
                    case DependencyResolvers.Unity:
                        break;
                }
                throw new DependencyInjectionContainerNotDefinedException(resolver);
            }
        }
    }
}
