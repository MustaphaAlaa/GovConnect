using ModelDTO.TestsDTO;

namespace DataConfigurations.TVFs.ITVFs;

/// <summary>
/// Interface for the table-valued function GetLDLAppsAllowedToRetakATest.
/// </summary>
public interface ITVF_GetLDLAppsAllowedToRetakATest
{
    /// <summary>
    /// Executes the table-valued function GetLDLAppsAllowedToRetakATest to retrieve local driving license application allowed to retake a test;
    /// </summary>
    /// <param name="LDLApplicationId">The Id of the local driving license application.</param>
    /// <param name="TestTypeId">The Id of the test type.</param>
    /// <returns>object of <see cref="LDLApplicationsAllowedToRetakeATestDTO"/>.</returns>
    public Task<LDLApplicationsAllowedToRetakeATestDTO?> GetLDLAppsAllowedToRetakATest(int LDLApplicationId, int TestTypeId);
}
