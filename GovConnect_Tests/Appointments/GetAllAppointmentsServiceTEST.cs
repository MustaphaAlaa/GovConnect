using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.IAppointments;
using IServices.IValidtors.ILocalDrivingLicenseApplications;
using Microsoft.Extensions.Logging;
using ModelDTO.Appointments;
using Models.Tests;
using Moq;
using Services.AppointmentsService;

namespace GovConnect_Tests.Appointments;

public class GetAllAppointmentsServiceTEST
{

    private readonly IGetAllAppointmentsService _getAllAppointmentsService;
    private readonly Mock<IGetAllRepository<Appointment>> _getAllRepository;
    private readonly Mock<ILogger<AppointmentDTO>> _logger;
    private readonly Mock<IMapper> _mapper;

    public GetAllAppointmentsServiceTEST()
    {
        _getAllRepository = new Mock<IGetAllRepository<Appointment>>();
        _mapper = new Mock<IMapper>();
        _logger = new Mock<ILogger<AppointmentDTO>>();

        _getAllAppointmentsService = new GetAllAppointmentsService(_getAllRepository.Object, _logger.Object, _mapper.Object);

    }


    [Fact]
    public async Task GetAllAsync_WhenDatabaseIsEmpty_ReturnNull()
    {
        // Arrange

        _getAllRepository.Setup(x => x.GetAllAsync())
           .ReturnsAsync(new List<Appointment>());
        _mapper.Setup(x => x.Map<AppointmentDTO>(It.IsAny<Appointment>()))
            .Returns(new AppointmentDTO());

        // Act
        var result = await _getAllAppointmentsService.GetAllAsync();

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAllAsync_WhenDatabaseIsNotEmpty_ReturnAppointmentDTOList()
    {
        // Arrange 
        List<Appointment> appointments = new() {
            new Appointment { AppointmentId = 1, AppointmentDay = new DateOnly(2025, 1, 5), TestTypeId = 1, TimeIntervalId = 1, IsAvailable = true },
            new Appointment { AppointmentId = 2, AppointmentDay = new DateOnly(2025, 2, 5), TestTypeId = 2, TimeIntervalId = 1, IsAvailable = true },
            new Appointment { AppointmentId = 3, AppointmentDay = new DateOnly(2025, 3, 5), TestTypeId = 3, TimeIntervalId = 1, IsAvailable = true },
        };

        List<AppointmentDTO> appointmentsDTO = new();

        foreach (var apointment in appointments)
        {
            appointmentsDTO.Add(new AppointmentDTO
            {
                AppointmentId = apointment.AppointmentId,
                AppointmentDay = apointment.AppointmentDay,
                TestTypeId = apointment.TestTypeId,
                TimeIntervalId = apointment.TimeIntervalId,
                IsAvailable = apointment.IsAvailable
            });
        }

        _getAllRepository.Setup(x => x.GetAllAsync())
           .ReturnsAsync(appointments);

        _mapper.Setup(x => x.Map<AppointmentDTO>(It.IsAny<Appointment>()))
            .Returns((Appointment source) => new AppointmentDTO
            {
                AppointmentId = source.AppointmentId,
                AppointmentDay = source.AppointmentDay,
                TestTypeId = source.TestTypeId,
                TimeIntervalId = source.TimeIntervalId,
                IsAvailable = source.IsAvailable
            });

        // Act
        var result = await _getAllAppointmentsService.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(appointmentsDTO);
    }



}