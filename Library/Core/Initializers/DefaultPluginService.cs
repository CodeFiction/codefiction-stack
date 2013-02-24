using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using CodeFiction.Stack.Library.Core.Extensions;
using CodeFiction.Stack.Library.CoreContracts.Plugins;

namespace CodeFiction.Stack.Library.Core.Initializers
{
    internal class DefaultPluginService : IPluginService
    {
        private readonly Dictionary<string, IPlugin> _plugins = new Dictionary<string, IPlugin>();

        public void Store(IEnumerable<Type> pluginTypes)
        {
            foreach (var pluginType in pluginTypes)
            {
                try
                {
                    var plugin = DependencyResolver.Current.CreateInstanceOfType<IPlugin>(pluginType);
                    _plugins[plugin.Name] = plugin;
                }
                catch (Exception ex)
                {
                    Debug.Write("'{0}' plugin cannot be constructed for following reason \r\n'{1}'.".FormatText(pluginType.FullName, ex.Message));
                }
            }
        }
    }
}
