using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CfCommerce.Common.ExceptionHandling.Managers;

namespace CfCommerce.Common.ExceptionHandling
{
    public class ExceptionManager
    {
        private static readonly Lazy<ICfExceptionManager> _current = new Lazy<ICfExceptionManager>(() => ExceptionManagerSelector.GetManager(ExceptionManagers.EnterpriseLibrary), true);

        public static ICfExceptionManager Current { get { return _current.Value; } }

        internal static class ExceptionManagerSelector
        {
            public static ICfExceptionManager GetManager(ExceptionManagers manager)
            {
                switch (manager)
                {
                    case ExceptionManagers.EnterpriseLibrary:
                        return new EntLibExceptionHandlingManager();
                }

                // TODO : throw libraray specific Exception
                throw new InvalidOperationException("");
            }
        }
    }
}
