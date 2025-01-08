using AutoMapper;
using IServices.IAppointments;
using IServices.ITests;
using Microsoft.AspNetCore.Mvc;
using Models.Tests;
using Models.Tests.Enums;
using System.Globalization;
using System.Net;
using ModelDTO.API;
using ModelDTO.Appointments;
using DataConfigurations;
using IServices.ITimeIntervalService;

namespace Web.Controllers.Appoinments;

[ApiController]
[Route("api/Appointments")]
public class AppointmentController : ControllerBase
{
    private readonly IGetTestTypeService _getTestTypes;
    private readonly IGetAllTestTypesService _getAllTestTypesService;
    private readonly ILogger<Appointment> _logger;
    private readonly IMapper _mapper;
    private readonly IGetAppointmentService _getAppointmentService;
    private readonly IGetAllAppointmentsService _getAllAppointmentService;
    private readonly IGetAllTimeIntervalService _getAllTimeIntervalService;
    private readonly ICreateAppointmentService _createAppointmentService;
    private readonly GovConnectDbContext _context;

    public AppointmentController(IGetTestTypeService getTestTypes,
        IGetAllTestTypesService getAllTestTypesService,
        IGetAppointmentService getAppointmentService,
        IGetAllAppointmentsService getAllAppointmentService,
        IGetAllTimeIntervalService getAllTimeIntervalService,
        ICreateAppointmentService createAppointmentService,
        ILogger<Appointment> logger,
        IMapper mapper,
        GovConnectDbContext context)
    {
        _logger = logger;
        _mapper = mapper;
        _getTestTypes = getTestTypes;
        _getAllTestTypesService = getAllTestTypesService;
        _getAppointmentService = getAppointmentService;
        _getAllAppointmentService = getAllAppointmentService;
        _getAllTimeIntervalService = getAllTimeIntervalService;
        _createAppointmentService = createAppointmentService;
        _context = context;
    }


    /// <summary>
    /// Gets a list of appointments for a specific test type by its Id and a given day.
    /// </summary>
    /// <param name="TypeId">The Id of the test type.</param>
    /// <param name="day">the date for which to retrive info. dd/mm/yyyy</param>
    /// <returns>A list of appointments scheduled for the specified test type and day.</returns>
    [HttpGet("type/{TypeId}")]
    public IActionResult GetTypeAppointments(int TypeId, string day)
    {
        _logger.LogInformation($"Get Appointments for TestType by id:{TypeId} and day:{day}");
        //validate for get and create

        if (!Enum.IsDefined(typeof(EnTestTypes), TypeId))
        {
            return BadRequest("Invalid Test Type Id");
        }

        DateTime date;

        try
        {
            date = DateTime.ParseExact(day, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            if ((date < DateTime.Now) || (date > DateTime.Now.AddDays(14)))
            {
                return BadRequest("Invalid Date Range");
            }

            string hh = $"i need appiontments for {TypeId}, {date.ToString("dd/MMMM//yyyy")}";
            return Ok(hh);
        }
        catch (Exception ex)
        {
            return BadRequest($"Invalid Date Format, {ex.Message}");
        }
    }


    [HttpPost("type/{TypeId}/CreateAppointment")]
    public async Task<IActionResult> CreateAppointment([FromRoute] int TypeId, [FromBody] CreateAppointmentsRequest req)
    {
        _logger.LogInformation($"Create Appointments for TestType by id: {TypeId}");

        // Validate the request body
        if (req == null)
        {
            return BadRequest(new ApiResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = new List<string> { "Request body cannot be null." }
            });
        }

        // Assign the TestTypeId from the route
        req.TestTypeId = TypeId;

        // Validate the model (e.g., required fields, valid DateTime formats)
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(new ApiResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = errors
            });
        }

        try
        {
            // Call the service to create the appointment
            var res = await _createAppointmentService.CreateAsync(req);

            // Return a success response
            var response = new ApiResponse
            {
                Result = res,
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the appointment.");

            // Return an error response
            return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.InternalServerError,
                ErrorMessages = new List<string> { "An error occurred while processing your request." }
            });
        }
    }

    [HttpGet("type/{TypeId}/available-days")]
    public async Task<IActionResult> GetDays([FromRoute] int TypeId)
    {
        _logger.LogInformation($"Get available days for appointments For TestTypeId: {TypeId}");

        // Validate the request body
        if (TypeId <= 0)
        {
            return BadRequest(new ApiResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = new List<string> { "Invalid Test Type Id" }
            });
        }


        try
        {
            var days = await _context.SP_GetAvailableDays(TypeId);

            var response = new ApiResponse
            {
                Result = days,
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the appointment.");

            // Return an error response
            return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.InternalServerError,
                ErrorMessages = new List<string> { "An error occurred while processing your request." }
            });
        }
    }


    [HttpGet("type/{TypeId:int}/Time-Interval")]
    public async Task<IActionResult> GetDays([FromRoute] int TypeId, [FromQuery] DateOnly day)
    {
        _logger.LogInformation($"Get available days for appointments For TestTypeId: {TypeId}");

        var appti = await _getAllAppointmentService.GetAllAsync(app => app.AppointmentDay == day && app.TestTypeId == TypeId);

        // var gg2 = await _getAllTimeIntervalService.GetTimeIntervalsDictionaryAsync(ti => ti.TimeIntervalId > 0);
        //var gg = await _getAllTimeIntervalService.GetAllAsync();

        //var koko = appti.Join(gg, a => a.TimeIntervalId,
        //    t => t.TimeIntervalId,
        //    (a, t) => new
        //    {
        //        Id = t.TimeIntervalId,
        //        Hour = t.Hour,
        //        Minute = t.Minute
        //    });
        //.Select(t =>
        //    new TimeIntervalDTO
        //    {
        //        TimeIntervalId = t.Id,
        //        Hour = t.Hour,
        //        Minute = t.Minute
        //    }).ToList();


        var koko = await _context.SP_GetTestTypeDayTimeInterval(TypeId, day);
        var dict = new Dictionary<int, List<TimeIntervalDTO>>();
        foreach (var t in koko)
        {
            if (!dict.TryAdd((int)t.Hour, new List<TimeIntervalDTO>() { t }))
            {
                dict[(int)t.Hour].Add(t);
            }
        }
        return Ok(dict);
        // Validate the request body
        //if (TypeId <= 0)
        //{
        //    return BadRequest(new ApiResponse
        //    {
        //        IsSuccess = false,
        //        StatusCode = HttpStatusCode.BadRequest,
        //        ErrorMessages = new List<string> { "Invalid Test Type Id" }
        //    });
        //}


        //try
        //{
        //    var days = await _context.SP_GetAvailableDays(TypeId);

        //    var response = new ApiResponse
        //    {
        //        Result = days,
        //        StatusCode = HttpStatusCode.OK,
        //        IsSuccess = true
        //    };

        //    return Ok(response);
        //}
        //catch (Exception ex)
        //{
        //    _logger.LogError(ex, "An error occurred while creating the appointment.");

        //    // Return an error response
        //    return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse
        //    {
        //        IsSuccess = false,
        //        StatusCode = HttpStatusCode.InternalServerError,
        //        ErrorMessages = new List<string> { "An error occurred while processing your request." }
        //    });
        //}
    }




}