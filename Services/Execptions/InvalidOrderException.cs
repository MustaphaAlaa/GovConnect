using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Execptions
{
    public class InvalidOrderException : Exception
    {
        public InvalidOrderException()
        {
        }

        public InvalidOrderException(string? message) : base(message)
        {
        }

        public InvalidOrderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
