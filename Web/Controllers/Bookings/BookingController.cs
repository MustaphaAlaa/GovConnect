﻿using IServices.ITimeIntervalService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelDTO.Appointments;
using Models.Tests.Enums;

namespace Web.Controllers.Bookingss
{
    [Route("api/Bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IGetTimeIntervalService _getTimeIntervalService;
        private readonly IGetAllTimeIntervalService _getAllTimeIntervalService;
        private readonly ILogger<BookingController> _logger;

        public BookingController(IGetTimeIntervalService getTimeIntervalService,
            IGetAllTimeIntervalService getAllTimeIntervalService,
            ILogger<BookingController> logger)
        {
            _getTimeIntervalService = getTimeIntervalService;
            _getAllTimeIntervalService = getAllTimeIntervalService;
            _logger = logger;
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