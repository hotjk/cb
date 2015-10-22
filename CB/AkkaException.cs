using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CB
{
    /// <summary>
    /// This exception provides the base for all Akka.NET specific exceptions within the system.
    /// </summary>
    public abstract class AkkaException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AkkaException"/> class.
        /// </summary>
        protected AkkaException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AkkaException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="cause">The exception that is the cause of the current exception.</param>
        protected AkkaException(string message, Exception cause = null)
            : base(message, cause)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AkkaException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        protected AkkaException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// The exception that is the cause of the current exception.
        /// </summary>
        protected Exception Cause { get { return InnerException; } }
    }
}