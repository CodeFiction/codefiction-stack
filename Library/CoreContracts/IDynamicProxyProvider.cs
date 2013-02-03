using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFiction.Stack.Library.CoreContracts
{
    public interface IDynamicProxyProvider
    {
        TProxy Create<TProxy>(IInterceptor interceptor, params Type[] interfaces);

        TProxy Create<TProxy>(TProxy target, IInterceptor interceptor, params Type[] interfaces);

        object Create(Type proxyType, IInterceptor interceptor, params Type[] interfaces);
    }
}
