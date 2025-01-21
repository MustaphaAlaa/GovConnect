using AutoMapper;
using DataConfigurations.TVFs.ITVFs;
using IRepository.ITVFs;
using IServices.ITests.ILDLApplicationsAllowedToRetakeATestServices;
using Microsoft.Extensions.Logging;

namespace Services.LDLApplicationsAllowedToRetakeATestServices;
public class LDLTestRetakeApplicationValidator : ILDLTestRetakeApplicationCreationValidator
{

    private readonly ITVF_GetLDLAppsAllowedToRetakATest _tVF_GetLDLAppsAllowedToRetakATest;
    private readonly ILogger<LDLTestRetakeApplicationValidator> _logger;
    private readonly IMapper _mapper;

    public LDLTestRetakeApplicationValidator(ITVF_GetLDLAppsAllowedToRetakATest tVF_GetLDLAppsAllowedToRetakATest,
        ILogger<LDLTestRetakeApplicationValidator> logger,
        IMapper mapper)
    {
        _tVF_GetLDLAppsAllowedToRetakATest = tVF_GetLDLAppsAllowedToRetakATest;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<bool> IsValid(int LDLApplicationId, int TestTypeId)
    {
        _logger.LogInformation($"{this.GetType().Name} -- IsValid");


        try
        {
            var isExist = await _tVF_GetLDLAppsAllowedToRetakATest.GetLDLAppsAllowedToRetakATest(LDLApplicationId, TestTypeId);



            return isExist == null;
        }
        catch (System.Exception ex)
        {
            throw new System.Exception(ex.Message, ex);
        }
    }
}