namespace DataConfigurations.TVFs.ITVFs;

public interface ITVF_GetAvailableDays
{
    /// <summary>
    /// Executes the stored procedure GetAvailableDays to retrieve available days for a given test type.
    /// </summary>
    /// <param name="TestTypeId">The ID of the test type.</param>
    /// <returns>A list of available days as DateOnly objects.</returns>
    public Task<List<DateOnly>> GetAvailableDays(int TestTypeId);
}


