using System.Collections.Generic;
using System.Reflection;

namespace CodeFiction.Stack.Library.Core.Initializers.Loaders
{
    internal class SpesificAssemblies : BaseAssemblyLoader
    {
        public SpesificAssemblies(IEnumerable<Assembly> assemblies)
        {
            Assemblies = assemblies;
        }

        public override IEnumerable<Assembly> LoadAssemblies()
        {
            return Assemblies;
        }
    }
}