using ModelDTO.Appointments;
using ModelDTO.TestsDTOs;

namespace DataConfigurations.TVFs.ITVFs;

/// <summary>
/// Interface for the table-valued function GetTestTypeDayTimeInterval.
/// </summary>
public interface ITVF_GetTestTypeDayTimeInterval
{
    /// <summary>
    /// Executes the table-valued function GetTestTypeDayTimeInterval to retrieve time intervals for a given test type and day.
    /// </summary>
    /// <param name="TestTypeId">The ID of the test type.</param>
    /// <param name="day">The specific day to retrieve time intervals for.</param>
    /// <returns>A list of TimeIntervalForADayDTO objects.</returns>
    public Task<List<TimeIntervalForADayDTO>> GetTestTypeDayTimeInterval(int TestTypeId, DateOnly day);
}
