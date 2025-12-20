using ModelDTO.BookingDTOs;
using Models.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices.IValidators.BookingValidators
{
    public interface IBookingRetrieveService : IAsyncRetrieveService<Booking, BookingDTO>
    {
    }
}
