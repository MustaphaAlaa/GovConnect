namespace IRepository.ISPs.IAppointmentProcedures;

/// <summary>
/// Interface for execute the MarkExpiredAppointmentsAsUnavailable stored procedure;
/// </summary>
public interface ISP_MarkExpiredAppointmentsAsUnavailable
{
    /// <summary>
    ///  Execute SP_MarkExpiredAppointmentsAsUnavailable stored procedure;
    /// </summary>
    /// <returns>number of affected rows</returns>
    public Task<int> Exec();
}

