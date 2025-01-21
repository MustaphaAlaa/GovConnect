using AutoMapper;
using IServices.IValidators;
using IServices.IValidators.BookingValidators;
using Microsoft.Extensions.Logging;
using ModelDTO.BookingDTOs;
using Models.Tests.Enums;
using Services.Exceptions;

namespace Services.BookingServices;

public class BookingTestTypeOrder : ITestTypeOrder
{
    private readonly IBookingRetrieveService _getBooking;
    private readonly ILogger<BookingTestTypeOrder> _logger;
    private readonly IMapper _mapper;

    public BookingTestTypeOrder(IBookingRetrieveService getBooking,
        ILogger<BookingTestTypeOrder> logger,
        IMapper mapper)
    {
        _getBooking = getBooking;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task CheckTheOrder(CreateBookingRequest request)
    {
        _logger.LogInformation($"{GetType().Name} -- CheckTheOrder");

        BookingDTO? booking;

        switch ((EnTestTypes)request.TestTypeId)
        {
            case EnTestTypes.Vision:
                return;
            case EnTestTypes.Written_Theory:
                booking = await _getBooking.GetByAsync(booking =>
                                  booking.TestTypeId == (int)EnTestTypes.Vision
                                  && booking.LocalDrivingLicenseApplicationId == request.LocalDrivingLicenseApplicationId);
                if (booking != null)
                {
                    throw new InvalidOrderException($"Invalid Order {EnTestTypes.Vision.ToString()} must be before it.");
                }
                break;
            case EnTestTypes.Practical_Street:
                booking = await _getBooking.GetByAsync(booking =>
                               booking.TestTypeId == (int)EnTestTypes.Written_Theory
                               && booking.LocalDrivingLicenseApplicationId == request.LocalDrivingLicenseApplicationId);
                if (booking != null)
                {
                    throw new InvalidOrderException($"Invalid Order {EnTestTypes.Written_Theory.ToString()} must be before it.");
                }

                break;
            default:
                throw new DoesNotExistException();
        }
    }
}
