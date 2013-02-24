using System;
using System.Reflection;
using Castle.DynamicProxy;
using CodeFiction.Stack.Library.CoreContracts;

namespace CodeFiction.Stack.Library.Core.Aspect
{
    public sealed class MethodInvocationContext : IMethodInvocationContext
    {
        private readonly IInvocation _invocation;

        internal MethodInvocationContext(IInvocation invocation)
        {
            _invocation = invocation;
        }

        private MethodInvocationContext()
        {
        }

        #region IMethodInvocationContext Members

        public MethodInfo Method
        {
            get { return _invocation.Method; }
        }

        public object[] Args
        {
            get { return _invocation.Arguments; }
        }

        public Type[] GenericArguments
        {
            get { return _invocation.GenericArguments; }
        }

        public bool Cancel { get; set; }

        public object ReturnValue
        {
            get { return _invocation.ReturnValue; }
            set { _invocation.ReturnValue = value; }
        }

        public object Proxy
        {
            get { return _invocation.InvocationTarget; }
        }

        public Type ProxyType
        {
            get { return _invocation.InvocationTarget.GetType(); }
        }

        public object Proceed()
        {
            if (IsMethodExecuted)
            {
                return ReturnValue;
            }

            _invocation.Proceed();
            return _invocation.ReturnValue;
        }


        public Exception Exception { get; set; }

        public bool IsMethodExecuted { get; set; }

        #endregion
    }
}
