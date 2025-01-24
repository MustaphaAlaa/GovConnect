using AutoMapper;
using IRepository.IGenericRepositories;
using IServices.ILDLApplicationsAllowedToRetakeATestServices;
using Microsoft.Extensions.Logging;
using ModelDTO.TestsDTO;
using Models.Tests;

namespace DefaultNamespace;

public class LDLTestRetakeApplicationUpdater : ILDLTestRetakeApplicationUpdater
{

    private readonly IGetRepository<LDLApplicationsAllowedToRetakeATest> _getRepo;
    private readonly IUpdateRepository<LDLApplicationsAllowedToRetakeATest> _updateRepo;
    private readonly ILogger<LDLTestRetakeApplicationUpdater> _logger;
    private readonly IMapper _mapper;
    public LDLTestRetakeApplicationUpdater(IGetRepository<LDLApplicationsAllowedToRetakeATest> getRepo,
        IUpdateRepository<LDLApplicationsAllowedToRetakeATest> updateRepo,
        ILogger<LDLTestRetakeApplicationUpdater> logger,
        IMapper mapper)
    {
        _getRepo = getRepo;
        _updateRepo = updateRepo;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<LDLApplicationsAllowedToRetakeATestDTO> UpdateAsync(LDLApplicationsAllowedToRetakeATestDTO updateRequest)
    {
        try
        {
            if (updateRequest == null)
            {
                throw new ArgumentNullException(nameof(updateRequest));
            }

            var AllowedToRetakeATestObj = await _getRepo.GetAsync(ldl => ldl.LocalDrivingLicenseApplicationId == updateRequest.LocalDrivingLicenseApplicationId
                                      && ldl.TestTypeId == updateRequest.TestTypeId);
            if (AllowedToRetakeATestObj is null)
            {
                _logger.LogError("LDLApplicationsAllowedToRetakeATestDTO object not found");
                throw new ArgumentNullException(nameof(AllowedToRetakeATestObj));
            }

            AllowedToRetakeATestObj.IsAllowedToRetakeATest = updateRequest.IsAllowedToRetakeATest;

            var updatedObj = await _updateRepo.UpdateAsync(AllowedToRetakeATestObj);

            var lDLAppAllowedToRetakeATestDTO = _mapper.Map<LDLApplicationsAllowedToRetakeATestDTO>(updatedObj);

            return lDLAppAllowedToRetakeATestDTO;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating LDLApplicationsAllowedToRetakeATestDTO object");
            throw;
        }
    }
}