using AutoMapper;
using IRepository;
using IServices.ITests.ILDLApplicationsAllowedToRetakeATestServices;
using IServices.ITests.ITest;
using Microsoft.Extensions.Logging;
using ModelDTO.TestsDTO;
using Models.Tests;

namespace Services.LDLApplicationsAllowedToRetakeATestServices;

public class LDLTestRetakeApplicationCreator : LDLTestRetakeApplicationCreatorBase
{
    private readonly ICreateRepository<LDLApplicationsAllowedToRetakeATest> _createRepository;
    private readonly ILDLTestRetakeApplicationCreationValidator _lDLTestRetakeApplicationCreationValidator;
    private readonly ITestCreationService _testCreationService;
    private readonly ILogger<LDLTestRetakeApplicationCreator> _logger;
    private readonly IMapper _mapper;
    public LDLTestRetakeApplicationCreator(ICreateRepository<LDLApplicationsAllowedToRetakeATest> createRepository,
                    ILDLTestRetakeApplicationCreationValidator lDLTestRetakeApplicationCreationValidator,
        ITestCreationService testCreationService,
        ILogger<LDLTestRetakeApplicationCreator> logger,
        IMapper mapper)
    {
        _createRepository = createRepository;
        _testCreationService = testCreationService;
        _lDLTestRetakeApplicationCreationValidator = lDLTestRetakeApplicationCreationValidator;
        _logger = logger;
        _mapper = mapper;
        _testCreationService.TestCreated += CreateAsync;

    }

    protected override async Task CreateAsync(object? sender, TestDTO e)
    {
        _logger.LogInformation($"{this.GetType().Name} -- CreateAsync");

        if (e.TestResult)
        {
            _logger.LogWarning($"It's not allowed for this LDL Applcation with id {e.LocalDrivingLicenseApplicationId} to retake a test type: {e.TestTypeId}");
            return;

        }

        var isValid = await _lDLTestRetakeApplicationCreationValidator.IsValid(e.LocalDrivingLicenseApplicationId, e.TestTypeId);

        if (!isValid)
        {
            _logger.LogWarning($"It's not allowed for this LDL Applcation with id {e.LocalDrivingLicenseApplicationId} to retake a test type: {e.TestTypeId}");

            return;
        }

        try
        {

            var allowedToRetakeATest = new LDLApplicationsAllowedToRetakeATest()
            {

                IsAllowedToRetakeATest = true,
                LocalDrivingApplicationId = e.LocalDrivingLicenseApplicationId,
                TestTypeId = e.TestTypeId
            };

            var createdObj = await _createRepository.CreateAsync(allowedToRetakeATest);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }

    }
}
