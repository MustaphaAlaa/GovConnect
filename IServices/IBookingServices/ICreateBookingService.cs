using ModelDTO.BookingDTOs;

namespace IServices.IBookingServices;

public interface ICreateBookingService : ICreateService<CreateBookingRequest, BookingDTO?>
{

}
