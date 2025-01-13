using AutoMapper;
using IRepository;
using IServices.IBookingServices;
using Microsoft.Extensions.Logging;
using ModelDTO.BookingDTOs;
using Models.Tests;
using Models.Tests.Enums;
using Services.Execptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BookingServices;

public class CreateBookingService : ICreateBookingService
{

    private readonly IFirstTimeBookingAnAppointment _firstTimeBookingAnAppointment;
    private readonly IGetRepository<TestType> _getTestTypeRepository;
    private readonly ICreateRepository<Booking> _createBookingRepository;
    private readonly ILogger<Booking> _logger;
    private readonly IMapper _mapper;

    public CreateBookingService(IFirstTimeBookingAnAppointment firstTimeBookingAnAppointment,
                                IGetRepository<TestType> getTestTypeRepository,
                                ICreateRepository<Booking> createBookingRepository,
                                ILogger<Booking> logger,
                                IMapper mapper)
    {
        _firstTimeBookingAnAppointment = firstTimeBookingAnAppointment;
        _getTestTypeRepository = getTestTypeRepository;
        _createBookingRepository = createBookingRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<BookingDTO?> CreateAsync(CreateBookingRequest entity)
    {
        _logger.LogInformation($"{this.GetType().Name} ---- CreateAsync");


        //step 1: validate the test type order

        //step 2: check if it first time to book an appointemnt
        var isFirstTime = await _firstTimeBookingAnAppointment.IsFirstTime(entity);
        BookingDTO bookingDTO = null;


        if (isFirstTime)
        {
            _logger.LogInformation($"{this.GetType().Name} ---- CreateAsync --- FirstTime");

            // Book the appointment
            // 
            var testType = await _getTestTypeRepository.GetAsync(tt => tt.TestTypeId == entity.TestTypeId);

            if (testType == null)
                throw new DoesNotExistException();


            Booking bookingReq = _mapper.Map<Booking>(entity);

            bookingReq.PaidFees = testType.TestTypeFees;
            bookingReq.BookingDate = DateTime.Now;
            bookingReq.BookingStatus = EnBookingStatus.Pendeing.ToString();
            bookingReq.RetakeTestApplicationId = null;

            try
            {
                var booking = await _createBookingRepository.CreateAsync(bookingReq);
                bookingDTO = _mapper.Map<BookingDTO>(booking);
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }

        }



        //step 3: valiadte if it already pass




        //step 4: Validate The RetakeTestApplication

        _logger.LogInformation($"{this.GetType().Name} ---- CreateAsync --- RetakeTestValidation");

        //is exist
        // not included in any Booking

        //step 5: return the DTO


        return bookingDTO;

    }
}
