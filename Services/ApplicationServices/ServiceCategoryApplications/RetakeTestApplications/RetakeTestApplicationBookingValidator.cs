using IServices.IValidators;
using IServices.IValidators.BookingValidators;
using Microsoft.Extensions.Logging;
using ModelDTO.BookingDTOs;
using Services.Exceptions;
namespace Services.ApplicationServices.ServiceCategoryApplications;

public class RetakeTestApplicationBookingValidator : IRetakeTestApplicationBookingValidator
{
    private readonly IBookingRetrieveService _bookingRetrieve;
    private readonly ILogger<RetakeTestApplicationBookingValidator> _logger;
    public RetakeTestApplicationBookingValidator(IBookingRetrieveService bookingRetrieve, ILogger<RetakeTestApplicationBookingValidator> logger)
    {
        _bookingRetrieve = bookingRetrieve;
        _logger = logger;
    }

    public async Task Validate(CreateBookingRequest retakeTestApplication)
    {
        _logger.LogInformation($"{this.GetType().Name} ---- Validate --- Booking by RetakeTestValidation");

        var booking = await _bookingRetrieve.GetByAsync(booking => booking.RetakeTestApplicationId == retakeTestApplication.RetakeTestApplicationId);

        if (booking != null)
        {
            _logger.LogError($"thie retake test application is used already");
            throw new AlreadyExistException();
        }
    }
}
