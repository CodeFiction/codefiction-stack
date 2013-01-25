using System;
using CfCommerce.Common.Exceptions;

namespace CfCommerce.Library.Core.Exceptions
{
    /// <summary>
    /// Represents the dependency injection container is not defined.
    /// </summary>
    public class DependencyInjectionContainerNotDefinedException : CoreLevelException
    {
        public DependencyInjectionContainerNotDefinedException(string message = null, Exception innerException = null)
            : base(message, innerException)
        {
        }

        internal DependencyInjectionContainerNotDefinedException(DependencyResolver.DependencyResolvers resolver)
            //TODO: implement base behavior after base exception is created.
        {
            
        }
    }
}
