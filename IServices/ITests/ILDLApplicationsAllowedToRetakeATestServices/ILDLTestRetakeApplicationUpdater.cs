using ModelDTO.TestsDTO;

namespace IServices.ITests.ILDLApplicationsAllowedToRetakeATestServices;

/// <summary>
/// Interface for updating records in the LDLApplicationsAllowedToRetakeATest table.
/// </summary>
public interface ILDLTestRetakeApplicationUpdater :
    IUpdateService<LDLApplicationsAllowedToRetakeATestDTO, LDLApplicationsAllowedToRetakeATestDTO>
{
}
