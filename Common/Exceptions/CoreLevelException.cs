using System;
using System.Runtime.Serialization;

namespace CodeFiction.Stack.Common.Exceptions
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
