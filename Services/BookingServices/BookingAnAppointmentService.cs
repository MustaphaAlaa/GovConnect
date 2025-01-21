using AutoMapper;
using IRepository;
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

    private readonly IFirstTimeBookingAnAppointment _firstTimeBookingAnAppointment;
    private readonly IGetRepository<TestType> _getTestTypeRepository;
    private readonly ICreateRepository<Booking> _createBookingRepository;
    private readonly IBookingCreationValidators _bookingCreationValidators;
    private readonly IRetakeTestApplicationValidator _retakeTestApplicationValidator;
    private readonly ITestTypeRetrievalService _testTypeRetrievalService;
    private readonly ILogger<Booking> _logger;
    private readonly IMapper _mapper;

    public BookingAnAppointmentService(IFirstTimeBookingAnAppointment firstTimeBookingAnAppointment,
                                        IGetRepository<TestType> getTestTypeRepository,
                                        ICreateRepository<Booking> createBookingRepository,
                                        IBookingCreationValidators bookingCreationValidators,
                                        IRetakeTestApplicationValidator retakeTestApplicationValidator,
                                        ITestTypeRetrievalService testTypeRetrievalService,
                                        ILogger<Booking> logger,
                                        IMapper mapper)
    {
        _firstTimeBookingAnAppointment = firstTimeBookingAnAppointment;
        _getTestTypeRepository = getTestTypeRepository;
        _createBookingRepository = createBookingRepository;
        _bookingCreationValidators = bookingCreationValidators;
        _retakeTestApplicationValidator = retakeTestApplicationValidator;
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

            await _bookingCreationValidators.IsValid(entity);


            if (entity.RetakeTestApplicationId > 0 || entity.RetakeTestApplicationId is not null)
            {
                _logger.LogInformation($"{this.GetType().Name} ---- CreateAsync --- RetakeTestValidation");
                await _retakeTestApplicationValidator.Validate(entity);

            }
            else
            {

                _logger.LogInformation($"{this.GetType().Name} ---- CreateAsync --- FirstTime");

                var isFirstTime = await _firstTimeBookingAnAppointment.IsFirstTime(entity);

                if (!isFirstTime)
                {
                    string logMsg = $"An Appointment is booked before for test type: {entity.TestTypeId}.";
                    _logger.LogError(logMsg);
                    throw new InvalidOperationException(logMsg);

                }
            }


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

