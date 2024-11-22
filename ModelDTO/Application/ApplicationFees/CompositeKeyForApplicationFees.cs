using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDTO.Application.Fees
{
    public class CompositeKeyForApplicationFees
    {
        public int ApplicationTypeId { get; set; }
        public int ApplicationForId { get; set; }
    }
}
