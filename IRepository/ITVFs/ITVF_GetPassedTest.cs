using ModelDTO.TestsDTO;

namespace IRepositorys.ITVFs;

/// <summary>
/// Interface for the table-valued function GetPassedTest.
/// </summary>
public interface ITVF_GetPassedTest
{
    /// <summary>
    /// Executes the table-valued function GetPassedTest to retrieve the passed test record.
    /// </summary>
    /// <param name="LDLApplicationId">The specific local driving license application id to retrieve its test result.</param>
    /// <param name="TestTypeId">The ID of the test type.</param>
    /// <returns>TestDTO object.</returns>
    public Task<TestDTO?> GetPassedTest(int LDLApplicationId, int TestTypeId);
}
