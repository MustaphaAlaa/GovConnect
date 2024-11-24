using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDTO.Application.Fees
{
    public record CompositeKeyForApplicationFees
    {
        public byte ApplicationTypeId { get; set; }
        public short ApplicationForId { get; set; }
    }
}
