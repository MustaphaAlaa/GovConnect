using ModelDTO.TestsDTO;

namespace DataConfigurations.TVFs.ITVFs;

/// <summary>
/// Interface for the table-valued function GetTestResultForABookingId.
/// </summary>
public interface ITVF_GetTestResultForABookingId
{
    /// <summary>
    /// Executes the table-valued function GetTestResultForABookingId to retrieve the test associated to the booking id.
    /// </summary>
    /// <param name="BookingId">The Bokoing Id for a test appointment</param>
    /// <returns>TestDTO object.</returns>
    public Task<TestDTO?> GetTestResultForABookingId(int BookingId);
}
