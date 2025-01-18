using AutoMapper;
using DataConfigurations.Migrations;
using DataConfigurations.TVFs.ITVFs;
using IRepository;
using IServices.ITests.ILDLApplicationsAllowedToRetakeATestServices;
using IServices.ITests.ITest;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelDTO.TestsDTO;
using Models.Tests;
using Services.Execptions;

namespace Services.LDLApplicationsAllowedToRetakeATestServices;

public class LDLTestRetakeApplicationCreator : ILDLTestRetakeApplicationCreator, IDisposable
{
    private readonly ICreateRepository<LDLApplicationsAllowedToRetakeATest> _createRepository;

    private readonly ILDLTestRetakeApplicationCreationValidator _lDLTestRetakeApplicationCreationValidator;
    private readonly ITVF_GetTestResultForABookingId _tVF_GetTestResultForABookingId;

    private readonly ITestCreationService _testCreationService;
    private readonly ILogger<LDLTestRetakeApplicationCreator> _logger;
    private readonly IMapper _mapper;
    //public LDLTestRetakeApplicationCreator(ICreateRepository<LDLApplicationsAllowedToRetakeATest> createRepository,
    //                ILDLTestRetakeApplicationCreationValidator lDLTestRetakeApplicationCreationValidator,
    //    ITestCreationService testCreationService,
    //    ILogger<LDLTestRetakeApplicationCreator> logger,
    //    ITVF_GetTestResultForABookingId tVF_GetTestResultForABookingId,
    //    IMapper mapper)
    //{
    //    _createRepository = createRepository;
    //    _testCreationService = testCreationService;
    //    _lDLTestRetakeApplicationCreationValidator = lDLTestRetakeApplicationCreationValidator;
    //    _testCreationService.TestCreated += CreateAsync;
    //    _tVF_GetTestResultForABookingId = tVF_GetTestResultForABookingId;
    //    _logger = logger;
    //    _mapper = mapper;

    //}

    IServiceScopeFactory _serviceScopeFactory;
    public LDLTestRetakeApplicationCreator(IServiceScopeFactory serviceScopeFactory,
        ITestCreationService testCreationService,
        ILogger<LDLTestRetakeApplicationCreator> logger,
        ITVF_GetTestResultForABookingId tVF_GetTestResultForABookingId,
        IMapper mapper)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _testCreationService = testCreationService;
        _testCreationService.TestCreated += CreateAsync;
        _tVF_GetTestResultForABookingId = tVF_GetTestResultForABookingId;
        _logger = logger;
        _mapper = mapper;

    }

    public async Task CreateAsync(object? sender, TestDTO e)
    {
        _logger.LogInformation($"{this.GetType().Name} -- CreateAsync");
        _logger.LogDebug($"TestDTO => Local Driving License Application Id: {e.LocalDrivingLicenseApplicationId} -- Test Type Id: {e.TestTypeId} -- Booking id: {e.BookingId}");
        using (var scope = _serviceScopeFactory.CreateScope())
        {

            var validator = scope.ServiceProvider.GetRequiredService<ILDLTestRetakeApplicationCreationValidator>();
            var testResultService = scope.ServiceProvider.GetRequiredService<ITVF_GetTestResultForABookingId>();

            var creation = scope.ServiceProvider.GetRequiredService<ICreateRepository<LDLApplicationsAllowedToRetakeATest>>();

            try
            {
                _logger.LogInformation($"{this.GetType().Name} -- CreateAsync");

                if (e.TestResult)
                {
                    _logger.LogError("cannot be allowed to retake the test because he is already passed it");
                    throw new ValidationExecption("Test Type Is Passed");
                }

                var testDTO = await testResultService.GetTestResultForABookingId(e.BookingId);


                if (testDTO is null)
                {
                    _logger.LogError("booking id doesn't exist in booking table");
                    throw new DoesNotExistException("Booking Id is not exist");
                }
                 
                var isValid = await validator.IsValid(testDTO.LocalDrivingLicenseApplicationId, testDTO.TestTypeId);
                //after this line dbcontext is disposed

                if (!isValid)
                {
                    _logger.LogWarning($"It's not allowed for this LDL Applcation with id {e.LocalDrivingLicenseApplicationId} to retake a test type: {e.TestTypeId}");

                    return;
                }


                var allowedToRetakeATest = new LDLApplicationsAllowedToRetakeATest()
                {
                    // here the 
                    IsAllowedToRetakeATest = true,
                    LocalDrivingLicenseApplicationId = testDTO.LocalDrivingLicenseApplicationId,
                    TestTypeId = testDTO.TestTypeId
                };

                //when i reach here the dbcontext is dispose
                var createdObj = await creation.CreateAsync(allowedToRetakeATest);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
    }

    public void Dispose()
    {
        _testCreationService.TestCreated -= CreateAsync;
    }
}