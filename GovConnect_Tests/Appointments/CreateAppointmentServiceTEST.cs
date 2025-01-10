using AutoMapper;
using Azure;
using FluentAssertions;
using IRepository;
using IServices.IAppointments;
using IServices.ITimeIntervalService;
using IServices.IValidators;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using ModelDTO.Appointments;
using Models;
using Models.Tests;
using Moq;
using Services.ApplicationServices.Validators;
using Services.AppointmentsService;
using Services.TimeIntervalServices;
using System.Linq.Expressions;

namespace GovConnect_Tests.Appointments;

public class CreateAppointmentServiceTEST
{
    private readonly Mock<ICreateRepository<Appointment>> _createRepository;
    private readonly Mock<IGetRepository<Appointment>> _getAppointmentRepository;
    private readonly Mock<IGetRepository<TimeInterval>> _getTimeIntervalRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<ILogger<Appointment>> _logger;

    private readonly Mock<IDateValidator> _dateValidator;
    private readonly Mock<ITestTypeValidator> _testTypeValidator;
    private readonly Mock<IGetTimeIntervalService> _getTimeIntervalService;

    private readonly ICreateAppointmentService _createAppointmentService;

    public CreateAppointmentServiceTEST()
    {
        _createRepository = new Mock<ICreateRepository<Appointment>>();
        _getAppointmentRepository = new Mock<IGetRepository<Appointment>>();
        _getTimeIntervalRepository = new Mock<IGetRepository<TimeInterval>>();
        _mapper = new Mock<IMapper>();
        _logger = new Mock<ILogger<Appointment>>();

        _dateValidator = new Mock<IDateValidator>();
        _testTypeValidator = new Mock<ITestTypeValidator>();
        _getTimeIntervalService = new Mock<IGetTimeIntervalService>();
        _createAppointmentService = new CreateAppointmentsService(_createRepository.Object,
                                                                    _getAppointmentRepository.Object,
                                                                    _mapper.Object,
                                                                    _logger.Object,
                                                                    _dateValidator.Object,
                                                                    _testTypeValidator.Object,
                                                                    _getTimeIntervalService.Object);
    }

