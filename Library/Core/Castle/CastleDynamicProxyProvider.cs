using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using CodeFiction.Stack.Library.CoreContracts;
using IInterceptor = CodeFiction.Stack.Library.CoreContracts.IInterceptor;

namespace CodeFiction.Stack.Library.Core.Castle
{
    public class CastleDynamicProxyProvider : IDynamicProxyProvider
    {
        private readonly ProxyGenerator _factory = new ProxyGenerator();

        public TProxy Create<TProxy>(IInterceptor interceptor, params Type[] interfaces)
        {
            return (TProxy)Create(typeof(TProxy), interceptor, interfaces);
        }

        public TProxy Create<TProxy>(TProxy target, IInterceptor interceptor, params Type[] interfaces)
        {
            CastleInterceptorAdapter adapter = new CastleInterceptorAdapter(interceptor, target);
            return typeof(TProxy).IsInterface ?
                (TProxy)_factory.CreateInterfaceProxyWithoutTarget(typeof(TProxy), interfaces, adapter) :
                (TProxy)_factory.CreateClassProxy(typeof(TProxy), interfaces, adapter);
        }

        public object Create(Type proxyType, IInterceptor interceptor, params Type[] interfaces)
        {
            CastleInterceptorAdapter adapter = new CastleInterceptorAdapter(interceptor, null);
            return proxyType.IsInterface ?
                _factory.CreateInterfaceProxyWithoutTarget(proxyType, interfaces, adapter) :
                _factory.CreateClassProxy(proxyType, interfaces, adapter);
        }
    }
}
