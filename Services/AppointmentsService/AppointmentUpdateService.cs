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
        private readonly ILogger _logger;
        private readonly ICreateBookingService _createBookingService;
        public AppointmentUpdateService(IUpdateRepository<Appointment> updateRepository,
                                        ICreateBookingService createBookingService,
                                        IGetAppointmentService getAppointmentService,
                                        IMapper mapper,
                                         ILogger logger)
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
            var appointment = await _getAppointmentService.GetByAsync(app => app.AppointmentId == booking.AppointmentId);

            Appointment appointmentDTO = new Appointment()
            {
                AppointmentId = appointment.AppointmentId,
                AppointmentDay = appointment.AppointmentDay,
                IsAvailable = false,
            };

            var updatedAppointment = await _updateRepository.UpdateAsync(appointmentDTO);
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
