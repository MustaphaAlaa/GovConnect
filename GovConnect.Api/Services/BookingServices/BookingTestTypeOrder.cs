using AutoMapper;
using IRepository.ITestRepos;
using IServices.IValidators;
using IServices.IValidators.BookingValidators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelDTO.BookingDTOs;
using ModelDTO.TestsDTO;
using Models.Tests.Enums;
using Services.Exceptions;

namespace Services.BookingServices;

public class BookingTestTypeOrder : ITestTypeOrder
{

    private readonly ILogger<BookingTestTypeOrder> _logger;

    private readonly ITestResultInfoRetrieve _testResultInfoRetrieve;

    public BookingTestTypeOrder(ITestResultInfoRetrieve testResultInfoRetrieve,
        ILogger<BookingTestTypeOrder> logger)
    {
        _testResultInfoRetrieve = testResultInfoRetrieve;
        _logger = logger;
    }

    public async Task CheckTheOrder(CreateBookingRequest request)
    {
        _logger.LogInformation($"{GetType().Name} -- CheckTheOrder");

        BookingDTO? booking;
        TestResultInfo? testResultInfo;
        switch ((EnTestTypes)request.TestTypeId)
        {
            case EnTestTypes.Vision:
                return;
            case EnTestTypes.Written_Theory:

                testResultInfo = await _testResultInfoRetrieve
                    .GetTestResultInfo(request.LocalDrivingLicenseApplicationId, (int)EnTestTypes.Vision)
                    .FirstOrDefaultAsync(t => t.Test.TestResult == true);

                if (testResultInfo == null || !testResultInfo.Test.TestResult)
                {
                    throw new InvalidOrderException($"Invalid Order {EnTestTypes.Vision.ToString()} must be before it.");
                }

                break;
            case EnTestTypes.Practical_Street:


                testResultInfo = await _testResultInfoRetrieve
                    .GetTestResultInfo(request.LocalDrivingLicenseApplicationId, (int)EnTestTypes.Written_Theory)
                   .FirstOrDefaultAsync(t => t.Test.TestResult == true);


                if (testResultInfo == null || !testResultInfo.Test.TestResult)
                {
                    throw new InvalidOrderException($"Invalid Order {EnTestTypes.Vision.ToString()} must be before it.");
                }

                break;
            default:
                throw new DoesNotExistException();
        }
    }
}
