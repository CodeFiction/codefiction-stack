using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using CodeFiction.Stack.Common.Utilities;
using CodeFiction.Stack.Library.Core.Aspect;
using CodeFiction.Stack.Library.Core.Aspect.Attributes;
using CodeFiction.Stack.Library.Core.Castle.Common;
using CodeFiction.Stack.Library.CoreContracts;
using IInterceptor = Castle.DynamicProxy.IInterceptor;

namespace CodeFiction.Stack.Library.Core.Castle
{
    public class StrategyInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            OnMethodExecuting(invocation);
        }

        protected virtual void OnMethodExecuting(IInvocation invocation)
        {
            var attributes = AttributesHelper.GetAttributes(invocation, typeof(AspectAttributeBase)).Cast<AspectAttributeBase>().
                    OrderBy(i => i.Order);
            IMethodInvocationContext context = new MethodInvocationContext(invocation);
            try
            {
                var cancelExecution = IterateAttributesAndReturnTrueIfCancel(context, attributes, ExecutionOrder.Before);
                if (!cancelExecution)
                {
                    invocation.Proceed();
                }
                IterateAttributesAndReturnTrueIfCancel(context, attributes, ExecutionOrder.After);
            }
            catch (Exception ex)
            {
                IterateAttributesAndReturnTrueIfCancel(context, attributes, ExecutionOrder.Exception, ex);
            }
        }

        private bool IterateAttributesAndReturnTrueIfCancel(IMethodInvocationContext context,
                                                            IEnumerable<AspectAttributeBase> attributes,
                                                            ExecutionOrder order, Exception ex = null)
        {
            var methods = GetAllMethodsInType(context, attributes, order);
            context.Exception = ex;
            bool cancelExecution = false;
            foreach (var method in methods)
            {
                Delegate @delegate = DelegateUtils.GetMethod(method, context.Proxy);
                @delegate.DynamicInvoke(new object[] {context});
               // method.Invoke(Activator.CreateInstance(method.ReflectedType), new object[] { context });
                cancelExecution = context.Cancel;
            }
            return cancelExecution;
        }

        private IEnumerable<MethodInfo> GetAllMethodsInType(IMethodInvocationContext context,
                                                     IEnumerable<AspectAttributeBase> attributes, ExecutionOrder order)
        {
            string key = string.Format("{0}:{1}:{2}", context.ProxyType.Name, context.Method.Name, order);

            Type attributeExecutionOrderType;
            switch (order)
            {
                default:
                case ExecutionOrder.Before:
                    attributeExecutionOrderType = typeof(WorksBeforeAttribute);
                    break;
                case ExecutionOrder.After:
                    attributeExecutionOrderType = typeof(WorksAfterAttribute);
                    break;
                case ExecutionOrder.Exception:
                    attributeExecutionOrderType = typeof(WorksOnExceptionAttribute);
                    break;
            }
            return attributes.SelectMany(attribute => attribute.GetType().GetMethods()
                .Where(m => m.GetCustomAttributes(attributeExecutionOrderType, true).Length > 0).ToList())
                .ToList();
        }
    }
}
