using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfCommerce.Common.ExceptionHandling
{
    public enum ExceptionManagers : byte
    {
        EnterpriseLibrary = 0
    }

    public enum PostHandlingAction : byte
    {
        None = 0,
        ThrowNewException = 1,
        NotifyRethrow =2
    }
}
