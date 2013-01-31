// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseException.cs" company="CodeFiction®">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace CodeFiction.Stack.Common.Exceptions
{
    [Serializable]
    public abstract class BaseException : Exception
    {
        protected BaseException(string message = null, Exception innerException = null)
            : base(message, innerException)
        {
        }


        protected BaseException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            /*
            Buraya ilerde Base classa eklenecek olan memberler eklenecek
            Ör:
            info.AddValue("hede", _hede);
            */
        }
    }
}
