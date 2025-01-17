using Models.Tests;

namespace IRepository.ISPs;

/// <summary>
/// Interface for stored procedure SP_InsertAppointment
/// </summary>
public interface ISP_InsertAppointment
{
    /// <summary>
    ///  Execute stored procedure SP_InsertAppointment
    /// </summary>
    /// <param name="appointment">appointment that contains variables for strod procedure</param>
    /// <returns></returns>
    public Task<int> SP_InsertAppointment(Appointment appointment);
}

