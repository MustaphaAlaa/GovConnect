﻿using IRepository.IGenericRepositories;
using IServices.IAppointments;
using IServices.IBookingServices;
using IServices.IValidators.BookingValidators;
using Microsoft.Extensions.Logging;
using ModelDTO.BookingDTOs;
using Models.Tests;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BookingServices;


/// <summary>
/// Service for handling first-time booking of an appointment.
/// </summary>
public class FirstTimeBookingAnAppointment : IFirstTimeBookingAnAppointmentValidation
{
    private readonly IGetRepository<Booking> _getBookingRepository;
    private readonly IGetRepository<Appointment> _getAppointmentRepository;
    private readonly ILogger<Booking> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="FirstTimeBookingAnAppointment"/> class.
    /// </summary>
    /// <param name="getBookingRepository">Repository for retrieving booking records.</param>
    /// <param name="getAppointmentService">Repository for retrieving appointment records.</param>
    /// <param name="logger">Logger for logging information.</param>
    public FirstTimeBookingAnAppointment(IGetRepository<Booking> getBookingRepository,
        IGetRepository<Appointment> getAppointmentService,
        ILogger<Booking> logger)
    {
        _getBookingRepository = getBookingRepository;
        _getAppointmentRepository = getAppointmentService;
        _logger = logger;
    }

    /// <summary>
    /// Determines if the user dosen't booked an test appointment before or not.
    /// </summary>
    /// <param name="createBookingRequest">The booking request details.</param>
    /// <returns>True if it is the first time booking, otherwise false.</returns>
    public async Task Validate(CreateBookingRequest createBookingRequest)
    {
        _logger.LogInformation($"{this.GetType().Name} ---- Validate --- FirstTime");

        Booking? booking = await _getBookingRepository.GetAsync(BookingAppointment =>
            BookingAppointment.LocalDrivingLicenseApplicationId == createBookingRequest.LocalDrivingLicenseApplicationId
            && BookingAppointment.UserId == createBookingRequest.UserId
            && BookingAppointment.TestTypeId == createBookingRequest.TestTypeId
        );

        if (booking is not null)
        {
            string msg = "The user has already booked an appointment for this test type.";
            _logger.LogError(msg);
            throw new AlreadyExistException(msg);
        }

    }
}
