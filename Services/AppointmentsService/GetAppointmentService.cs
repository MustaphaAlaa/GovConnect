using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IRepository;
using IServices.IAppointments;
using Microsoft.Extensions.Logging;
using ModelDTO.Appointments;
using Models.Tests;

namespace Services.AppointmentsService;

public class GetAppointmentService : IGetAppointmentService
{
    private readonly IGetRepository<Appointment> _getRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<Appointment> _logger;

    public GetAppointmentService(IGetRepository<Appointment> getRepository,
        IMapper mapper,
        ILogger<Appointment> logger)
    {
        _getRepository = getRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AppointmentDTO> GetByAsync(Expression<Func<Appointment, bool>> predicate)
    {
        _logger.LogInformation($"{this.GetType().Name} GetByAsync By Expression --> {predicate.ToString()}");

        var appointment = await _getRepository.GetAsync(predicate);

        var appointmentDTO = appointment != null ? _mapper.Map<AppointmentDTO>(appointment) : null;

        return appointmentDTO;

    }
}