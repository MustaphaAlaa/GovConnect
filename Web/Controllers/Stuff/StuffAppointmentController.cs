using AutoMapper;
using IRepository.ITVFs;
using IServices.IAppointments;
using IServices.ITests.ITestTypes;
using IServices.ITimeIntervalService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelDTO.API;
using ModelDTO.Appointments;
using Models.Tests;
using System.Net;

namespace Web.Controllers.Stuff
{


    [ApiController]
    [Route("api/Stuff/Appointments")]
    public class StuffAppointmentController : ControllerBase
    {
        private readonly ILogger<Appointment> _logger;
        private readonly IMapper _mapper;

        private readonly IGetAppointmentService _getAppointmentService;
        private readonly IGetAllAppointmentsService _getAllAppointmentService;
        private readonly ICreateAppointmentService _createAppointmentService;

        private readonly ITestTypeRetrievalService _getTestTypes;
        private readonly IAsyncAllTestTypesRetrieverService _getAllTestTypesService;

        private readonly IGetAllTimeIntervalService _getAllTimeIntervalService;
        private readonly ITVF_GetTestTypeDayTimeInterval _TVF_GetTestTypeDayTimeInterval;
        private readonly ITVF_GetAvailableDays _TVF_GetAvailableDays;
        public StuffAppointmentController(ITestTypeRetrievalService getTestTypes,
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




        [HttpPost("type/{TypeId}/CreateAppointment")]
        [Authorize(Policy = "JWT", Roles = "Employee,Admin")]

        public async Task<IActionResult> CreateAppointment([FromRoute] int TypeId, [FromBody] CreateAppointmentsRequest req)
        {
            _logger.LogInformation($"{this.GetType().Name} -- {nameof(this.CreateAppointment)}");
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


    }
}
