using ModelDTO.Appointments;
using Models.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices.IAppointments;

public interface IGetAppointmentService : IAsyncRetrieveService<Appointment, AppointmentDTO>
{
}
