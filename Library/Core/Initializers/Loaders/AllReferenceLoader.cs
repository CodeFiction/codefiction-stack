using System;
using System.Collections.Generic;
using System.Reflection;

namespace CodeFiction.Stack.Library.Core.Initializers.Loaders
{
    internal class AllReferenceLoader : BaseAssemblyLoader
    {
        public override IEnumerable<Assembly> LoadAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }
    }
}