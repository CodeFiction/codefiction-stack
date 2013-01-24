using System;

namespace CfCommerce.Library.Core.Exceptions
{
    /// <summary>
    /// Represents the dependency injection container is not defined.
    /// </summary>
    public class DependencyInjectionContainerNotDefinedException : Exception //TODO: implement from a base exception.
    {
        internal DependencyInjectionContainerNotDefinedException(DependencyResolver.DependencyResolvers resolver)
            //TODO: implement base behavior after base exception is created.
        {
            
        }
    }
}
