using System;
using System.Diagnostics;
using System.Web.Hosting;
using CodeFiction.Stack.Library.Core.Extensions;

namespace CfCommerce.Presentation.AspNetCore
{
    /// <summary>
    /// Notifies when the application is started or stops.
    /// </summary>
    public class ApplicationLifeManager : IRegisteredObject
    {
        private readonly Action _onApplicationInitialize;
        private readonly Action _onApplicationShutdown;

        public ApplicationLifeManager(Action onStop)
            : this(onStop, () => { })
        {
        }

        public ApplicationLifeManager(Action onStop, Action onStart)
        {
            _onApplicationInitialize = onStart;
            _onApplicationShutdown = onStop;
        }

        public void Stop(bool immediate)
        {
            try
            {
                _onApplicationShutdown();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Application stop action failed: '{0}'.".FormatText(e.Message));
            }
            finally
            {
                HostingEnvironment.UnregisterObject(this);
            }
        }

        public void InitializeManager()
        {
            try
            {
                HostingEnvironment.RegisterObject(this);
                _onApplicationInitialize();
            }
            catch (Exception e)
            {
                //_log.Error("AspNet Host start failed.", e);
                Debug.WriteLine("Aspnet host start failed: '{0}'.".FormatText(e.Message));
            }
        }
    }
}
