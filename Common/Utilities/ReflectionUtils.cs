using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CodeFiction.Stack.Common.Utilities
{
    public static class ReflectionUtils
    {
        public static IEnumerable<Type> FindImplementingTypesFromAssembly<TType>(Assembly assembly)
        {
            var types = from type in assembly.GetTypes()
                        where typeof(TType).IsAssignableFrom(type) && !type.IsInterface
                        select type;
            return types;
        }
    }
}
