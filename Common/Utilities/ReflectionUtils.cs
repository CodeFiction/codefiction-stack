using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace CodeFiction.Stack.Common.Utilities
{
    /// <summary>
    /// Helper class that handles the reflection operations.
    /// </summary>
    public static class ReflectionUtils
    {
        /// <summary>
        /// Finds the types that are implementing the TType in the given assembly.
        /// </summary>
        public static IEnumerable<Type> FindImplementingTypesFromAssembly<TType>(Assembly assembly)
        {
            var types = from type in assembly.GetTypes()
                        where typeof(TType).IsAssignableFrom(type) && !type.IsInterface
                        select type;
            return types;
        }

        /// <summary>
        /// Creates the instance of given type with the given arguments.
        /// <remarks>Arguments in the argumentslist must be matched with the name in the constructor parameters.</remarks>
        /// <remarks>Chooses the constructor that has the longest signature.</remarks>
        /// </summary>
        public static TType CreateInstanceOfType<TType>(Type objectType, Dictionary<string, object> argumentsList)
            where TType : class
        {
            var x = Activator.CreateInstance(typeof(object), null);
            var constructor = objectType.GetConstructors().OrderByDescending(t => t.GetParameters().Count()).FirstOrDefault();
            if (constructor == null)
            {
                //TODO: throw exception.
                throw new Exception();
            }
            ArrayList args = new ArrayList();
            foreach (var parameterInfo in constructor.GetParameters())
            {
                var hasParameter = argumentsList != null && argumentsList.ContainsKey(parameterInfo.Name);
                if (!hasParameter && parameterInfo.ParameterType.IsInterface && parameterInfo.ParameterType.IsAbstract)
                {
                    throw new Exception("abstract, interface data must be added to the arguments list.");
                }
                args.Add(!hasParameter ? (!parameterInfo.ParameterType.IsValueType ? null : Activator.CreateInstance(parameterInfo.ParameterType)) : argumentsList[parameterInfo.Name]);
            }
            return Activator.CreateInstance(objectType, args.ToArray()) as TType;
        }
    }
}
