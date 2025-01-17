using AutoMapper;
using DataConfigurations.TVFs.ITVFs;
using IRepository;
using IServices.ITests.ILDLApplicationsAllowedToRetakeATestServices;
using IServices.ITests.ITest;
using Microsoft.Extensions.Logging;
using ModelDTO.TestsDTO;
using Models.Tests;
using Services.Execptions;

namespace Services.LDLApplicationsAllowedToRetakeATestServices;

public class LDLTestRetakeApplicationCreator : ILDLTestRetakeApplicationCreator
{
    private readonly ICreateRepository<LDLApplicationsAllowedToRetakeATest> _createRepository;
    private readonly ILDLTestRetakeApplicationCreationValidator _lDLTestRetakeApplicationCreationValidator;
    private readonly ITVF_GetTestResultForABookingId _tVF_GetTestResultForABookingId;
    private readonly ITestCreationService _testCreationService;
    private readonly ILogger<LDLTestRetakeApplicationCreator> _logger;
    private readonly IMapper _mapper;
    public LDLTestRetakeApplicationCreator(ICreateRepository<LDLApplicationsAllowedToRetakeATest> createRepository,
                    ILDLTestRetakeApplicationCreationValidator lDLTestRetakeApplicationCreationValidator,
        ITestCreationService testCreationService,
        ILogger<LDLTestRetakeApplicationCreator> logger,
        ITVF_GetTestResultForABookingId tVF_GetTestResultForABookingId,
        IMapper mapper)
    {
        _createRepository = createRepository;
        _testCreationService = testCreationService;
        _lDLTestRetakeApplicationCreationValidator = lDLTestRetakeApplicationCreationValidator;
        _testCreationService.TestCreated += CreateAsync;
        _tVF_GetTestResultForABookingId = tVF_GetTestResultForABookingId;
        _logger = logger;
        _mapper = mapper;

    }

    public async Task CreateAsync(object? sender, TestDTO e)
    {
        try
        {
            _logger.LogInformation($"{this.GetType().Name} -- CreateAsync");

            if (e.TestResult)
            {
                _logger.LogWarning($"It's not allowed for this LDL Applcation with id {e.LocalDrivingLicenseApplicationId} to retake a test type: {e.TestTypeId}");
                return;

            }

            var testDTO = await _tVF_GetTestResultForABookingId.GetTestResultForABookingId(e.BookingId);


            if (testDTO is null)
            {
                _logger.LogError("booking id doesn't exist in booking table");
                throw new DoesNotExistException("Booking Id is not exist");
            }

            var isValid = await _lDLTestRetakeApplicationCreationValidator.IsValid(testDTO.LocalDrivingLicenseApplicationId, testDTO.TestTypeId);
            //after this line dbcontext is disposed

            if (!isValid)
            {
                _logger.LogWarning($"It's not allowed for this LDL Applcation with id {e.LocalDrivingLicenseApplicationId} to retake a test type: {e.TestTypeId}");

                return;
            }


            var allowedToRetakeATest = new LDLApplicationsAllowedToRetakeATest()
            {

                IsAllowedToRetakeATest = true,
                LocalDrivingLicenseApplicationId = e.LocalDrivingLicenseApplicationId,
                TestTypeId = e.TestTypeId
            };

            //when i reach here the dbcontext is dispose
            var createdObj = await _createRepository.CreateAsync(allowedToRetakeATest);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }

    }


}
