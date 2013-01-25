using System.Collections.Generic;
using System.Reflection;
using CFCommerce.Library.CoreContracts;

namespace CfCommerce.Library.Core.Initializers
{
    /// <summary>
    /// Main bootstrapper that manages the lifecycle of the application.
    /// </summary>
    public class CommerceBootstrapper
    {
        private IEnumerable<Assembly> _loadedAssemblies;

        /// <summary>
        /// Starts the application and loads the assemblies with the given assembly loader
        /// </summary>
        public void StartApplication(IAssemblyLoader loader)
        {
            _loadedAssemblies = loader.LoadAssemblies();

            foreach (var assembly in _loadedAssemblies)
            {
                OnAssembliesLoading(assembly);
            }
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

        protected virtual void OnAssembliesLoading(Assembly assembly)
        {
            //TODO: loads the IModule from assembly. (Or IInstaller)
        }
    }
}
