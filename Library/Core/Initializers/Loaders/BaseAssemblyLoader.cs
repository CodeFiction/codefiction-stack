using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeFiction.Stack.Library.CoreContracts;

namespace CodeFiction.Stack.Library.Core.Initializers.Loaders
{
    internal abstract class BaseAssemblyLoader : IAssemblyLoader
    {
        protected IEnumerable<Assembly> Assemblies;


        public void Dispose()
        {
            Assemblies = Enumerable.Empty<Assembly>();
        }

        public abstract IEnumerable<Assembly> LoadAssemblies();
    }
}