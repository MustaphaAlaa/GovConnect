/*using Microsoft.Extensions.Logging;
using ModelDTO.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IRepository;
using IServices.IValidators;
using IServices.IValidtors.IAppointments;
using Models.Tests;

namespace Services.AppointmentsService.Validators;

public class CreateAppointmentValidators : ICreateAppointmentValidator
{
    private readonly IValidateAppointmentBase _validateAppointmentBase;
    private readonly ILogger<CreateAppointmentValidators> _logger;
    private readonly IGetRepository<Appointment> _getRepository;
    private readonly IMapper _mapper;
    private readonly IDateValidator _dateValidator;

    public CreateAppointmentValidators(IValidateAppointmentBase validateAppointmentBase,
        ILogger<CreateAppointmentValidators> logger,
        IMapper mapper,
        IDateValidator dateValidator)
    {
        _validateAppointmentBase = validateAppointmentBase;
        _logger = logger;
        _mapper = mapper;
        _dateValidator = dateValidator;
    }

    //Check
    //appointment is exist
    //is the date is valid
    //is time interval is valid
    public Task<DateTime> Validate(CreateAppointmentsRequest request)
    {
        foreach (var day in request.AppointmentDay)
        {
            foreach (var timeInterval in request.TimeIntervalIds)
            {
                var isAppointmentExist = await _getRepository.GetAsync(app =>
                    app.TestTypeId == entity.TestTypeId &&
                    app.TimeIntervalId == timeInterval &&
                    DateOnly.FromDateTime(day) == app.AppointmentDay);

                if (isAppointmentExist != null)
                    continue;

                var createReq = _mapper.Map<Appointment>(entity);
                createReq.IsAvailable = true;
                try
                {
                    await _createAppointmentValidator.Validate(entity.TestTypeId, day.ToString("d"));
                }
                catch (Exception ex)
                {
                    invalidAppointments.Add(entity);
                }
                //var appointment = await _createRepository.CreateAsync(createReq);
                //appointments.Add(appointment);
            }
        }

        throw new Exception();
    }


    
}*/