    [Fact]
    public async Task CreateAsync_WhenArgumentIsNull_ThrowsArgumentException()
    {
        // Act
        Func<Task<AppointmentCreationResponse>> action = async () => await _createAppointmentService.CreateAsync(null);

        // Assert
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task CreateAsync_WhenDateIsInvalid_ReturnsAppointmentCreationResponseWithInvalidDates()
    {
        // Arrange
        CreateAppointmentsRequest createAppointmentsRequest = new()
        {
            TestTypeId = 1,
            Appointments = new Dictionary<DateTime, List<int>>()
                {
                    { DateTime.Now.AddDays(30), new List<int> { 1, 2, 3 } },
                    { new DateTime(1998, 1, 1), new List<int> { 1, 2, 3 } }
                }
        };
        _dateValidator.Setup(temp => temp.Validate(It.IsAny<DateTime>()))
            .Throws(new ArgumentOutOfRangeException("Invalid Date Range"));

        AppointmentCreationResponse expectedResponse = new();
        foreach (var item in createAppointmentsRequest.Appointments)
        {
            var res = new AppointmentResult
            {
                Date = item.Key,
                Status = "Failed",
                Reason = "Specified argument was out of the range of valid values. (Parameter 'Invalid Date Range')",
                TimeIntervalIds = item.Value
            };
            expectedResponse.FailedAppointments.Add(res);
        }

        // Act
        var result = await _createAppointmentService.CreateAsync(createAppointmentsRequest);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
    }


    [Fact]
    public async Task CreateAsync_WhenTimeIntervalIsInvalid_ReturnsAppointmentCreationResponseWithInvalidDates()
    {
        // Arrange
        CreateAppointmentsRequest createAppointmentsRequest = new()
        {
            TestTypeId = 1,
            Appointments = new Dictionary<DateTime, List<int>>()
                {
                    { DateTime.Now.AddDays(30), new List<int> { 21, 32, 31 } },
                    { new DateTime(1998, 1, 1), new List<int> { 11, 23, 1222 } }
                }
        };
        _dateValidator.Setup(temp => temp.Validate(It.IsAny<DateTime>()));
        _getTimeIntervalRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<TimeInterval, bool>>>()))
            .ReturnsAsync(null as TimeInterval);
        AppointmentCreationResponse expectedResponse = new();
        foreach (var item in createAppointmentsRequest.Appointments)
        {
            foreach (var ti in item.Value)
            {
                expectedResponse.FailedAppointments.Add(new AppointmentResult
                {
                    Date = item.Key,
                    Status = "Failed",
                    Reason = $"Time Interval With Id : {ti} Not Found",
                    TimeIntervalIds = new List<int>() { ti }
                });
            }
        }
        // Act
        var result = await _createAppointmentService.CreateAsync(createAppointmentsRequest);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
    }


    [Fact]
    public async Task CreateAsync_WhenAppintmentIsAlreadyExist_ReturnsAppointmentCreationResponseWithInvalidDates()
    {
        // Arrange
        CreateAppointmentsRequest createAppointmentsRequest = new()
        {
            TestTypeId = 1,
            Appointments = new Dictionary<DateTime, List<int>>()
                {
                    { DateTime.Now.AddDays(30), new List<int> { 1, 2, 3 } },
                    { new DateTime(1998, 1, 1), new List<int> { 1, 2, 3 } }
                }
        };
        _dateValidator.Setup(a => a.Validate(It.IsAny<DateTime>()));

        _getTimeIntervalService.Setup(temp => temp.GetByAsync(It.IsAny<Expression<Func<TimeInterval, bool>>>()))
            .ReturnsAsync(new TimeIntervalDTO());

        _getAppointmentRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<Appointment, bool>>>()))
            .ReturnsAsync(new Appointment());

        AppointmentCreationResponse expectedResponse = new();
        foreach (var item in createAppointmentsRequest.Appointments)
        {
            foreach (var ti in item.Value)
            {
                expectedResponse.FailedAppointments.Add(new AppointmentResult
                {
                    Date = item.Key,
                    Status = "Failed",
                    Reason = $" Appointment is already Exist",
                    TimeIntervalIds = new List<int>() { ti }
                });
            }
        }
        // Act
        var result = await _createAppointmentService.CreateAsync(createAppointmentsRequest);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task CreateAsync_CreateAppointments_ReturnsAppointmentCreationResponseWithInvalidDates()
    {
        // Arrange
        CreateAppointmentsRequest createAppointmentsRequest = new()
        {
            TestTypeId = 1,
            Appointments = new Dictionary<DateTime, List<int>>()
                {
                    { DateTime.Now.AddDays(30), new List<int> { 1, 2, 3 } },
                    { new DateTime(1998, 1, 1), new List<int> { 1, 2, 3 } }
                }
        };


        _dateValidator.Setup(a => a.Validate(It.IsAny<DateTime>()));

        _getTimeIntervalService.Setup(temp => temp.GetByAsync(It.IsAny<Expression<Func<TimeInterval, bool>>>()))
            .ReturnsAsync(new TimeIntervalDTO());

        _getAppointmentRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<Appointment, bool>>>()))
            .ReturnsAsync(null as Appointment);

        _createRepository.Setup(temp => temp.CreateAsync(It.IsAny<Appointment>()))
            .ReturnsAsync((Appointment source) => new Appointment()
            {
                AppointmentDay = source.AppointmentDay,
                TimeIntervalId = source.TimeIntervalId,
                TestTypeId = source.TestTypeId,
                IsAvailable = source.IsAvailable

            });

        AppointmentCreationResponse expectedResponse = new();
        foreach (var item in createAppointmentsRequest.Appointments)
        {
            foreach (var ti in item.Value)
            {

                var createAppointment = new Appointment()
                {
                    AppointmentDay = DateOnly.FromDateTime(item.Key),
                    TimeIntervalId = ti,
                    TestTypeId = createAppointmentsRequest.TestTypeId,
                    IsAvailable = true
                };


                expectedResponse.CreatedAppointments.Add(new AppointmentResult
                {
                    Date = createAppointment.AppointmentDay.ToDateTime(TimeOnly.MinValue),
                    Status = "Success",
                    TimeIntervalIds = new List<int>() { ti }
                });
            }
        }
        // Act
        var result = await _createAppointmentService.CreateAsync(createAppointmentsRequest);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
    }




}
