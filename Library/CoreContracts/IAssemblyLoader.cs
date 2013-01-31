using System;
using System.Collections.Generic;
using System.Reflection;

namespace CodeFiction.Stack.Library.CoreContracts
{
    public interface IAssemblyLoader : IDisposable
    {
        IEnumerable<Assembly> LoadAssemblies();
    }
}
