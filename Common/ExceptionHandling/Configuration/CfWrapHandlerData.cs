using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfCommerce.Common.ExceptionHandling.Configuration
{
    internal class CfWrapHandlerData : BaseHandlerData
    {
        public string ExceptionMessage { get; set; }
        public Type ExceptionType { get; set; }
    }
}
