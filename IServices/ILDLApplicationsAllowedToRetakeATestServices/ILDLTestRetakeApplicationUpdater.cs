using ModelDTO.TestsDTO;

namespace IServices.ILDLApplicationsAllowedToRetakeATestServices;

/// <summary>
/// Interface for updating records in the LDLApplicationsAllowedToRetakeATest table.
/// </summary>
public interface ILDLTestRetakeApplicationUpdater :
    IUpdateService<LDLApplicationsAllowedToRetakeATestDTO, LDLApplicationsAllowedToRetakeATestDTO>
{
}
