

using System.ComponentModel.DataAnnotations;

namespace ModelDTO.Appointments;

public class CreateAppointmentsRequest
{
    [Required(ErrorMessage = "The TestTypeId field is required.")]

    public int TestTypeId { get; set; }

    [Required(ErrorMessage = "The AppointmentDay field is required.")]
    [MinLength(1, ErrorMessage = "The AppointmentDay field must contain at least one date.")]
    public List<DateTime> AppointmentDay { get; set; }

    [Required(ErrorMessage = "The AppointmenTimeIntervalIds field is required.")]
    [MinLength(1, ErrorMessage = "The TimeIntervalIds field must contain at least one date.")]
    public List<int> TimeIntervalIds { get; set; }
}