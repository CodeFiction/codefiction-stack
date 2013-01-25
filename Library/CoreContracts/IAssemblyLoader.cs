using System;
using System.Collections.Generic;
using System.Reflection;

namespace CFCommerce.Library.CoreContracts
{
    public interface IAssemblyLoader : IDisposable
    {
        IEnumerable<Assembly> LoadAssemblies();
    }
}
