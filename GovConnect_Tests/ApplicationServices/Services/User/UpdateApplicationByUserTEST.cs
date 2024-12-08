using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.User;
using Microsoft.AspNetCore.Routing.Template;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Models.Users;
using Moq;
using Services.ApplicationServices.Services.UserAppServices;
using Services.Execptions;
using System.Linq.Expressions;

namespace GovConnect_Tests.ApplicationServices.Services.UserTests;

public class UpdateApplicationByUserTEST
{
    private readonly IFixture _fixture;
    private readonly Mock<IMapper> _mapper;

    private readonly Mock<IUpdateRepository<LicenseApplication>> _updateRepository;
    private readonly Mock<IGetRepository<LicenseApplication>> _getRepository;

    private readonly IUpdateApplicationByUser _updateApplicationByUser;

    public UpdateApplicationByUserTEST()
    {
        _fixture = new Fixture();
        _mapper = new Mock<IMapper>();

        _getRepository = new Mock<IGetRepository<LicenseApplication>>();
        _updateRepository = new Mock<IUpdateRepository<LicenseApplication>>();

        _updateApplicationByUser = new UpdateApplcationByUserService(_updateRepository.Object, _getRepository.Object, _mapper.Object);
    }


    [Fact]
    public async Task UpdateAsync_UpdateObj_ThrowArgumentNullException()
    {
        //Arrange
        UpdateApplicationByUser updateRequest = null;

        //Act
        Func<Task> action = async () => await _updateApplicationByUser.UpdateAsync(updateRequest);
        //Assert
        await action.Should().ThrowAsync<ArgumentNullException>();
    }


    [Theory]
    [InlineData(null)]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task UpdateAsync_InvaildApplicationId_ThrowsArgumentOutOfRangeException(int id)
    {
        //Arrange
        UpdateApplicationByUser updateRequest = new UpdateApplicationByUser() { Id = id };

        //Act
        Func<Task> action = async () => await _updateApplicationByUser.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<ArgumentOutOfRangeException>();
    }

    [Fact]
    public async Task UpdateAsync_InvaildApplicantUserId_ThrowsArgumentOutOfRangeException()
    {
        //Arrange
        UpdateApplicationByUser updateRequest = new UpdateApplicationByUser() { Id = 1 };

        //Act
        Func<Task> action = async () => await _updateApplicationByUser.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<ArgumentOutOfRangeException>();
    }

    [Fact]
    public async Task UpdateAsync_InvaildApplicationTypeId_ThrowsArgumentOutOfRangeException()
    {
        //Arrange
        UpdateApplicationByUser updateRequest = new UpdateApplicationByUser() { Id = 1, ApplicantUserId = Guid.NewGuid(), ApplicationTypeId = 0 };

        //Act
        Func<Task> action = async () => await _updateApplicationByUser.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<ArgumentOutOfRangeException>();
    }


    [Theory]
    [InlineData(null)]
    [InlineData(0)]

    public async Task UpdateAsync_WhenApplicationForIdIsInvaild_ThrowsArgumentOutOfRangeException(short id)
    {
        //Arrange
        UpdateApplicationByUser updateRequest = _fixture.Build<UpdateApplicationByUser>()
                                                .With(app => app.ApplicationTypeId, id)
                                                .Create();

        //Act
        Func<Task> action = async () => await _updateApplicationByUser.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<ArgumentOutOfRangeException>();
    }

