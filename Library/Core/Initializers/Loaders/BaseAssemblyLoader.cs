using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CFCommerce.Library.CoreContracts;

namespace CfCommerce.Library.Core.Initializers.Loaders
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