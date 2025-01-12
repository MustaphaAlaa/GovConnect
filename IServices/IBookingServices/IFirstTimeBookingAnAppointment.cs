using ModelDTO.BookingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices.IBookingServices
{
    public interface IFirstTimeBookingAnAppointment
    {
        public Task<bool> IsFirstTime(CreateBookingRequest createBookingRequest);
    }
}
