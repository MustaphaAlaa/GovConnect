using AutoMapper;
using IRepository.IGenericRepositories;
using IServices.IAppointments;
using IServices.IBookingServices;
using Microsoft.Extensions.Logging;
using ModelDTO.Appointments;
using ModelDTO.BookingDTOs;
using Models.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AppointmentsService
{
    public class AppointmentUpdateService : IAppointmentUpdateService
    {

        private readonly IUpdateRepository<Appointment> _updateRepository;
        private readonly IGetAppointmentService _getAppointmentService;
        private readonly IMapper _mapper;
        private readonly ILogger<AppointmentUpdateService> _logger;
        private readonly ICreateBookingService _createBookingService;
        public AppointmentUpdateService(IUpdateRepository<Appointment> updateRepository,
                                        ICreateBookingService createBookingService,
                                        IGetAppointmentService getAppointmentService,
                                        IMapper mapper,
                                         ILogger<AppointmentUpdateService> logger)
        {
            _updateRepository = updateRepository;
            _createBookingService = createBookingService;
            this._getAppointmentService = getAppointmentService;
            _mapper = mapper;
            _logger = logger;
            _createBookingService.AppointmentIsBooked += _createBookingService_AppointmentIsBooked;
        }

        private async Task _createBookingService_AppointmentIsBooked(object obj, BookingDTO booking)
        {
            // for latter 
            // When Update an existing Booking Appointment
            // Mark the old one as available and the new one as not available

            var newAppointment = await _getAppointmentService.GetByAsync(app => app.AppointmentId == booking.AppointmentId);

            Appointment newAppointmentDTO = _mapper.Map<Appointment>(newAppointment);

            newAppointmentDTO.IsAvailable = false;


            //  i commented it because i lazy to create more appointments, so i am using only one for testing.
            //  var updatedAppointment = await _updateRepository.UpdateAsync(newAppointmentDTO);
        }

        public async Task<Appointment> UpdateAsync(AppointmentDTO updateRequest)
        {
            _logger.LogInformation($"{this.GetType().Name} -- UpdateAsync");
            if (updateRequest.AppointmentDay < DateOnly.FromDateTime(DateTime.Now))
                return null;


            var appointment = _mapper.Map<Appointment>(updateRequest);

            appointment.AppointmentDay = updateRequest.AppointmentDay;
            appointment.AppointmentId = updateRequest.AppointmentId;

            var updatedAppointment = await _updateRepository.UpdateAsync(appointment);

            return updatedAppointment;
        }
    }
}
