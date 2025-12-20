using ModelDTO.TestsDTO;

namespace IRepository.ITVFs;


/// <summary>
/// Interface for the table-valued function GetAvailableDays.
/// </summary>
public interface ITVF_GetAvailableDays
{
    /// <summary>
    /// Executes the table-valued function GetAvailableDays to retrieve available days for a given test type.
    /// </summary>
    /// <param name="TestTypeId">The ID of the test type.</param>
    /// <returns>A list of available days as DateOnly objects.</returns>
    public Task<List<DateOnly>> GetAvailableDays(int TestTypeId);
}


