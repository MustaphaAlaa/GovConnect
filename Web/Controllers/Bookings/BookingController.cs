using IRepository.ISPs.IAppointmentProcedures;
using IServices.IBookingServices;
using IServices.ITimeIntervalService;
using IServices.IValidators;
using IServices.IValidators.BookingValidators;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ModelDTO.API;
using ModelDTO.Appointments;
using ModelDTO.BookingDTOs;
using Models.Tests.Enums;
using Repositorties.SPs.AppointmentReps;
using Services.BookingServices.Validators;
using System.Net;

namespace Web.Controllers.Bookingss
{
    /*
     !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
     !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
     !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
     !!!!!!!!!!!!!!!!!!!!!!!!!!
     * I'll clean and and restructre all endpoints later these for testing purpose
     *
     */
    [Route("api/Bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IGetAllTimeIntervalService _getAllTimeIntervalService;
        private readonly IGetTimeIntervalService _getTimeIntervalService;
        private readonly ILogger<BookingController> _logger;
        private readonly IFirstTimeBookingAnAppointmentValidation _firstTimeBookingAnAppointment;
        private readonly IRetakeTestApplicationBookingValidator _retakeTestApplicationBookingValidator;
        private readonly ISP_MarkExpiredAppointmentsAsUnavailable _markExpiredAppointmentsAsUnavailable;
        private readonly ICreateBookingService _createBookingService;
        private readonly IBookingCreationValidators _bookingCreationValidators;
        public BookingController(IGetTimeIntervalService getTimeIntervalService,
                         IGetAllTimeIntervalService getAllTimeIntervalService,
                         IFirstTimeBookingAnAppointmentValidation firstTimeBookingAnAppointment,
                         IRetakeTestApplicationBookingValidator retakeTestApplicationBookingValidator,
                         IBookingCreationValidators bookingCreationValidators,
                         ISP_MarkExpiredAppointmentsAsUnavailable markExpiredAppointmentsAsUnavailable,
        ICreateBookingService createBookingService,
                         ILogger<BookingController> logger)
        {
            _getTimeIntervalService = getTimeIntervalService;
            _getAllTimeIntervalService = getAllTimeIntervalService;
            _firstTimeBookingAnAppointment = firstTimeBookingAnAppointment;
            _retakeTestApplicationBookingValidator = retakeTestApplicationBookingValidator;
            _markExpiredAppointmentsAsUnavailable = markExpiredAppointmentsAsUnavailable;
            _bookingCreationValidators = bookingCreationValidators;
            _createBookingService = createBookingService;
            _logger = logger;
        }




        [HttpPost("Appointment/firsttime")]
        public async Task<IActionResult> FirstTimeBookingAppointment([FromBody] CreateBookingRequest createBookingRequest)
        {
            IActionResult actionResult = await BookingAnAppointment(createBookingRequest, _firstTimeBookingAnAppointment);
            return actionResult;
        }

        [HttpPost("Appointment/retakeatest")]
        public async Task<IActionResult> RetakeTestBookingAppointment([FromBody] CreateBookingRequest createBookingRequest)
        {
            IActionResult actionResult = await BookingAnAppointment(createBookingRequest, _retakeTestApplicationBookingValidator);
            return actionResult;
        }

        private async Task<IActionResult> BookingAnAppointment(CreateBookingRequest createBookingRequest, IBookingCreationTypeValidation bookingTypeValidation)
        {
            ApiResponse res = new ApiResponse();
            try
            {
                var affectedRows = await _markExpiredAppointmentsAsUnavailable.Exec();

                await _bookingCreationValidators.IsValid(createBookingRequest, bookingTypeValidation);

                var booked = await _createBookingService.CreateAsync(createBookingRequest);

                res.StatusCode = HttpStatusCode.OK;
                res.Result = booked;
                return Ok(res);

            }
            catch (Exception ex)
            {
                res.ErrorMessages.Add(ex.Message);
                res.Result = null;
                res.StatusCode = HttpStatusCode.NotAcceptable;
                return BadRequest(res);
            }
        }

        [HttpGet("TimeInterval/{id:int}")]
        public async Task<IActionResult> GetTimeInterval(int id)
        {
            _logger.LogInformation($"{this.GetType().Name} ---- GetTimeInterval By Id {id}");
            var ti = await _getTimeIntervalService.GetByAsync(ti => ti.TimeIntervalId == id);

            return Ok(ti);
        }

        [HttpGet("TimeIntervals")]
        public async Task<IActionResult> GetTimeIntervals(int hour = 0)
        {
            _logger.LogInformation($"{this.GetType().Name} ---- GetAllTimeInterval.");

            List<TimeIntervalDTO> timeIntervalDTOs = new List<TimeIntervalDTO>();

            if (hour == 1 || hour == 2)
            {
                hour += 12;
            }

            bool isHourExist = Enum.IsDefined(typeof(EnHour), hour);


            if (hour == 0 || !isHourExist)
            {
                timeIntervalDTOs = await _getAllTimeIntervalService.GetAllAsync();
            }
            else
            {

                var ti = await _getAllTimeIntervalService.GetAllAsync(app => app.Hour == (EnHour)hour);
                timeIntervalDTOs = ti.ToList();
            }

            return Ok(timeIntervalDTOs);
        }

        [HttpGet("Appointment")]
        public IActionResult GetAppointments()
        {


            return Ok();
        }


        [HttpPost("Appointment")]
        public IActionResult CreateAppointments(List<CreateAppointmentsRequest> appointmentsDTOs)
        {
            return Ok();
        }
    }
}