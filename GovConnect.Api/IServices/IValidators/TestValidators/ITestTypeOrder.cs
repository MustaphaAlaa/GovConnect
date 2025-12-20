using ModelDTO.BookingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices.IValidators
{
    public interface ITestTypeOrder
    {
        Task CheckTheOrder(CreateBookingRequest request);
    }
}
