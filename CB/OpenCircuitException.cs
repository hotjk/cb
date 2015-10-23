using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CB
{
    /// <summary>
    /// Exception throws when CircuitBreaker is open
    /// </summary>
    public class OpenCircuitException : Exception
    {
        public OpenCircuitException() : base("Circuit Breaker is open; calls are failing fast") { }

        public OpenCircuitException(string message)
            : base(message)
        {
        }

        public OpenCircuitException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
