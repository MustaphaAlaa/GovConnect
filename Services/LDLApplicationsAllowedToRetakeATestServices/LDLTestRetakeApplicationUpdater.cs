using IRepository.IGenericRepositories;
using IServices.ILDLApplicationsAllowedToRetakeATestServices;
using Microsoft.Extensions.Logging;
using ModelDTO.TestsDTO;

namespace DefaultNamespace;

public class LDLTestRetakeApplicationUpdater : ILDLTestRetakeApplicationUpdater
{

    IGetRepository<LDLApplicationsAllowedToRetakeATestDTO> _getRepo;
    IUpdateRepository<LDLApplicationsAllowedToRetakeATestDTO> _updateRepo;
    ILogger<LDLTestRetakeApplicationUpdater> _logger;

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

            return updatedObj;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating LDLApplicationsAllowedToRetakeATestDTO object");
            throw;
        }
}
}