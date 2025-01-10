using IServices.IBookingServices;
using ModelDTO.BookingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BookingServices;

public class CreateBookingService : ICreateBookingService
{
    public Task<BookingDTO> CreateAsync(CreateBookingRequest entity)
    {



        throw new NotImplementedException();
    }
}
