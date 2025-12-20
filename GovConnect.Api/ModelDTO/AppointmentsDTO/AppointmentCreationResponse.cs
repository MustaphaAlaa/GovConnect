namespace ModelDTO.Appointments;

public class AppointmentCreationResponse
{
    public List<AppointmentResult> CreatedAppointments { get; set; } = new List<AppointmentResult>();
    public List<AppointmentResult> FailedAppointments { get; set; } = new List<AppointmentResult>();
}