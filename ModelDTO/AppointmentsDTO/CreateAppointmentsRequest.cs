

using System.ComponentModel.DataAnnotations;

namespace ModelDTO.Appointments;

public class CreateAppointmentsRequest
{
    [Required(ErrorMessage = "The TestTypeId field is required.")]

    public int TestTypeId { get; set; }


    [Required(ErrorMessage = "The AppointmentDay field is required.")]

    public Dictionary<DateTime, List<int>> Appointments { get; set; }

}


