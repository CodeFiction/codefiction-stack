using System;
using System.Collections.Generic;

namespace CodeFiction.Stack.Library.CoreContracts.Plugins
{
    public interface IPluginService
    {
        void Store(IEnumerable<Type> pluginTypes);
    }
}
