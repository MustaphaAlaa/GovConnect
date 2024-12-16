using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.Employee;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using ModelDTO.ApplicationDTOs.Employee;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices.Services.EmployeeAppService;
using Services.Execptions;
using System.Linq.Expressions;


namespace GovConnect_Tests.ApplicationServices.Services.EmployeeTests;

public class UpdateApplicationByEmployeeTEST
{
    private readonly IFixture _fixture;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IGetRepository<Application>> _getRepository;
    private readonly Mock<IUpdateRepository<Application>> _updateRepository;
    private readonly IUpdateApplicationByEmployee _updateApplicationByEmployee;


    public UpdateApplicationByEmployeeTEST()
    {
        _fixture = new Fixture();
        _mapper = new Mock<IMapper>();
        _getRepository = new Mock<IGetRepository<Application>>();
        _updateRepository = new Mock<IUpdateRepository<Application>>();

        _updateApplicationByEmployee = new UpdateApplicationByEmployeeService(_getRepository.Object,
                                                    _updateRepository.Object, _mapper.Object);
    }


    [Fact]
    public async Task UpdateAsync_WhenUpdateRequestObjIsNull_ThrowsArgumentNullException()
    {
        //Arrange
        UpdateApplicationByEmployee updateRequest = null;

        //Act
        Func<Task> action = async () => await _updateApplicationByEmployee.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task UpdateAsync_WhenIdIsInvalid_ThrowsArgumentException(int id)
    {
        //Arrange
        UpdateApplicationByEmployee updateRequest = new() { Id = id };

        //Act
        Func<Task> action = async () => await _updateApplicationByEmployee.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task UpdateAsync_WhenApplicantUserIdIsInvalid_ThrowsArgumentException()
    {
        //Arrange
        UpdateApplicationByEmployee updateRequest = new()
        {
            Id = 2,
            ApplicantUserId = Guid.Empty
        };

        //Act
        Func<Task> action = async () => await _updateApplicationByEmployee.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task UpdateAsync_WhenRequestApplicantUserIdIsNotEqualToApplicationApplicantId_ThrowsInvalidOperationException()
    {
        //Arrange
        UpdateApplicationByEmployee updateRequest = new()
        {
            Id = 2,
            ApplicantUserId = Guid.NewGuid(),
            ApplicationStatus = 1,
        };

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<Application, bool>>>()))
            .ReturnsAsync(new Application
            {
                UserId = Guid.NewGuid()
            });


        //Act
        Func<Task> action = async () => await _updateApplicationByEmployee.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task UpdateAsync_WhenApplicationStatusIsInvalid_ThrowsArgumentException()
    {
        //Arrange
        UpdateApplicationByEmployee updateRequest = new()
        {
            Id = 2,
            ApplicantUserId = Guid.Empty,
            ApplicationStatus = 1,
        };

        //Act
        Func<Task> action = async () => await _updateApplicationByEmployee.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task UpdateAsync_WhenApplicationDoesNotExist_ThrowsDoesNotExistException()
    {
        //Arrange
        UpdateApplicationByEmployee updateRequest = _fixture.Build<UpdateApplicationByEmployee>()
         .With(app => app.ApplicationStatus, 2)
         .Create();

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<Application, bool>>>()))
                  .ReturnsAsync(null as Application);


        //Act
        Func<Task> action = async () => await _updateApplicationByEmployee.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<DoesNotExistException>();
    }

    [Fact]
    public async Task UpdateAsync_WhenFaildToUpdate_ThrowsFailedToUpdateException()
    {
        //Arrange
        UpdateApplicationByEmployee updateRequest = _fixture.Build<UpdateApplicationByEmployee>()
          .With(app => app.ApplicationStatus, 2)
          .Create();

        var dummyApplication = new Application
        {
            Id = 1,
            UserId = updateRequest.ApplicantUserId,
            ApplicationStatus = 5,

        };


        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<Application, bool>>>()))
            .ReturnsAsync(dummyApplication);

        _updateRepository.Setup(temp => temp.UpdateAsync(It.IsAny<Application>()))
            .ReturnsAsync(null as Application);


        //Act
        Func<Task> action = async () => await _updateApplicationByEmployee.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<FailedToUpdateException>();
    }

    [Fact]
    public async Task UpdateAsync_WhenFaildToMapFromModelToDTO_ThrowsAutoMapperMappingException()
    {
        //Arrange
        UpdateApplicationByEmployee updateRequest = _fixture.Build<UpdateApplicationByEmployee>()
            .With(app => app.ApplicationStatus, 2)
            .Create();

        var dummyApplication = new Application
        {
            Id = 1,
            UserId = updateRequest.ApplicantUserId,
            ApplicationStatus = 5,

        };

        _mapper.Setup(temp => temp.Map<Application>(It.IsAny<UpdateApplicationByEmployee>()))
            .Returns(dummyApplication);

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<Application, bool>>>()))
                      .ReturnsAsync(dummyApplication);

        _updateRepository.Setup(temp => temp.UpdateAsync(dummyApplication))
                         .ReturnsAsync(dummyApplication);

        _mapper.Setup(temp => temp.Map<Application>(It.IsAny<UpdateApplicationByEmployee>()))
               .Returns(null as Application);

        //Act
        Func<Task> action = async () =>
                            await _updateApplicationByEmployee.UpdateAsync(updateRequest);

        //Assert
        await action.Should()
                .ThrowAsync<AutoMapperMappingException>();
    }

    [Fact]
    public async Task UpdateAsync_SuccessfullyUpdateTheOject_ReturnsApplicationDTOForEmployee()
    {
        //Arrange
        UpdateApplicationByEmployee updateRequest = _fixture.Build<UpdateApplicationByEmployee>()
                                    .With(app => app.ApplicationStatus, 5)
                                    .Create();

        var dummyApplication = new Application
        {
            Id = 1,
            UserId = updateRequest.ApplicantUserId,
            ApplicationStatus = 5,

        };

        ApplicationDTOForEmployee applicationDTOForEmployee = new ApplicationDTOForEmployee()
        {
            Id = updateRequest.Id,
            ApplicantUserId = updateRequest.ApplicantUserId,
            ApplicationStatus = updateRequest.ApplicationStatus,
            UpdatedByEmployeeId = updateRequest.UpdatedByEmployeeId
        };

        _mapper.Setup(temp => temp.Map<Application>(It.IsAny<UpdateApplicationByEmployee>()))
            .Returns(dummyApplication);

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<Application, bool>>>()))
                      .ReturnsAsync(dummyApplication);

        _updateRepository.Setup(temp => temp.UpdateAsync(dummyApplication))
                         .ReturnsAsync(dummyApplication);

        _mapper.Setup(temp => temp.Map<ApplicationDTOForEmployee>(It.IsAny<Application>()))
               .Returns(applicationDTOForEmployee);

        //Act
        var result = await _updateApplicationByEmployee.UpdateAsync(updateRequest);

        //Assert
        result.Should().BeEquivalentTo(applicationDTOForEmployee);
    }
}
