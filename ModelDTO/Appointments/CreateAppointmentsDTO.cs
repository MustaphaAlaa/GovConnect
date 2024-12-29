

namespace ModelDTO.Appointments;

public class CreateAppointmentsDTO
{
    public DateOnly AppointmentDay { get; set; }

    public List<int> TimeIntervalIds { get; set; }
}
