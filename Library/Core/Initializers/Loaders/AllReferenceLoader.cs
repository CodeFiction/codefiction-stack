using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CfCommerce.Library.Core.Initializers.Loaders
{
    internal class AllReferenceLoader : BaseAssemblyLoader
    {
        public override IEnumerable<Assembly> LoadAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }
    }
}