using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Execptions
{
    public class FailedToUpdateException : Exception
    {
        public FailedToUpdateException()
        {
        }

        public FailedToUpdateException(string? message) : base(message)
        {
        }

        public FailedToUpdateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
