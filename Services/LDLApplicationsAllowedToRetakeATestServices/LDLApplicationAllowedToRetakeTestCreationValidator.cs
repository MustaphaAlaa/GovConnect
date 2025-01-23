using AutoMapper;
using DataConfigurations.TVFs.ITVFs;
using IRepository.ITVFs;
using IServices.ILDLApplicationsAllowedToRetakeATestServices;
using Microsoft.Extensions.Logging;
using ModelDTO.TestsDTO;
using Services.Exceptions;

namespace Services.LDLApplicationsAllowedToRetakeATestServices;
public class LDLApplicationAllowedToRetakeTestCreationValidator : ILDLTestRetakeApplicationCreationValidator
{

    private readonly ITVF_GetLDLAppsAllowedToRetakATest _tVF_GetLDLAppsAllowedToRetakATest;
    private readonly ILogger<LDLApplicationAllowedToRetakeTestCreationValidator> _logger;
    private readonly IMapper _mapper;

    public LDLApplicationAllowedToRetakeTestCreationValidator(ITVF_GetLDLAppsAllowedToRetakATest tVF_GetLDLAppsAllowedToRetakATest,
        ILogger<LDLApplicationAllowedToRetakeTestCreationValidator> logger,
        IMapper mapper)
    {
        _tVF_GetLDLAppsAllowedToRetakATest = tVF_GetLDLAppsAllowedToRetakATest;
        _logger = logger;
        _mapper = mapper;
    }

    public   Task<bool> IsValid(TestDTO testDTO)
    {
        _logger.LogInformation($"{this.GetType().Name} -- IsValid");


        try
        {
             
             if (testDTO.TestResult)
            {
                _logger.LogError("cannot be allowed to retake the test because it is already passed.");
                throw new ValidationException("Test Type Is Passed");
            }

                return Task.FromResult(true);
                
            // var testResult  = await testResultService.GetTestResultForABookingId(e.BookingId);


            // if (testResult is null)
            // {
            //     _logger.LogError("booking id doesn't exist in booking table");
            //     throw new DoesNotExistException("Booking Id is not exist");
            // }

 
            // var isExist = await _tVF_GetLDLAppsAllowedToRetakATest.GetLDLAppsAllowedToRetakATest(LDLApplicationId, TestTypeId);



            // return isExist == null;
        }
        catch (System.Exception ex)
        {
            throw new System.Exception(ex.Message, ex);
        }
    }
}