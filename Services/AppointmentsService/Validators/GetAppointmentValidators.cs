 using IServices.IValidtors.IAppointments;
using Microsoft.Extensions.Logging;

namespace Services.AppointmentsService.Validators;

public class GetAppointmentValidators : IGetAppointmentValidator
{

    private readonly IValidateAppointmentBase _validateAppointmentBase;
    private readonly ILogger<GetAppointmentValidators> _logger;

    public GetAppointmentValidators(IValidateAppointmentBase validateAppointmentBase,
        ILogger<GetAppointmentValidators> logger)
    {
        _validateAppointmentBase = validateAppointmentBase;
        _logger = logger;
    }

    public async Task<DateTime> Validate(int TypeId, string day)
    {
        DateTime date;

        try
        {
            date = await _validateAppointmentBase.ValidateAppointment(TypeId, day);



        }
        catch (System.Exception ex)
        {
            throw new System.Exception(ex.Message, ex);
        }
        return date;
    }
}