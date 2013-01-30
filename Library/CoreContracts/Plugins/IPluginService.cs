using System;
using System.Collections.Generic;

namespace CFCommerce.Library.CoreContracts.Plugins
{
    public interface IPluginService
    {
        void Store(IEnumerable<Type> pluginTypes);
    }
}
