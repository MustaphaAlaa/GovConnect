using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Execptions
{
    public class DoseNotExistException : Exception
    {
        public DoseNotExistException()
        {
        }

        public DoseNotExistException(string? message) : base(message)
        {
        }

        public DoseNotExistException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
