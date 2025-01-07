using AutoMapper;
using IRepository;
using IServices.IAppointments;
using Microsoft.Extensions.Logging;
using ModelDTO.Appointments;
using Models.Tests;
using IServices.ITimeIntervalService;
using IServices.IValidators;
using IServices.IValidtors.IAppointments;

namespace Services.AppointmentsService;

public class CreateAppointmentsService : ICreateAppointmentService
{
    private readonly ICreateRepository<Appointment> _createRepository;
    //private readonly ICreateAppointmentValidator _createAppointmentValidator;
    private readonly IGetRepository<Appointment> _getRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<Appointment> _logger;
    private readonly IDateValidator _dateValidator;
    private readonly ITestTypeValidator _testTypeValidator;
    private readonly IGetTimeIntervalService _getTimeIntervalService;

    public CreateAppointmentsService(ICreateRepository<Appointment> createRepository,
        IGetRepository<Appointment> getRepository,
        //ICreateAppointmentValidator createAppointmentValidator,
        IMapper mapper,
        ILogger<Appointment> logger,
        IDateValidator dateValidator,
        ITestTypeValidator testTypeValidator,
        IGetTimeIntervalService getTimeIntervalService)
    {
        _createRepository = createRepository;
        _getRepository = getRepository;
        //_createAppointmentValidator = createAppointmentValidator;
        _mapper = mapper;
        _logger = logger;
        _dateValidator = dateValidator;
        _testTypeValidator = testTypeValidator;
        _getTimeIntervalService = getTimeIntervalService;
    }


    /// <summary>
    /// Create Appointments for a test type
    /// </summary>
    /// <param name="entity">the request to create new appointments</param>
    /// <returns>AppointmentCreationResponse Contains appointments are created, and appointments failed in creation </returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<AppointmentCreationResponse> CreateAsync(CreateAppointmentsRequest entity)
    {
        _logger.LogInformation($"{this.GetType().Name} CreateAsync");
        if (entity == null)
        {
            _logger.LogError($"{this.GetType().Name} CreateAsync Error, Argument is null");
            throw new ArgumentNullException(nameof(entity));
        }

        AppointmentCreationResponse response = new();


        foreach (var day in entity.AppointmentDay)
        {
            try
            {
                _dateValidator.Validate(day);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"{this.GetType().Name} CreateAsync Error: {e.Message}");

                var res = new AppointmentResult();
                res.Date = day;
                res.Reason = e.Message;
                res.Status = "Failed";
                res.TimeIntervalIds = entity.TimeIntervalIds;

                response.FailedAppointments.Add(res);
                continue;
            }

            foreach (var timeInterval in entity.TimeIntervalIds)
            {
                var tI = await _getTimeIntervalService.GetByAsync(TI => TI.TimeIntervalId == timeInterval);
                if (tI is null)
                {
                    _logger.LogInformation($"{this.GetType().Name} CreateAsync Error: Time Interval Not Found");

                    var res = new AppointmentResult();
                    res.Date = new DateTime();
                    res.Reason = $"Time Interval With Id : {timeInterval} Not Found";
                    res.Status = "Failed";
                    res.TimeIntervalIds = new List<int>() { timeInterval };

                    response.FailedAppointments.Add(res);
                    continue;
                }

                var isAppointmentExist = await _getRepository.GetAsync(app =>
                    app.TestTypeId == entity.TestTypeId &&
                    app.TimeIntervalId == timeInterval &&
                    DateOnly.FromDateTime(day) == app.AppointmentDay);

                if (isAppointmentExist != null)
                {
                    _logger.LogInformation($"{this.GetType().Name} CreateAsync Error: Appointment Exists");

                    var res = new AppointmentResult();
                    res.Date = new DateTime();
                    res.TimeIntervalIds = new List<int>() { timeInterval };
                    res.Reason = $"Time Interval With Id : {timeInterval} Not Found";
                    res.Status = "Failed";

                    response.FailedAppointments.Add(res);
                    continue;
                }


                var createAppointmentReq = new Appointment()
                {
                    AppointmentDay = DateOnly.FromDateTime(day),
                    TimeIntervalId = timeInterval,
                    TestTypeId = entity.TestTypeId,
                    IsAvailable = true
                };

                try
                {
                    _logger.LogInformation($"{this.GetType().Name} CreateAsync : Creating Appointment.");

                    var appointment = await _createRepository.CreateAsync(createAppointmentReq);

                    AppointmentResult appointmentResult = new();
                    appointmentResult.Date = appointment.AppointmentDay.ToDateTime(TimeOnly.MinValue);
                    appointmentResult.TimeIntervalIds = new List<int>() { timeInterval };
                    appointmentResult.Status = "Success";
                    response.CreatedAppointments.Add(appointmentResult);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to create Appointment : {ex.Message}");
                }
            }
        }

        return response;
    }
}