using CodeFiction.Stack.Library.CoreContracts;
using CastleDynamicProxy = Castle.DynamicProxy;

namespace CodeFiction.Stack.Library.Core.Castle
{
    internal class CastleInterceptorAdapter : CastleDynamicProxy.IInterceptor
    {
        private readonly IInterceptor _interceptor;
        private readonly object _target;

        public CastleInterceptorAdapter(IInterceptor interceptor, object target)
        {
            _interceptor = interceptor;
            _target = target;
        }

        public void Intercept(CastleDynamicProxy.IInvocation invocation)
        {
            
            invocation.ReturnValue = _interceptor.Intercept(new CastleInvocationAdapter(invocation, _target));
        }
    }
}