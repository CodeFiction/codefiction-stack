using System;
using System.Linq;
using System.Collections.Generic;
using CodeFiction.Stack.Library.CoreContracts.Plugins;

namespace CodeFiction.Stack.Library.Core.Initializers
{
    internal class DefaultPluginService : IPluginService
    {
        private readonly Dictionary<string, IPlugin> _plugins = new Dictionary<string, IPlugin>();

        public void Store(IEnumerable<Type> pluginTypes)
        {
            foreach (IPlugin plugin in pluginTypes.Select(pluginType => DependencyResolver.Current.CreateInstanceOfType<IPlugin>(pluginType)))
            {
                _plugins[plugin.Name] = plugin;
            }
        }
    }
}
