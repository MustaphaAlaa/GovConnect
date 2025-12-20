using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelDTO.Appointments;

namespace IServices.IValidtors.IAppointments;

public interface ICreateAppointmentValidator
{
    Task<DateTime> Validate(CreateAppointmentsRequest request);
}
