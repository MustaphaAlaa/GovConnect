using IServices.IApplicationServices.IServiceCategoryApplications.IRetakeTestApplication;
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
    private readonly IRetakeTestApplicationRetriever _retakeTestApplicationRetriever;
    public RetakeTestApplicationBookingValidator(IBookingRetrieveService bookingRetrieve,
        IRetakeTestApplicationRetriever retakeTestApplication
        , ILogger<RetakeTestApplicationBookingValidator> logger)
    {
        _bookingRetrieve = bookingRetrieve;
        _retakeTestApplicationRetriever = retakeTestApplication;
        _logger = logger;
    }

    public async Task Validate(CreateBookingRequest retakeTestApplication)
    {
        _logger.LogInformation($"{this.GetType().Name} ---- Validate --- Booking by RetakeTestValidation");

        var retakeTest = await _retakeTestApplicationRetriever.GetByAsync(retakeTestApplication =>
        retakeTestApplication.RetakeTestApplicationId == retakeTestApplication.RetakeTestApplicationId);

        if (retakeTest == null)
        {
            _logger.LogError($"Retake test application does not exist");
            throw new DoesNotExistException();
        }

        var booking = await _bookingRetrieve.GetByAsync(booking =>
                            booking.LocalDrivingLicenseApplicationId == retakeTestApplication.LocalDrivingLicenseApplicationId
                            && booking.TestTypeId == retakeTest.TestTypeId);

        if (booking is null)
        {
            string msg = "An appoinment for this Test Type does not booked before";
            _logger.LogError(msg);
            throw new AlreadyExistException(msg);
        }


        var isUsedBefore = await _bookingRetrieve.GetByAsync(booking =>
                            booking.RetakeTestApplicationId == retakeTestApplication.RetakeTestApplicationId);

        if (isUsedBefore != null)
        {
            _logger.LogError($"thie retake test application is used already");
            throw new AlreadyExistException();
        }
    }
}
