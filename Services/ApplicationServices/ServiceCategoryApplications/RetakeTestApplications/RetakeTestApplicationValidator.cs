using IServices.IValidators;
using IServices.IValidators.BookingValidators;
using Microsoft.Extensions.Logging;
using ModelDTO.BookingDTOs;
using Services.Exceptions;
namespace Services.ApplicationServices.ServiceCategoryApplications;

public class RetakeTestApplicationValidator : IRetakeTestApplicationValidator
{
    private readonly IBookingRetrieveService _bookingRetrieve;
    private readonly ILogger<RetakeTestApplicationValidator> _logger;
    public RetakeTestApplicationValidator(IBookingRetrieveService bookingRetrieve, ILogger<RetakeTestApplicationValidator> logger)
    {
        _bookingRetrieve = bookingRetrieve;
        _logger = logger;
    }

    public async Task Validate(CreateBookingRequest retakeTestApplication)
    {
        var booking = await _bookingRetrieve.GetByAsync(booking => booking.RetakeTestApplicationId == retakeTestApplication.RetakeTestApplicationId);

        if (booking != null)
        {
            _logger.LogError($"thie retake test application is used already");
            throw new AlreadyExistException();
        }
    }
}
