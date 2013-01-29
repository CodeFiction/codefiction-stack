using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfCommerce.Common.Configuration.Fluent
{
    /// <summary>
    /// Interface that is used to build fluent interfaces and hides methods declared by <see cref="object"/> from IntelliSense.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IFluentInterface
    {
    }
}
