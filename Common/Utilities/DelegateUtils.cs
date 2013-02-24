using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CodeFiction.Stack.Common.Utilities
{
    public static class DelegateUtils
    {
        // TODO : Move to real cache container
        private static readonly Dictionary<string,object> CacheContainer = new Dictionary<string, object>(); 
        
        public static Delegate GetGetter(PropertyInfo property)
        {
            string key = GetKeyName(property.DeclaringType.FullName, string.Format("{0}Getter", property.Name));

            Delegate propertDelegate;

            if (CacheContainer.ContainsKey(key))
            {
                propertDelegate = (Delegate)CacheContainer[key];
            }
            else
            {
                propertDelegate = CreateGetter(property);
                CacheContainer.Add(key, propertDelegate);
            }

            return propertDelegate;
        }

        public static Delegate GetSetter(PropertyInfo property)
        {
            string key = GetKeyName(property.DeclaringType.FullName, string.Format("{0}Setter", property.Name));

            Delegate propertDelegate;

            if (CacheContainer.ContainsKey(key))
            {
                propertDelegate = (Delegate)CacheContainer[key];
            }
            else
            {
                propertDelegate = CreateSetter(property);
                CacheContainer.Add(key, propertDelegate);
            }

            return propertDelegate;
        }

        public static Delegate GetMethod(MethodInfo methodInfo, object target = null)
        {
            string key = GetKeyName(methodInfo.DeclaringType.FullName, methodInfo.Name);

            Delegate methodDelegate;

            if (CacheContainer.ContainsKey(key))
            {
                methodDelegate = (Delegate)CacheContainer[key];
            }
            else
            {
                methodDelegate = CreateMethod(methodInfo);
                CacheContainer.Add(key, methodDelegate);
            }

            return methodDelegate;
        }

        public static Delegate GetGenericMethod(MethodInfo methodInfo, params Type[] genericTypes)
        {
            string joinToString = genericTypes.Select(t => t.FullName).JoinToString("-");
            string key = GetKeyName(methodInfo.DeclaringType.FullName, string.Format("{0}{1}", methodInfo.Name, joinToString));

            Delegate methodDelegate;

            if (CacheContainer.ContainsKey(key))
            {
                methodDelegate = (Delegate)CacheContainer[key];
            }
            else
            {
                methodInfo = methodInfo.MakeGenericMethod(genericTypes);
                methodDelegate = CreateMethod(methodInfo);
                CacheContainer.Add(key, methodDelegate);
            }

            return methodDelegate;
        } 

        public static Delegate CreateSetter(PropertyInfo property)
        {
            var objParm = Expression.Parameter(property.DeclaringType, "o");
            var valueParm = Expression.Parameter(property.PropertyType, "value");
            Type delegateType = typeof(Action<,>).MakeGenericType(property.DeclaringType, property.PropertyType);
            var lambda = Expression.Lambda(delegateType, Expression.Assign(Expression.Property(objParm, property.Name), valueParm), objParm, valueParm);
            return lambda.Compile();
        }

        public static Delegate CreateGetter(PropertyInfo property)
        {
            var objParm = Expression.Parameter(property.DeclaringType, "o");
            Type delegateType = typeof(Func<,>).MakeGenericType(property.DeclaringType, property.PropertyType);
            var lambda = Expression.Lambda(delegateType, Expression.Property(objParm, property.Name), objParm);
            return lambda.Compile();
        }

        public static Delegate CreateMethod(MethodInfo methodInfo, object target = null)
        {
            List<Type> args = new List<Type>(methodInfo.GetParameters().Select(p => p.ParameterType));
            Type delegateType;
            if (methodInfo.ReturnType == typeof(void))
            {
                delegateType = Expression.GetActionType(args.ToArray());
            }
            else
            {
                args.Add(methodInfo.ReturnType);
                delegateType = Expression.GetFuncType(args.ToArray());
            }
            return (target == null)
               ? Delegate.CreateDelegate(delegateType, methodInfo)
               : Delegate.CreateDelegate(delegateType, target, methodInfo);
        }

        public static string GetKeyName(string className, string memberName)
        {
            return string.Format("{0}-{1}", className, memberName);
        }
    }
}
