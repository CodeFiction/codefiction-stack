using System.Web;
using CfCommerce.Presentation.AspNetCore;
using CodeFiction.Stack.Library.Core.Initializers;
using CodeFiction.Stack.Library.Core.Initializers.Loaders;

[assembly: PreApplicationStartMethod(typeof(AspNetApplicationStarter), "Start")]


namespace CfCommerce.Presentation.AspNetCore
{
    /// <summary>
    /// Initializes the application when the Asp.Net Host started.
    /// </summary>
    public class AspNetApplicationStarter
    {
        private static bool _applicationInitialized;
        private static readonly object SyncRoot = new object();

        private static readonly ApplicationLifeManager LifeManager = new ApplicationLifeManager(OnApplicationShutdown, OnApplicationStart);

        private static readonly Bootstrapper Bootstrapper = Bootstrapper.Create();

        /// <summary>
        /// Starts the application. This method executes by the <see cref="PreApplicationStartMethodAttribute"/>.
        /// </summary>
        public static void Start()
        {
            if (_applicationInitialized) return;
            lock (SyncRoot)
            {
                if (_applicationInitialized) return;
                LifeManager.InitializeManager();
                _applicationInitialized = true;
            }
        }

        private static void OnApplicationStart()
        {
            Bootstrapper.StartApplication(AssemblyLoader.AllLoader);
        }

        private static void OnApplicationShutdown()
        {

        }
    }
}
