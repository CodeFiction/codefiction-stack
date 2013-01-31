using System.Reflection;
using CodeFiction.Stack.Library.CoreContracts;

namespace CodeFiction.Stack.Library.Core.Initializers.Loaders
{
    /// <summary>
    /// Helper class for creating an assembly loader.
    /// </summary>
    public static class AssemblyLoader
    {
        /// <summary>
        /// Assembly loader that locates all the referenced assemblies for the project.
        /// </summary>
        public static IAssemblyLoader AllLoader
        {
            get { return new AllReferenceLoader(); }
        }

        /// <summary>
        /// Loads the given assemblies.
        /// </summary>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IAssemblyLoader FromAssembly(params Assembly[] assemblies)
        {
            return new SpesificAssemblies(assemblies);
        }
    }
}
