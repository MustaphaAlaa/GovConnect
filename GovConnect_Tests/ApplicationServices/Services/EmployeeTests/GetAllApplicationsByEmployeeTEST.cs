using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.Employee;
using IServices.IApplicationServices.Category;
using ModelDTO.ApplicationDTOs.Employee;
using ModelDTO.ApplicationDTOs.Category;
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
    private readonly Mock<IGetAllRepository<Application>> _getAllRepository;
    private readonly IGetAllApplicationsEmp _getAllApplicationsEmp;


    public GetAllApplicationsByEmployeeTEST()
    {
        _fixture = new Fixture();
        _mapper = new Mock<IMapper>();

        _getAllRepository = new Mock<IGetAllRepository<Application>>();
        _getAllApplicationsEmp = new GetAllApplicationsByEmployeeService(_getAllRepository.Object, _mapper.Object);
    }


    [Fact]
    public async Task GetAllAsync_EmptyDb_ReturnEmptyList()
    {
        //Arrange
        List<Application> applications = new() { };
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
        List<Application> applicationList = new() {
          _fixture.Build<Application>()
          .With (app=> app.ServiceFees, null as ServiceFees)
          .With(app=> app.Employee, null as Employee)
          .With(app=> app.User, null as User)
          .Create(),
            _fixture.Build<Application>()
          .With (app=> app.ServiceFees, null as ServiceFees)
          .With(app=> app.Employee, null as Employee)
          .With(app=> app.User, null as User)
          .Create(),
            _fixture.Build<Application>()
          .With (app=> app.ServiceFees, null as ServiceFees)
          .With(app=> app.Employee, null as Employee)
          .With(app=> app.User, null as User)
          .Create(),
        };

        _mapper.Setup(temp => temp.Map<ApplicationDTOForEmployee>(It.IsAny<Application>()))
            .Returns((Application source) => new ApplicationDTOForEmployee()
            {
                Id = source.Id,
                ApplicantUserId = source.UserId,
                ApplicationDate = source.ApplicationDate,
                ServiceCategoryId = source.ServiceCategoryId,
                ApplicationStatus = source.ApplicationStatus,
                ApplicationPurposeId = source.ApplicationPurposeId,
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
        List<Application> applicationList = new() { };


        List<ApplicationDTOForEmployee> applicationDTOs = new() { };

        _mapper.Setup(temp => temp.Map<ApplicationDTOForEmployee>(It.IsAny<Application>()))
            .Returns((Application source) => new ApplicationDTOForEmployee()
            {
                Id = source.Id,
                ApplicantUserId = source.UserId,
                ApplicationDate = source.ApplicationDate,
                ServiceCategoryId = source.ServiceCategoryId,
                ApplicationStatus = source.ApplicationStatus,
                ApplicationPurposeId = source.ApplicationPurposeId,
                LastStatusDate = source.LastStatusDate,
                PaidFees = source.PaidFees,
                UpdatedByEmployeeId = source.UpdatedByEmployeeId,

            });

        _getAllRepository.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Application, bool>>>()))
             .ReturnsAsync(applicationList.AsQueryable());
        //Act
        var result = await _getAllApplicationsEmp.GetAllAsync(It.IsAny<Expression<Func<Application, bool>>>());

        //Assert
        result.Should().BeEquivalentTo(applicationDTOs);
    }

    [Fact]
    public async Task GetAllExpressingAsync_DbHasData_ReturnApplicationDTOsList()
    {
        //Arrange
        List<Application> applicationList = new() {
          _fixture.Build<Application>()
          .With (app=> app.ServiceFees, null as ServiceFees)
          .With(app=> app.Employee, null as Employee)
          .With(app=> app.User, null as User)
          .Create(),
            _fixture.Build<Application>()
          .With (app=> app.ServiceFees, null as ServiceFees)
          .With(app=> app.Employee, null as Employee)
          .With(app=> app.User, null as User)
          .Create(),
            _fixture.Build<Application>()
          .With (app=> app.ServiceFees, null as ServiceFees)
          .With(app=> app.Employee, null as Employee)
          .With(app=> app.User, null as User)
          .Create(),
        };


        _mapper.Setup(temp => temp.Map<ApplicationDTOForEmployee>(It.IsAny<Application>()))
            .Returns((Application source) => new ApplicationDTOForEmployee()
            {
                Id = source.Id,
                ApplicantUserId = source.UserId,
                ApplicationDate = source.ApplicationDate,
                ServiceCategoryId = source.ServiceCategoryId,
                ApplicationStatus = source.ApplicationStatus,
                ApplicationPurposeId = source.ApplicationPurposeId,
                LastStatusDate = source.LastStatusDate,
                PaidFees = source.PaidFees,
                UpdatedByEmployeeId = source.UpdatedByEmployeeId,
            });


        List<ApplicationDTOForEmployee> applicationDTOs = applicationList
                   .Select(app => _mapper.Object.Map<ApplicationDTOForEmployee>(app))
                   .ToList();


        _getAllRepository.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Application, bool>>>()))
          .ReturnsAsync(applicationList.AsQueryable());

        //Act
        var result = await _getAllApplicationsEmp.GetAllAsync(It.IsAny<Expression<Func<Application, bool>>>());

        //Assert
        result.Should().BeEquivalentTo(applicationDTOs);
    }

}
