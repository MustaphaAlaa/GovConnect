using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Services.Execptions
{
    internal class ValidationExecption : Exception
    {
        public ValidationExecption()
        {
        }

        public ValidationExecption(string? message) : base(message)
        {
        }

        public ValidationExecption(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
