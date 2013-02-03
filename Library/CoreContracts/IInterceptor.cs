using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFiction.Stack.Library.CoreContracts
{
    public interface IInterceptor
    {
        object Intercept(IMethodInvocation methodInvocation);
    }
}
