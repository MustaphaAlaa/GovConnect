using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDTO.ApplicationDTOs.Fees
{
    public record CompositeKeyForServiceFees
    {
        public byte ApplicationPurposeId { get; set; }
        public short ServiceCategoryId { get; set; }
    }
}
