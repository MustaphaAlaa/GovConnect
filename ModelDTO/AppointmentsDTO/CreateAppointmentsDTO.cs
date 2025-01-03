

namespace ModelDTO.Appointments;

public class CreateAppointmentsDTO
{
    public int TestTypeId { get; set; }
    public DateOnly AppointmentDay { get; set; }

    public List<int> TimeIntervalIds { get; set; }
}
