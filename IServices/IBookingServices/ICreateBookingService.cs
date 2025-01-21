using ModelDTO.BookingDTOs;

namespace IServices.IBookingServices;

public interface ICreateBookingService : ICreateService<CreateBookingRequest, BookingDTO?>
{
    event Func<object, BookingDTO, Task> AppointmentIsBooked;
}
