using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.Employee;
using IServices.IApplicationServices.For;
using ModelDTO.ApplicationDTOs.Employee;
using ModelDTO.ApplicationDTOs.For;
using Models.ApplicationModels;
using Models.Users;
using Moq;
using Services.ApplicationServices.Services.EmployeeAppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GovConnect_Tests.ApplicationServices.Services.EmployeeTests;

public class GetAllApplicationsByEmployeeTEST
{
    private readonly IFixture _fixture;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IGetAllRepository<LicenseApplication>> _getAllRepository;
    private readonly IGetAllApplicationsEmp _getAllApplicationsEmp;


    public GetAllApplicationsByEmployeeTEST()
    {
        _fixture = new Fixture();
        _mapper = new Mock<IMapper>();

        _getAllRepository = new Mock<IGetAllRepository<LicenseApplication>>();
        _getAllApplicationsEmp = new GetAllApplicationsByEmployeeService(_getAllRepository.Object, _mapper.Object);
    }


    [Fact]
    public async Task GetAllAsync_EmptyDb_ReturnEmptyList()
    {
        //Arrange
        List<LicenseApplication> applications = new() { };
        _getAllRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(applications);

        //Act
        List<ApplicationDTOForEmployee> result = await _getAllApplicationsEmp.GetAllAsync();

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_DbHasData_ReturnApplicationDTOsList()
    {
        //Arrange
        List<LicenseApplication> applicationList = new() {
          _fixture.Build<LicenseApplication>()
          .With (app=> app.ApplicationFees, null as ApplicationFees)
          .With(app=> app.Employee, null as Employee)
          .With(app=> app.User, null as User)
          .Create(),
            _fixture.Build<LicenseApplication>()
          .With (app=> app.ApplicationFees, null as ApplicationFees)
          .With(app=> app.Employee, null as Employee)
          .With(app=> app.User, null as User)
          .Create(),
            _fixture.Build<LicenseApplication>()
          .With (app=> app.ApplicationFees, null as ApplicationFees)
          .With(app=> app.Employee, null as Employee)
          .With(app=> app.User, null as User)
          .Create(),
        };

        _mapper.Setup(temp => temp.Map<ApplicationDTOForEmployee>(It.IsAny<LicenseApplication>()))
            .Returns((LicenseApplication source) => new ApplicationDTOForEmployee()
            {
                Id = source.Id,
                ApplicantUserId = source.UserId,
                ApplicationDate = source.ApplicationDate,
                ApplicationForId = source.ApplicationForId,
                ApplicationStatus = source.ApplicationStatus,
                ApplicationTypeId = source.ApplicationTypeId,
                LastStatusDate = source.LastStatusDate,
                PaidFees = source.PaidFees,
                UpdatedByEmployeeId = source.UpdatedByEmployeeId,

            });

        List<ApplicationDTOForEmployee> applicationDTOs = applicationList
            .Select(app => _mapper.Object.Map<ApplicationDTOForEmployee>(app))
            .ToList();

        _getAllRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(applicationList);

        //Act
        List<ApplicationDTOForEmployee> result = await _getAllApplicationsEmp.GetAllAsync();

        //Assert
        result.Should().BeEquivalentTo(applicationDTOs);
    }


    [Fact]
    public async Task GetAllExpressionAsync_EmptyDb_ReturnEmptyList()
    {
        //Arrange
        List<LicenseApplication> applicationList = new() { };


        List<ApplicationDTOForEmployee> applicationDTOs = new() { };

        _mapper.Setup(temp => temp.Map<ApplicationDTOForEmployee>(It.IsAny<LicenseApplication>()))
            .Returns((LicenseApplication source) => new ApplicationDTOForEmployee()
            {
                Id = source.Id,
                ApplicantUserId = source.UserId,
                ApplicationDate = source.ApplicationDate,
                ApplicationForId = source.ApplicationForId,
                ApplicationStatus = source.ApplicationStatus,
                ApplicationTypeId = source.ApplicationTypeId,
                LastStatusDate = source.LastStatusDate,
                PaidFees = source.PaidFees,
                UpdatedByEmployeeId = source.UpdatedByEmployeeId,

            });

        _getAllRepository.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<LicenseApplication, bool>>>()))
             .ReturnsAsync(applicationList.AsQueryable());
        //Act
        var result = await _getAllApplicationsEmp.GetAllAsync(It.IsAny<Expression<Func<LicenseApplication, bool>>>());

        //Assert
        result.Should().BeEquivalentTo(applicationDTOs);
    }

    [Fact]
    public async Task GetAllExpressingAsync_DbHasData_ReturnApplicationDTOsList()
    {
        //Arrange
        List<LicenseApplication> applicationList = new() {
          _fixture.Build<LicenseApplication>()
          .With (app=> app.ApplicationFees, null as ApplicationFees)
          .With(app=> app.Employee, null as Employee)
          .With(app=> app.User, null as User)
          .Create(),
            _fixture.Build<LicenseApplication>()
          .With (app=> app.ApplicationFees, null as ApplicationFees)
          .With(app=> app.Employee, null as Employee)
          .With(app=> app.User, null as User)
          .Create(),
            _fixture.Build<LicenseApplication>()
          .With (app=> app.ApplicationFees, null as ApplicationFees)
          .With(app=> app.Employee, null as Employee)
          .With(app=> app.User, null as User)
          .Create(),
        };


        _mapper.Setup(temp => temp.Map<ApplicationDTOForEmployee>(It.IsAny<LicenseApplication>()))
            .Returns((LicenseApplication source) => new ApplicationDTOForEmployee()
            {
                Id = source.Id,
                ApplicantUserId = source.UserId,
                ApplicationDate = source.ApplicationDate,
                ApplicationForId = source.ApplicationForId,
                ApplicationStatus = source.ApplicationStatus,
                ApplicationTypeId = source.ApplicationTypeId,
                LastStatusDate = source.LastStatusDate,
                PaidFees = source.PaidFees,
                UpdatedByEmployeeId = source.UpdatedByEmployeeId,
            });


        List<ApplicationDTOForEmployee> applicationDTOs = applicationList
                   .Select(app => _mapper.Object.Map<ApplicationDTOForEmployee>(app))
                   .ToList();


        _getAllRepository.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<LicenseApplication, bool>>>()))
          .ReturnsAsync(applicationList.AsQueryable());

        //Act
        var result = await _getAllApplicationsEmp.GetAllAsync(It.IsAny<Expression<Func<LicenseApplication, bool>>>());

        //Assert
        result.Should().BeEquivalentTo(applicationDTOs);
    }

}
