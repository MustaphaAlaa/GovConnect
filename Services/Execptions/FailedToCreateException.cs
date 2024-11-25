using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Execptions
{
    public class FailedToCreateException : Exception
    {
        public FailedToCreateException()
        {
        }

        public FailedToCreateException(string? message) : base(message)
        {
        }

        public FailedToCreateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
