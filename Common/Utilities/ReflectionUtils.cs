using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace CfCommerce.Common.Utilities
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
