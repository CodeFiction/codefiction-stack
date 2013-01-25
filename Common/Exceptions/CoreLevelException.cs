using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CfCommerce.Common.Exceptions
{
    [Serializable]
    public abstract class CoreLevelException : BaseException
    {
        protected CoreLevelException(string message = null, Exception innerException = null)
            : base(message, innerException)
        {
        }


        protected CoreLevelException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}
