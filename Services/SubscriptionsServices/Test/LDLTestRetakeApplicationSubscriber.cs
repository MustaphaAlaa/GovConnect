using AutoMapper;
using DataConfigurations.TVFs.ITVFs;
using IRepository.ITestRepos;
using IServices.IApplicationServices.IServiceCategoryApplications.IRetakeTestApplication;
using IServices.ILDLApplicationsAllowedToRetakeATestServices;
using IServices.ITests.ITest;
using IServices.IValidators.BookingValidators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelDTO.TestsDTO;

namespace Services.SubscriptionsServices.Tests;

public class LDLTestRetakeApplicationSubscriber : ILDLTestRetakeApplicationSubscriber
{

    private readonly ITestCreationService _testCreationService;
    private readonly IRetakeTestApplicationCreation _retakeTestApplicationCreation;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    private readonly ILogger<LDLTestRetakeApplicationSubscriber> _logger;
    private readonly IMapper _mapper;



    // Constructor
    public LDLTestRetakeApplicationSubscriber(
    ITestCreationService testCreationService,
    IRetakeTestApplicationCreation retakeTestApplicationCreation,
    IServiceScopeFactory serviceScopeFactory,
        ILogger<LDLTestRetakeApplicationSubscriber> logger,
     IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
        _testCreationService = testCreationService;
        _serviceScopeFactory = serviceScopeFactory;
        _retakeTestApplicationCreation = retakeTestApplicationCreation;

        _testCreationService.TestCreated += OnCreation;
        _retakeTestApplicationCreation.RetakeTestApplicationCreated += OnUpdate;
    }

    private async Task OnCreation(object? arg1, TestDTO testDTO)
    {
        try
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {

                var testResultInfoRetrieve = scope.ServiceProvider.GetRequiredService<ITestResultInfoRetrieve>();
                var bookingRetrieve = scope.ServiceProvider.GetRequiredService<IBookingRetrieveService>();

                //Booking's TestTypeID
                var booking = await bookingRetrieve.GetByAsync(b => b.BookingId
                                    == testDTO.BookingId);


                testDTO.TestTypeId = booking.TestTypeId;
                testDTO.LocalDrivingLicenseApplicationId = booking.LocalDrivingLicenseApplicationId;

                var isFirstTime = await testResultInfoRetrieve.GetTestResultInfo(testDTO.LocalDrivingLicenseApplicationId, testDTO.TestTypeId)
                     .AnyAsync(tr => tr.Booking.TestTypeId == booking.TestTypeId
                     && tr.Booking.LocalDrivingLicenseApplicationId == booking.LocalDrivingLicenseApplicationId);


                if (isFirstTime)
                {
                    var createLDLTestAllowed = scope.ServiceProvider.GetRequiredService<ILDLTestRetakeApplicationCreator>();
                    await createLDLTestAllowed.CreateAsync(testDTO);
                    return;
                }


                var updateLDLTestAllowed = scope.ServiceProvider.GetRequiredService<ILDLTestRetakeApplicationUpdater>();

                LDLApplicationsAllowedToRetakeATestDTO allowedToRetakeATestDTO = new()
                {
                    IsAllowedToRetakeATest = testDTO.TestResult,
                    LocalDrivingLicenseApplicationId = testDTO.LocalDrivingLicenseApplicationId,
                    TestTypeId = testDTO.TestTypeId

                };

                var updatedObj = await updateLDLTestAllowed.UpdateAsync(allowedToRetakeATestDTO);

            }


            // await OnUpdate(arg1, testDTO);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }

    }


    private async Task OnUpdate(object? arg1, TestDTO testDTO)
    {
        try
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {

                var bookingRetrieve = scope.ServiceProvider.GetRequiredService<IBookingRetrieveService>();

                //Booking's TestTypeID


                /*!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                 * !!!!!!!!!!!!!!!!!!!!!!!!!!!!
                 * !!!!!!!!!!!!!!!!!!!
                
                 * For Not it's only retake test application who uses this method so i'am sure there's a ldl id and test type id
                */

                //testDTO.TestTypeId = booking.TestTypeId;
                //testDTO.LocalDrivingLicenseApplicationId = booking.LocalDrivingLicenseApplicationId;

                var updateLDLTestAllowed = scope.ServiceProvider.GetRequiredService<ILDLTestRetakeApplicationUpdater>();

                LDLApplicationsAllowedToRetakeATestDTO allowedToRetakeATestDTO = new()
                {
                    IsAllowedToRetakeATest = testDTO.TestResult,
                    LocalDrivingLicenseApplicationId = testDTO.LocalDrivingLicenseApplicationId,
                    TestTypeId = testDTO.TestTypeId

                };

                var updatedObj = await updateLDLTestAllowed.UpdateAsync(allowedToRetakeATestDTO);
                return;
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }

    }
}
