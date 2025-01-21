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
    private readonly IGetRepository<Appointment> _getRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<Appointment> _logger;
    private readonly IDateValidator _dateValidator;
    private readonly ITestTypeValidator _testTypeValidator;
    private readonly IGetTimeIntervalService _getTimeIntervalService;

    public CreateAppointmentsService(ICreateRepository<Appointment> createRepository,
        IGetRepository<Appointment> getRepository,
         IMapper mapper,
        ILogger<Appointment> logger,
        IDateValidator dateValidator,
        ITestTypeValidator testTypeValidator,
        IGetTimeIntervalService getTimeIntervalService)
    {
        _createRepository = createRepository;
        _getRepository = getRepository;
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


        foreach (var item in entity.Appointments)
        {
            try
            {
                _dateValidator.Validate(item.Key);
            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger.LogInformation($"{this.GetType().Name} CreateAsync Error: {e.Message}");

                var res = new AppointmentResult();
                res.Date = item.Key;
                res.Reason = e.Message;
                res.Status = "Failed";
                res.TimeIntervalIds = item.Value;

                response.FailedAppointments.Add(res);
                continue;
            }

            foreach (var timeInterval in item.Value)
            {
                var tI = await _getTimeIntervalService.GetByAsync(TI => TI.TimeIntervalId == timeInterval);
                if (tI is null)
                {
                    _logger.LogInformation($"{this.GetType().Name} CreateAsync Error: Time Interval Not Found");

                    var res = new AppointmentResult();
                    res.Date = item.Key;
                    res.Reason = $"Time Interval With Id : {timeInterval} Not Found";
                    res.Status = "Failed";
                    res.TimeIntervalIds = new List<int>() { timeInterval };

                    response.FailedAppointments.Add(res);
                    continue;
                }

                var isAppointmentExist = await _getRepository.GetAsync(app =>
                    app.TestTypeId == entity.TestTypeId &&
                    app.TimeIntervalId == timeInterval &&
                    DateOnly.FromDateTime(item.Key) == app.AppointmentDay);

                if (isAppointmentExist != null)
                {
                    _logger.LogInformation($"{this.GetType().Name} CreateAsync Error: Appointment Exists");

                    var res = new AppointmentResult()
                    {
                        Date = item.Key,
                        Status = "Failed",
                        Reason = $" Appointment is already Exist",
                        TimeIntervalIds = new List<int>() { timeInterval },
                    };
                    response.FailedAppointments.Add(res);
                    continue;
                }


                var createAppointmentReq = new Appointment()
                {
                    AppointmentDay = DateOnly.FromDateTime(item.Key),
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
                catch (System.Exception ex)
                {
                    throw new System.Exception($"Failed to create Appointment : {ex.Message}");
                }
            }
        }

        return response;
    }
}