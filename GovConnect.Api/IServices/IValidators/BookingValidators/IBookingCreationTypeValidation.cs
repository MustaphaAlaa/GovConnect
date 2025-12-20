using ModelDTO.BookingDTOs;

namespace IServices.IValidators.BookingValidators;

public interface IBookingCreationTypeValidation
{
    public Task Validate(CreateBookingRequest createBookingRequest);

}