using Models.Tests;

namespace IRepository.ISPs.IAppointmentProcedures;

/// <summary>
/// Interface for execute the InsertAppointment stored procedure;
/// </summary>
public interface ISP_InsertAppointment
{
    /// <summary>
    ///  Execute stored procedure Exec
    /// </summary>
    /// <param name="appointment">appointment that contains variables for strod procedure</param>
    /// <returns></returns>
    public Task<int> Exec(Appointment appointment);
}

