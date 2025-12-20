using AutoMapper;
using DataConfigurations.Migrations;
using DataConfigurations.TVFs.ITVFs;
using IRepository.IGenericRepositories;
using IServices;
using IServices.ILDLApplicationsAllowedToRetakeATestServices;
using IServices.ITests.ITest;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelDTO.TestsDTO;
using Models.Tests;
using Services.Exceptions;

namespace Services.LDLApplicationsAllowedToRetakeATestServices;

public class LDLTestRetakeApplicationCreator : ILDLTestRetakeApplicationCreator
{
    private readonly ICreateRepository<LDLApplicationsAllowedToRetakeATest> _createRepository;
    private readonly ILDLTestRetakeApplicationCreationValidator _validator;

    private readonly ILogger<LDLTestRetakeApplicationCreator> _logger;
    private readonly IMapper _mapper;



    public LDLTestRetakeApplicationCreator(
         ICreateRepository<LDLApplicationsAllowedToRetakeATest> createRepository,
         ILDLTestRetakeApplicationCreationValidator validator,
        ILogger<LDLTestRetakeApplicationCreator> logger,

        IMapper mapper)
    {

        _createRepository = createRepository;
        _validator = validator;
        _logger = logger;
        _mapper = mapper;

    }

    public async Task CreateAsync(TestDTO testDTO)
    {
        _logger.LogInformation($"{this.GetType().Name} -- CreateAsync");
        _logger.LogDebug($"TestDTO => Local Driving License Application Id: {testDTO.LocalDrivingLicenseApplicationId} -- Test Type Id: {testDTO.TestTypeId} -- Booking id: {testDTO.BookingId}");


        try
        {
            _logger.LogInformation($"{this.GetType().Name} -- CreateAsync");



            //var isValid = await validator.IsValid(testDTO.LocalDrivingLicenseApplicationId, testDTO.TestTypeId);
            var isValid = await _validator.IsValid(testDTO);


            if (!isValid)
            {
                _logger.LogWarning($"It's not allowed for this LDL Application with id {testDTO.LocalDrivingLicenseApplicationId} to retake a test type: {testDTO.TestTypeId}");

                return;
            }


            var allowedToRetakeATest = new LDLApplicationsAllowedToRetakeATest()
            {
                // here the 
                IsAllowedToRetakeATest = true,
                LocalDrivingLicenseApplicationId = testDTO.LocalDrivingLicenseApplicationId,
                TestTypeId = testDTO.TestTypeId
            };


            var createdObj = await _createRepository.CreateAsync(allowedToRetakeATest);

        }
        catch (System.Exception ex)
        {
            throw new System.Exception(ex.Message, ex);
        }

    }
}


