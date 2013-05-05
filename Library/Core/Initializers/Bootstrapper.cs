using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeFiction.Stack.Common.Utilities;
using CodeFiction.Stack.Library.CoreContracts;
using CodeFiction.Stack.Library.CoreContracts.Plugins;

namespace CodeFiction.Stack.Library.Core.Initializers
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
            _resolver = DependencyResolverActivator.Current;
            _pluginService = new DefaultPluginService();
        }

        public IDependencyResolver DependencyResolver { get { return _resolver; } }

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
            dependencyAction(DependencyResolverActivator.Current);
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
