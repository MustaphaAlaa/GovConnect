using AutoMapper;
using FluentAssertions;
using IRepository.IGenericRepositories;
using IServices.IAppointments;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using ModelDTO.Appointments;
using Models.Tests;
using Moq;
using Services.AppointmentsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GovConnect_Tests.Appointments;
public class GetAppointmentServiceTEST
{
    private readonly IGetAppointmentService _getAppointmentService;
    private readonly Mock<IGetRepository<Appointment>> _getRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<ILogger<Appointment>> _logger;

    public GetAppointmentServiceTEST()
    {
        _getRepository = new Mock<IGetRepository<Appointment>>();
        _mapper = new Mock<IMapper>();
        _logger = new Mock<ILogger<Appointment>>();
        _getAppointmentService = new GetAppointmentService(_getRepository.Object, _mapper.Object, _logger.Object);
    }

    [Fact]
    public async Task GetByAsync_WhenAppointmentExists_ReturnsAppointmentDTO()
    {
        // Arrange
        var appointment = new Appointment
        {
            AppointmentId = 1,
            AppointmentDay = new DateOnly(2025, 1, 5),
            TestTypeId = 1,
            TimeIntervalId = 1,
            IsAvailable = true
        };
        var appointmentDTO = new AppointmentDTO
        {
            AppointmentId = appointment.AppointmentId,
            AppointmentDay = appointment.AppointmentDay,
            TestTypeId = appointment.TestTypeId,
            TimeIntervalId = appointment.TimeIntervalId,
            IsAvailable = appointment.IsAvailable
        };

        _getRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Appointment, bool>>>())).ReturnsAsync(appointment);
        _mapper.Setup(x => x.Map<AppointmentDTO>(appointment)).Returns(appointmentDTO);

        // Act
        var result = await _getAppointmentService.GetByAsync(x => x.AppointmentId == 1);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(appointmentDTO);
    }

    [Fact]
    public async Task GetByAsync_WhenAppointmentDoesNotExists_ReturnsNull()
    {
        // Arrange
        _getRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Appointment, bool>>>())).ReturnsAsync((Appointment)null);
        _mapper.Setup(x => x.Map<AppointmentDTO>(It.IsAny<Appointment>())).Returns((AppointmentDTO)null);

        // Act
        var result = await _getAppointmentService.GetByAsync(x => x.AppointmentId == 1);

        // Assert
        result.Should().BeNull();
    }
}
