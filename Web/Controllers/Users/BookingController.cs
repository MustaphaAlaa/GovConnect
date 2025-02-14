using IRepository.ISPs.IAppointmentProcedures;
using IServices.IAppointments;
using IServices.IBookingServices;
using IServices.ILDLApplicationsAllowedToRetakeATestServices;
using IServices.ITimeIntervalService;
using IServices.IValidators;
using IServices.IValidators.BookingValidators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.IdentityModel.Tokens;
using ModelDTO.API;
using ModelDTO.Appointments;
using ModelDTO.BookingDTOs;
using Models.Tests.Enums;
using Repositorties.SPs.AppointmentReps;
using Services.BookingServices.Validators;
using System.Net;

namespace Web.Controllers.Users
{
    /// <summary>
    /// Controller for handling booking-related operations.
    /// </summary>
    [Route("api/Booking")]
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
        private readonly ILDLTestRetakeApplicationSubscriber _lDLTestRetakeApplicationSubscriber;
        private readonly IAppointmentUpdateService _appointmentUpdate;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingController"/> class.
        /// </summary>
        /// <param name="getTimeIntervalService">Service for retrieving time intervals.</param>
        /// <param name="getAllTimeIntervalService">Service for retrieving all time intervals.</param>
        /// <param name="firstTimeBookingAnAppointment">Validator for first-time booking appointments.</param>
        /// <param name="retakeTestApplicationBookingValidator">Validator for retake test booking appointments.</param>
        /// <param name="bookingCreationValidators">Validators for booking creation.</param>
        /// <param name="markExpiredAppointmentsAsUnavailable">Service for marking expired appointments as unavailable.</param>
        /// <param name="createBookingService">Service for creating bookings.</param>
        /// <param name="appointmentUpdate">Service for updating appointments.</param>
        /// <param name="lDLTestRetakeApplicationSubscriber">Subscriber for retake test applications.</param>
        /// <param name="logger">Logger instance.</param>
        public BookingController(IGetTimeIntervalService getTimeIntervalService,
                         IGetAllTimeIntervalService getAllTimeIntervalService,
                         IFirstTimeBookingAnAppointmentValidation firstTimeBookingAnAppointment,
                         IRetakeTestApplicationBookingValidator retakeTestApplicationBookingValidator,
                         IBookingCreationValidators bookingCreationValidators,
                         ISP_MarkExpiredAppointmentsAsUnavailable markExpiredAppointmentsAsUnavailable,
                         ICreateBookingService createBookingService,
                         IAppointmentUpdateService appointmentUpdate,
                         ILDLTestRetakeApplicationSubscriber lDLTestRetakeApplicationSubscriber,
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

            //for events
            _appointmentUpdate = appointmentUpdate;
            _lDLTestRetakeApplicationSubscriber = lDLTestRetakeApplicationSubscriber;
        }

        /// <summary>
        /// Books an appointment for the first time.
        /// </summary>
        /// <param name="createBookingRequest">The booking request details.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("Appointment/firsttime")]
        [Authorize(Policy = "JWT", Roles = "User")]
        public async Task<IActionResult> FirstTimeBookingAppointment([FromBody] CreateBookingRequest createBookingRequest)
        {
            createBookingRequest.RetakeTestApplicationId = null;
            IActionResult actionResult = await BookingAnAppointment(createBookingRequest, _firstTimeBookingAnAppointment);
            return actionResult;
        }

        /// <summary>
        /// Books an appointment for retaking a test.
        /// </summary>
        /// <param name="createBookingRequest">The booking request details.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("Appointment/retakeatest")]
        [Authorize(Policy = "JWT", Roles = "User")]
        public async Task<IActionResult> RetakeTestBookingAppointment([FromBody] CreateBookingRequest createBookingRequest)
        {
            IActionResult actionResult = await BookingAnAppointment(createBookingRequest, _retakeTestApplicationBookingValidator);
            return actionResult;
        }

        /// <summary>
        /// Creates an appointment for a specific booking type.
        /// </summary>
        /// <param name="createBookingRequest">The booking request details.</param>
        /// <param name="bookingTypeValidation">The booking type validation service.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
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
    }
}