    [Fact]
    public async Task UpdateAsync_ApplicationDoesNotExist_ThrowsDoseNotExistException()
    {
        //Arrange
        UpdateApplicationByUser updateRequest = _fixture.Build<UpdateApplicationByUser>()
                                                        .Create();

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<LicenseApplication, bool>>>()))
            .ReturnsAsync(null as LicenseApplication);

        //Act
        Func<Task> action = async () => await _updateApplicationByUser.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<DoesNotExistException>();
    }


    [Fact]
    public async Task UpdateAsync_WhenUpdateRepositoryFails_ThrowsFailedToUpdateException()
    {

        //Arrange
        UpdateApplicationByUser updateRequest = _fixture.Build<UpdateApplicationByUser>()
                                                        .Create();


        LicenseApplication dummyLicenseApplication = new LicenseApplication() { };

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<LicenseApplication, bool>>>()))
            .ReturnsAsync(dummyLicenseApplication);

        _mapper.Setup(temp => temp.Map<LicenseApplication>(It.IsAny<UpdateApplicationByUser>()))
                        .Returns(dummyLicenseApplication);

        _updateRepository.Setup(temp => temp.UpdateAsync(It.IsAny<LicenseApplication>()))
                    .ReturnsAsync(null as LicenseApplication);
        //Act
        Func<Task> action = async () => await _updateApplicationByUser.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<FailedToUpdateException>();
    }


    [Fact]
    public async Task UpdateAsync_WhenFailToMappingFromApplicationlToApplicationDTO_ThrowsAutoMapperMappingException()
    {

        //Arrange
        UpdateApplicationByUser updateRequest = _fixture.Build<UpdateApplicationByUser>()
                                                        .Create();

        LicenseApplication existsLicenseApplication = _fixture.Build<LicenseApplication>()
                                 .With(app => app.ApplicationFees, null as ApplicationFees)
                                 .With(app => app.Employee, null as Employee)
                                 .With(app => app.User, null as Models.Users.User)
                              .Create();

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<LicenseApplication, bool>>>()))
            .ReturnsAsync(existsLicenseApplication);

        _updateRepository.Setup(temp => temp.UpdateAsync(It.IsAny<LicenseApplication>()))
            .ReturnsAsync(existsLicenseApplication);

        _mapper.Setup(temp => temp.Map<LicenseApplication>(It.IsAny<UpdateApplicationByUser>()))
                        .Returns(existsLicenseApplication);
        //Act
        Func<Task> action = async () => await _updateApplicationByUser.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<AutoMapperMappingException>();
    }



    [Fact]
    public async Task UpdateAsync_WhenSuccessfullyUpdate_ReturnsApplicationDTOForuser()
    {

        //Arrange
        UpdateApplicationByUser updateRequest = _fixture.Build<UpdateApplicationByUser>()
                                                        .Create();

        LicenseApplication updatedLicenseApplication = new LicenseApplication()
        {
            Id = updateRequest.Id,
            ApplicantUserId = updateRequest.ApplicantUserId,
            ApplicationTypeId = updateRequest.ApplicationTypeId,
            ApplicationForId = updateRequest.ApplicationForId,
        };

        LicenseApplication dummyLicenseApplication = new() { };

        ApplicationDTOForUser applicationDTOForUser = new ApplicationDTOForUser()
        {
            Id = updateRequest.Id,
            ApplicantUserId = updatedLicenseApplication.ApplicantUserId,
            ApplicationTypeId = updatedLicenseApplication.ApplicationTypeId,
            ApplicationForId = updatedLicenseApplication.ApplicationForId,
            LastStatusDate = updatedLicenseApplication.LastStatusDate,
            ApplicationStatus = updatedLicenseApplication.ApplicationStatus,
            ApplicationDate = updatedLicenseApplication.ApplicationDate,
            PaidFees = updatedLicenseApplication.PaidFees,
        };

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<LicenseApplication, bool>>>()))
            .ReturnsAsync(dummyLicenseApplication);

        _mapper.Setup(temp => temp.Map<LicenseApplication>(It.IsAny<UpdateApplicationByUser>()))
                        .Returns(dummyLicenseApplication);

        _updateRepository.Setup(temp => temp.UpdateAsync(It.IsAny<LicenseApplication>()))
                    .ReturnsAsync(updatedLicenseApplication);

        _mapper.Setup(temp => temp.Map<ApplicationDTOForUser>(It.IsAny<LicenseApplication>()))
                     .Returns(applicationDTOForUser);

        //Act
        var result = await _updateApplicationByUser.UpdateAsync(updateRequest);

        //Assert
        result.Should().BeEquivalentTo(applicationDTOForUser);
    }
}