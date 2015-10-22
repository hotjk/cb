using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CB
{
    /// <summary>
    /// Signals that a method has been invoked at an illegal or
    /// inappropriate time.
    /// </summary>
    public class IllegalStateException : AkkaException
    {
        public IllegalStateException(string message) : base(message)
        {

        }

        protected IllegalStateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

