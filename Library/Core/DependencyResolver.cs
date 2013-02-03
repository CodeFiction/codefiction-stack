using System;
using CodeFiction.Stack.Library.Core.DependencyResolvers;
using CodeFiction.Stack.Library.Core.Exceptions;
using CodeFiction.Stack.Library.CoreContracts;

namespace CodeFiction.Stack.Library.Core
{
    /// <summary>
    /// Activates the default Dependency Resolver.
    /// </summary>
    public class DependencyResolve
    {
        internal enum DependencyResolvers { Castle, Unity, Funq, NInject }

        private static readonly Lazy<IDependencyResolver> _current = new Lazy<IDependencyResolver>(() => DependencyResolverSelector.GetResolver(DependencyResolvers.Castle), true);

        /// <summary>
        /// Gets the default dependency injection resolver.
        /// <remarks>Dependency injection containers are constructed with the DependencyResolverSelector.</remarks>
        /// </summary>
        internal static IDependencyResolver Current
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
                    case DependencyResolvers.Funq:
                    case DependencyResolvers.NInject:
                    case DependencyResolvers.Unity:
                        break;
                }
                throw new DependencyInjectionContainerNotDefinedException(resolver);
            }
        }
    }
}
