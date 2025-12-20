using IServices.IValidators.BookingValidators;
using ModelDTO.BookingDTOs;

namespace IServices.IBookingServices;

/// <summary>
/// Interface for creating booking services.
/// </summary>
public interface ICreateBookingService : ICreateService<CreateBookingRequest, BookingDTO?>
{
    /// <summary>
    /// Event triggered when an appointment is booked.
    /// </summary>
    event Func<object, BookingDTO, Task> AppointmentIsBooked;


}

