using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Project1_App.App.Exceptions
{
    [Serializable]
    public class UnexpectedServerBehaviorException : Exception
    {
        public UnexpectedServerBehaviorException()
        {
        }

        public UnexpectedServerBehaviorException(string? message) : base(message)
        {
        }

        public UnexpectedServerBehaviorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnexpectedServerBehaviorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

