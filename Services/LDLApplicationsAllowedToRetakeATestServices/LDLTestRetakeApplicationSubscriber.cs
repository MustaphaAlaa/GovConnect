using AutoMapper;
using DataConfigurations.TVFs.ITVFs;
using IRepository.ITestRepos;
using IServices.ILDLApplicationsAllowedToRetakeATestServices;
using IServices.ITests.ITest;
using IServices.IValidators.BookingValidators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelDTO.TestsDTO;

namespace GovConnect.Services.LDLApplicationsAllowedToRetakeATestServices;
public class LDLTestRetakeApplicationSubscriber : ILDLTestRetakeApplicationSubscriber
{

    private readonly ITestCreationService _testCreationService;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    private readonly ILogger<LDLTestRetakeApplicationSubscriber> _logger;
    private readonly IMapper _mapper;



    // Constructor
    public LDLTestRetakeApplicationSubscriber(ILogger<LDLTestRetakeApplicationSubscriber> logger,
     IMapper mapper,
    ITestCreationService testCreationService, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _mapper = mapper;
        _testCreationService = testCreationService;
        _serviceScopeFactory = serviceScopeFactory;

        _testCreationService.TestCreated += OnTestCreation;
    }

    private async Task OnTestCreation(object? arg1, TestDTO testDTO)
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
