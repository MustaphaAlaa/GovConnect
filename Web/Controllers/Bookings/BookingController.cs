using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelDTO.Appointments;

namespace Web.Controllers.Bookingss
{
    [Route("api/Bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        [HttpGet("Appointment")]
        public IActionResult GetAppointments()
        {
            return Ok();
        }


        [HttpPost("Appointment")]
        public IActionResult CreateAppointments(List<CreateAppointmentsDTO> appointmentsDTOs)
        {
            return Ok();
        }
    }
}
