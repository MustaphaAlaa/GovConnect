using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    public class InvalidOrderException : System.Exception
    {
        public InvalidOrderException()
        {
        }

        public InvalidOrderException(string? message) : base(message)
        {
        }

        public InvalidOrderException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }
    }
}
