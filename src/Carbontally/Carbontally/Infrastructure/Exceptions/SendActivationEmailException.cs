using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Carbontally
{
    [Serializable()]
    public class SendActivationEmailException : Exception
    {
        public SendActivationEmailException() 
            : base() 
        { }

        public SendActivationEmailException(string message)
            : base(message)
        { }

        public SendActivationEmailException(string message, Exception innerException) : 
            base(message, innerException)
        { }

        protected SendActivationEmailException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}