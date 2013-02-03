using System;
using System.Reflection;

namespace CodeFiction.Stack.Library.CoreContracts
{
    public interface IMethodInvocation
    {
        object Proxy { get; }

        object Target { get; }

        MethodInfo Method { get; }

        object[] Arguments { get; }

        Type[] GenericArguments { get; }
    }
}
