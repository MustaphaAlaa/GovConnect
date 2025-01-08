using ModelDTO.Appointments;

namespace DataConfigurations
{
    /// <summary>
    /// Interface for the stored procedure SP_GetTestTypeDayTimeInterval.
    /// </summary>
    public interface ISP_GetTestTypeDayTimeInterval
    {
        /// <summary>
        /// Executes the stored procedure SP_GetTestTypeDayTimeInterval to retrieve time intervals for a given test type and day.
        /// </summary>
        /// <param name="TestTypeId">The ID of the test type.</param>
        /// <param name="day">The specific day to retrieve time intervals for.</param>
        /// <returns>A list of TimeIntervalDTO objects.</returns>
        public Task<List<TimeIntervalDTO>> SP_GetTestTypeDayTimeInterval(int TestTypeId, DateOnly day);
    }
}
