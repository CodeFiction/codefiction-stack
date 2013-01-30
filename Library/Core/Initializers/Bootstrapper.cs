using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CFCommerce.Library.CoreContracts;
using CFCommerce.Library.CoreContracts.Plugins;
using CfCommerce.Common.Utilities;

namespace CfCommerce.Library.Core.Initializers
{
    /// <summary>
    /// Main bootstrapper that manages the lifecycle of the application.
    /// </summary>
    public class Bootstrapper
    {
        private IEnumerable<Assembly> _loadedAssemblies;
        private readonly IPluginService _pluginService;
        private readonly IDependencyResolver _resolver;

        /// <summary>
        /// Initializes a new instance of <see cref="Bootstrapper"/>.
        /// </summary>
        protected Bootstrapper()
        {
            _resolver = DependencyResolver.Current;
            _pluginService = new DefaultPluginService();
        }

        public static Bootstrapper Create()
        {
            return new Bootstrapper();
        }

        /// <summary>
        /// Starts the application and loads the assemblies with the given assembly loader
        /// </summary>
        public void StartApplication(IAssemblyLoader loader)
        {
            _loadedAssemblies = loader.LoadAssemblies();
            IList<Type> plugins = new List<Type>();

            foreach (var assembly in _loadedAssemblies)
            {
                OnAssembliesLoading(assembly, plugins);
            }

            OnDependencyLoading(_resolver);

            _pluginService.Store(plugins);
        }

        public Bootstrapper RegisterComponent(Action<IDependencyResolver> dependencyAction)
        {
            dependencyAction(DependencyResolver.Current);
            return this;
        }

        protected virtual void OnDependencyLoading(IDependencyResolver resolver)
        {

        }

        /// <summary>
        /// Triggers the terminating actions on all assembly installers.
        /// </summary>
        public void StopApplication()
        {
            OnApplicationTerminating();
        }

        protected virtual void OnApplicationTerminating()
        {
            //TODO: terminating operations of the IInstaller
        }

        protected virtual void OnAssembliesLoading(Assembly assembly, IList<Type> loadedPlugins)
        {
            ReflectionUtils.FindImplementingTypesFromAssembly<IPlugin>(assembly).ToList().ForEach(loadedPlugins.Add);
        }
    }
}
