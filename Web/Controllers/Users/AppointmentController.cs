using AutoMapper;
using IServices.IAppointments;
using Models.Tests;
using Models.Tests.Enums;
using ModelDTO.API;
using ModelDTO.Appointments;
using IServices.ITimeIntervalService;
using IServices.ITests.ITestTypes;
using IRepository.ITVFs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.TimeIntervalServices;
using System.Net;

namespace Web.Controllers.Users;

/*
     !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
     !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
     !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
     !!!!!!!!!!!!!!!!!!!!!!!!!!
     * I'll clean and and restructre all endpoints later these for testing purpose
     *
     */


[ApiController]
[Route("api/Appointments")]
public class AppointmentController : ControllerBase
{
    private ApiResponse _apiResponse;
    private readonly ILogger<Appointment> _logger;
    private readonly IMapper _mapper;

    private readonly IGetAppointmentService _getAppointmentService;
    private readonly IGetAllAppointmentsService _getAllAppointmentService;

    private readonly ITestTypeRetrievalService _getTestTypes;
    private readonly IAsyncAllTestTypesRetrieverService _getAllTestTypesService;
    private readonly IGetTimeIntervalService _getTimeIntervalService;
    private readonly IGetAllTimeIntervalService _getAllTimeIntervalService;
    private readonly ITVF_GetTestTypeDayTimeInterval _TVF_GetTestTypeDayTimeInterval;
    private readonly ITVF_GetAvailableDays _TVF_GetAvailableDays;
    public AppointmentController(ITestTypeRetrievalService getTestTypes,
        IAsyncAllTestTypesRetrieverService getAllTestTypesService,
        IGetAppointmentService getAppointmentService,
        IGetAllAppointmentsService getAllAppointmentService,
        IGetAllTimeIntervalService getAllTimeIntervalService,
        ICreateAppointmentService createAppointmentService,
        ILogger<Appointment> logger,
        IMapper mapper,
        ITVF_GetTestTypeDayTimeInterval TVF_GetTestTypeDayTimeInterval,
        ITVF_GetAvailableDays TVF_GetAvailable)
    {
        _logger = logger;
        _mapper = mapper;
        _getTestTypes = getTestTypes;
        _getAllTestTypesService = getAllTestTypesService;
        _getAppointmentService = getAppointmentService;
        _getAllAppointmentService = getAllAppointmentService;
        _getAllTimeIntervalService = getAllTimeIntervalService;
        _createAppointmentService = createAppointmentService;
        _TVF_GetTestTypeDayTimeInterval = TVF_GetTestTypeDayTimeInterval;
        _TVF_GetAvailableDays = TVF_GetAvailable;
    }


    /// <summary>
    /// Gets a list of appointments for a specific test type by its Id and a given day.
    /// </summary>
    /// <param name="TypeId">The Id of the test type.</param>
    /// <param name="day">the date for which to retrive info. dd/mm/yyyy</param>
    /// <returns>A list of appointments scheduled for the specified test type and day.</returns>
    [HttpGet("type/{TypeId}")]
    [Authorize(Policy = "JWT", Roles = "User,Employee")]
    public IActionResult GetTypeAppointments(int TypeId, string day)
    {
        _logger.LogInformation($"Get Appointments for TestType by id:{TypeId} and day:{day}");
        //Validate for get and create

        if (!Enum.IsDefined(typeof(EnTestTypes), TypeId))
        {
            return BadRequest("Invalid Test Type Id");
        }

        DateTime date;

        try
        {
            date = DateTime.ParseExact(day, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            if (date < DateTime.Now || date > DateTime.Now.AddDays(14))
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



    [HttpGet("type/{TypeId}/available-days")]
    [Authorize(Policy = "JWT", Roles = "User,Employee")]
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
            var days = await _TVF_GetAvailableDays.GetAvailableDays(TypeId);

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
    [Authorize(Policy = "JWT", Roles = "User,Employee")]
    public async Task<IActionResult> GetTimeIntervals([FromRoute] int TypeId, [FromQuery] DateOnly day)
    {
        _logger.LogInformation($"Get available days for appointments For TestTypeId: {TypeId}");



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

            var testTypeDayTimeIntervals = await _TVF_GetTestTypeDayTimeInterval.GetTestTypeDayTimeInterval(TypeId, day);

            var response = new ApiResponse
            {
                Result = testTypeDayTimeIntervals,
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


    [HttpGet("TimeInterval/{id:int}")]
    [Authorize(Policy = "JWT", Roles = "User,Employee")]

    public async Task<IActionResult> GetTimeInterval(int id)
    {
        _logger.LogInformation($"{this.GetType().Name} ---- GetTimeInterval By Id {id}");
        try
        {
            var ti = await _getTimeIntervalService.GetByAsync(ti => ti.TimeIntervalId == id);
            _apiResponse = new ApiResponse()
            {
                IsSuccess = true,
                ErrorMessages = null,
                Result = ti,
                StatusCode = HttpStatusCode.OK,
            };
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{this.GetType().Name} -- {nameof(this.GetTimeInterval)}");

            _apiResponse = new ApiResponse()
            {
                IsSuccess = false,
                ErrorMessages = null,
                Result = null,
                StatusCode = HttpStatusCode.InternalServerError,
            };
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpGet("TimeIntervals")]
    [Authorize(Policy = "JWT", Roles = "User,Employee")]
    public async Task<IActionResult> GetTimeIntervals(int hour = 0)
    {
        _logger.LogInformation($"{this.GetType().Name} ---- GetAllTimeInterval.");
        try
        {
            if (hour == 1 || hour == 2)
            {
                hour += 12;
            }

            bool isHourExist = Enum.IsDefined(typeof(EnHour), hour);

            _apiResponse = new ApiResponse()
            {
                ErrorMessages = null,
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
            };


            if (hour == 0 || !isHourExist)
            {
                List<TimeIntervalDTO> timeIntervalDTOs = await _getAllTimeIntervalService.GetAllAsync();
                _apiResponse.Result = await _getAllTimeIntervalService.GetAllAsync();
            }
            else
            {

                var ti = await _getAllTimeIntervalService.GetAllAsync(app => app.Hour == (EnHour)hour);

                _apiResponse.Result = ti.ToList();
            }

            return Ok(_apiResponse);
        }
        catch (Exception ex)
        {
            _apiResponse = new ApiResponse();
            _apiResponse.ErrorMessages.Add(ex.Message);
            _apiResponse.IsSuccess = false;
            _apiResponse.StatusCode = HttpStatusCode.InternalServerError;

            return this.StatusCode((int)_apiResponse.StatusCode, _apiResponse);

        }
    }

}