using AutoMapper;
using IRepository.IGenericRepositories;
using IRepository.ISPs.IAppointmentProcedures;
using IServices.IApplicationServices.Fees;
using IServices.IBookingServices;
using IServices.ITests.ITestTypes;
using IServices.IValidators;
using IServices.IValidators.BookingValidators;
using Microsoft.Extensions.Logging;
using ModelDTO.BookingDTOs;
using Models.ApplicationModels;
using Models.Tests;
using Models.Tests.Enums;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BookingServices;

public class BookingAnAppointmentService : ICreateBookingService
{
    private readonly ICreateRepository<Booking> _createBookingRepository;

    private readonly ITestTypeRetrievalService _testTypeRetrievalService;

    private readonly ILogger<Booking> _logger;
    private readonly IMapper _mapper;

    public BookingAnAppointmentService(ICreateRepository<Booking> createBookingRepository,
                                         ITestTypeRetrievalService testTypeRetrievalService,
                                         ILogger<Booking> logger,
                                         IMapper mapper)
    {
        _createBookingRepository = createBookingRepository;
        _testTypeRetrievalService = testTypeRetrievalService;
        _logger = logger;
        _mapper = mapper;


    }

    public event Func<object, BookingDTO, Task> AppointmentIsBooked;

    public async Task<BookingDTO?> CreateAsync(CreateBookingRequest entity)
    {
        _logger.LogInformation($"{this.GetType().Name} ---- CreateAsync");
        try
        {
            var testType = await _testTypeRetrievalService.GetByAsync(tt => tt.TestTypeId == entity.TestTypeId);

            if (testType == null)
                throw new DoesNotExistException();

            Booking bookingReq = _mapper.Map<Booking>(entity);

            bookingReq.PaidFees = testType.TestTypeFees;
            bookingReq.BookingDate = DateTime.Now;
            bookingReq.BookingStatus = EnBookingStatus.Pendeing.ToString();
            bookingReq.RetakeTestApplicationId = null;


            var booking = await _createBookingRepository.CreateAsync(bookingReq);
            var bookingDTO = _mapper.Map<BookingDTO>(booking);

            AppointmentIsBooked?.Invoke(this, bookingDTO);
            return bookingDTO;

        }
        catch (System.Exception ex) { throw new Exception(ex.Message, ex); }

    }
}

