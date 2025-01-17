using ModelDTO.TestsDTO;

namespace IRepository.ITVFs;

/// <summary>
/// Interface for the table-valued function GetTestResult.
/// </summary>
public interface ITVF_GetTestResult
{
    /// <summary>
    /// Executes the table-valued function GetTestResult to retrieve test for a test id.
    /// </summary>
    /// <param name="TestTypeId">The ID of the test type.</param>
    /// <returns>TestDTO object.</returns>
    public Task<TestDTO?> GetTestResult(int TestTypeId);
}
