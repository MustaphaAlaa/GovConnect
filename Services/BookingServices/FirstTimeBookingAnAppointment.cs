using IRepository;
using IServices.IAppointments;
using IServices.IBookingServices;
using Microsoft.Extensions.Logging;
using ModelDTO.BookingDTOs;
using Models.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BookingServices;

public class FirstTimeBookingAnAppointment : IFirstTimeBookingAnAppointment
{

    private readonly IGetRepository<Booking> _getBookingRepository;
    private readonly IGetRepository<Appointment> _getAppointmentRepository;
    private readonly ILogger<Booking> _logger;

    public FirstTimeBookingAnAppointment(IGetRepository<Booking> getBookingRepository,
        IGetRepository<Appointment> getAppointmentService,
        ILogger<Booking> logger)
    {
        _getBookingRepository = getBookingRepository;
        _getAppointmentRepository = getAppointmentService;
        _logger = logger;
    }

    public async Task<bool> IsFirstTime(CreateBookingRequest createBookingRequest)
    {

        Booking? booking = await _getBookingRepository.GetAsync(BookingAppointment =>
        BookingAppointment.LocalDrivingLicenseApplicationId == createBookingRequest.LocalDrivingLicenseApplicationId
        && BookingAppointment.UserId == createBookingRequest.UserId
        && BookingAppointment.TestTypeId == createBookingRequest.TestTypeId
        );

        return booking is null;
    }
}
