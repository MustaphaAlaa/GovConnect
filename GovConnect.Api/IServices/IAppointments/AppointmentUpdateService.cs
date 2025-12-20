using ModelDTO.Appointments;
using Models.Tests;

namespace IServices.IAppointments
{
    public interface IAppointmentUpdateService : IUpdateService<AppointmentDTO, Appointment>
    {
    }
}
