using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.User;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Models.Users;
using Moq;
using Services.ApplicationServices.Services.UserAppServices;
using Services.Execptions;
using System.Linq.Expressions;

namespace DVLD_Tests.ApplicationServices.Services.UserTests;

public class CreateApplcationServiceTEST
{
    private readonly IFixture _fixture;

    private readonly ICreateApplication _createApplication;
    private readonly Mock<ICreateRepository<Application>> _createRepository;
    private readonly Mock<IGetRepository<Application>> _getApplicationRepository;
    private readonly Mock<IGetRepository<ApplicationFees>> _getAppFeesRepository;
    private readonly Mock<IMapper> _mapper;

    public CreateApplcationServiceTEST()
    {
        _fixture = new Fixture();
        _mapper = new Mock<IMapper>();

        _getAppFeesRepository = new Mock<IGetRepository<ApplicationFees>>();

        _createRepository = new Mock<ICreateRepository<Application>>();
        _getApplicationRepository = new Mock<IGetRepository<Application>>();

        _createApplication = new CreateApplicationService(_createRepository.Object,
                                _getApplicationRepository.Object,
                                _getAppFeesRepository.Object,
                                _mapper.Object);
    }

    [Fact]
    public async Task CreateAsync_CreateRequestObjIsNull_ThrowsArgumentNullException()
    {
        //Arrang
        CreateApplicationRequest createRequest = null;

        //Act
        Func<Task> action = async () => await _createApplication.CreateAsync(createRequest);

        //Asert
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task CreateAsync_ApplicantUserIdIsEmpty_ThrowsArgumentException()
    {
        //Arrang
        CreateApplicationRequest createRequest = _fixture.Build<CreateApplicationRequest>()
                                                 .With(app => app.ApplicantUserId, Guid.Empty)
                                                 .Create();
        //Act
        Func<Task> action = async () => await _createApplication.CreateAsync(createRequest);

        //Asert
        await action.Should().ThrowAsync<ArgumentException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData(0)]
    public async Task CreateAsync_ApplicationTypeIdInvalid_ThrowsArgumentException(byte Id)
    {
        //Arrang
        CreateApplicationRequest createRequest = _fixture.Build<CreateApplicationRequest>()
                                                 .With(app => app.ApplicationTypeId, Id)
                                                 .Create();
        //Act
        Func<Task> action = async () => await _createApplication.CreateAsync(createRequest);

        //Asert
        await action.Should().ThrowAsync<ArgumentException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task CreateAsync_ApplicationForIdInvalid_ThrowsArgumentException(short Id)
    {
        //Arrang
        CreateApplicationRequest createRequest = _fixture.Build<CreateApplicationRequest>()
                                                 .With(app => app.ApplicationForId, Id)
                                                 .Create();
        //Act
        Func<Task> action = async () => await _createApplication.CreateAsync(createRequest);

        //Asert
        await action.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task CreateAsync_WhenApplicationFeesDoesNotExist_ThrowsDoseNotExistException()
    {
        //Arrang
        CreateApplicationRequest createRequest = _fixture.Build<CreateApplicationRequest>()
                                                 .Create();
        _getAppFeesRepository
            .Setup(temp => temp
            .GetAsync(It.IsAny<Expression<Func<ApplicationFees, bool>>>()))
            .ReturnsAsync(null as ApplicationFees);

        //Act
        Func<Task> action = async () => await _createApplication.CreateAsync(createRequest);

        //Asert
        await action.Should().ThrowAsync<DoesNotExistException>();
    }

    [Theory]
    [InlineData(ApplicationStatus.Finalized)]
    [InlineData(ApplicationStatus.Pending)]
    [InlineData(ApplicationStatus.InProgress)]
    public async Task CreateAsync_ApplicationExistAndStatusIsNotApprovedOrRejected_ThrowsInvalidOperationException(ApplicationStatus statue)
    {
        //Arrang
        CreateApplicationRequest createRequest = _fixture.Build<CreateApplicationRequest>()
                                                 .Create();

        Application application = _fixture.Build<Application>()
            .With(app => app.ApplicantUserId, createRequest.ApplicantUserId)
            .With(app => app.ApplicationStatus, (byte)statue)
            .With(app => app.Employee, null as Employee)
            .With(app => app.User, null as Models.Users.User)
            .With(app => app.ApplicationFees, null as ApplicationFees)
            .Create();

        _getApplicationRepository
            .Setup(temp => temp
            .GetAsync(It.IsAny<Expression<Func<Application, bool>>>()))
            .ReturnsAsync(application);

        _getAppFeesRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationFees, bool>>>()))
            .ReturnsAsync(new ApplicationFees() { });

        //Act
        Func<Task> action = async () => await _createApplication.CreateAsync(createRequest);

        //Asert
        await action.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task CreateAsync_FailureToMapFromCreateApplicationDTOToApplicationModel_ThrowsAutoMapperMappingException()
    {
        //Arrang
        CreateApplicationRequest createRequest = _fixture.Build<CreateApplicationRequest>()
                                                 .Create();

        _getApplicationRepository
           .Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<Application, bool>>>()))
           .ReturnsAsync(null as Application);

        _getAppFeesRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationFees, bool>>>()))
            .ReturnsAsync(new ApplicationFees
            {
                ApplicationForId = createRequest.ApplicationForId,
                ApplicationTypeId = createRequest.ApplicationTypeId,
                Fees = 44,
            });

        _mapper.Setup(temp => temp.Map<Application>(It.IsAny<CreateApplicationRequest>()))
            .Returns(null as Application);

        //Act
        Func<Task> action = async () => await _createApplication.CreateAsync(createRequest);

        //Asert
        await action.Should().ThrowAsync<AutoMapperMappingException>();
    }

    [Fact]
    public async Task CreateAsync_FailurToCreateApplication_ThrowsFailedToCreateException()
    {
        //Arrang
        CreateApplicationRequest createRequest = _fixture.Build<CreateApplicationRequest>()
                                                 .Create();

        Application application = _fixture.Build<Application>()
             .With(app => app.ApplicantUserId, createRequest.ApplicantUserId)
             .With(app => app.Employee, null as Employee)
             .With(app => app.User, null as User)
             .With(app => app.ApplicationFees, null as ApplicationFees)
             .With(app => app.ApplicationStatus, (byte)ApplicationStatus.Approved)
             .Create();

        _getApplicationRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<Application, bool>>>()))
            .ReturnsAsync(application);

        _getAppFeesRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationFees, bool>>>()))
           .ReturnsAsync(new ApplicationFees
           {
               ApplicationForId = createRequest.ApplicationForId,
               ApplicationTypeId = createRequest.ApplicationTypeId,
               Fees = 44,
           });

        _mapper.Setup(temp => temp.Map<Application>(It.IsAny<CreateApplicationRequest>()))
            .Returns(application);

        _createRepository.Setup(temp => temp.CreateAsync(It.IsAny<Application>()))
            .ReturnsAsync(null as Application);

        //Act
        Func<Task> action = async () => await _createApplication.CreateAsync(createRequest);

        //Asert
        await action.Should().ThrowAsync<FailedToCreateException>();
    }

    [Fact]
    public async Task CreateAsync_FailureToMapFromApplicationModelToApplicationDTOForUser_ThrowsAutoMapperMappingException()
    {
        //Arrang
        CreateApplicationRequest createRequest = _fixture.Build<CreateApplicationRequest>()
                                                 .Create();

        Application application = _fixture.Build<Application>()
            .With(app => app.ApplicantUserId, createRequest.ApplicantUserId)
            .With(app => app.ApplicationForId, createRequest.ApplicationForId)
            .With(app => app.ApplicationTypeId, createRequest.ApplicationForId)
            .With(app => app.Employee, null as Employee)
            .With(app => app.User, null as Models.Users.User)
            .With(app => app.ApplicationFees, null as ApplicationFees)
            .Create();

        _getApplicationRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<Application, bool>>>()))
            .ReturnsAsync(null as Application);

        _getAppFeesRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationFees, bool>>>()))
           .ReturnsAsync(new ApplicationFees
           {
               ApplicationForId = createRequest.ApplicationForId,
               ApplicationTypeId = createRequest.ApplicationTypeId,
               Fees = 44,
           });

        _createRepository.Setup(temp => temp.CreateAsync(It.IsAny<Application>()))
            .ReturnsAsync(application);

        _mapper.Setup(temp => temp.Map<ApplicationDTOForUser>(It.IsAny<Application>()))
            .Returns(null as ApplicationDTOForUser);

        //Act
        Func<Task> action = async () => await _createApplication.CreateAsync(createRequest);

        //Asert
        await action.Should().ThrowAsync<AutoMapperMappingException>();
    }

    [Fact]
    public async Task CreateAsync_ApplicationisCreated_ReturnsApplicationDToForUser()
    {
        //Arrang
        CreateApplicationRequest createRequest = _fixture.Build<CreateApplicationRequest>()
                                                 .Create();

        Application application = _fixture.Build<Application>()
            .With(app => app.ApplicantUserId, createRequest.ApplicantUserId)
            .With(app => app.ApplicationForId, createRequest.ApplicationForId)
            .With(app => app.ApplicationTypeId, createRequest.ApplicationForId)
            .With(app => app.Employee, null as Employee)
            .With(app => app.User, null as Models.Users.User)
            .With(app => app.ApplicationFees, null as ApplicationFees)
            .Create();

        ApplicationDTOForUser applicationDtoForUser = _fixture.Build<ApplicationDTOForUser>()
            .With(app => app.ApplicantUserId, createRequest.ApplicantUserId)
            .With(app => app.ApplicationForId, createRequest.ApplicationForId)
            .With(app => app.ApplicationTypeId, createRequest.ApplicationForId)
            .Create();

        _getApplicationRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<Application, bool>>>()))
            .ReturnsAsync(null as Application);

        _getAppFeesRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationFees, bool>>>()))
           .ReturnsAsync(new ApplicationFees
           {
               ApplicationForId = createRequest.ApplicationForId,
               ApplicationTypeId = createRequest.ApplicationTypeId,
               Fees = 44,
           });

        _createRepository.Setup(temp => temp.CreateAsync(It.IsAny<Application>()))
            .ReturnsAsync(application);

        _mapper.Setup(temp => temp.Map<Application>(It.IsAny<CreateApplicationRequest>()))
            .Returns(application);

        _mapper.Setup(temp => temp.Map<ApplicationDTOForUser>(It.IsAny<Application>()))
            .Returns(applicationDtoForUser);

        //Act
        var result = await _createApplication.CreateAsync(createRequest);

        //Asert
        result.Should().BeEquivalentTo(applicationDtoForUser);
    }
}