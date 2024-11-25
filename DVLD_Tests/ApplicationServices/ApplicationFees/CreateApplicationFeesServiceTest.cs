using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.Fees;
using ModelDTO.ApplicationDTOs.Fees;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices;
using Services.ApplicationServices.Fees;
using System.Linq.Expressions;

namespace DVLD_Tests.ApplicationServices;

public class CreateApplicationFeesServiceTest
{
    private readonly IFixture _fixture;
    private readonly Mock<IMapper> _mapper;

    private readonly Mock<IGetRepository<ApplicationFees>> _getRepository;
    private readonly Mock<ICreateRepository<ApplicationFees>> _createRepository;

    private readonly ICreateApplicationFees _createApplicationFees;

    public CreateApplicationFeesServiceTest()
    {
        _fixture = new Fixture();
        _mapper = new Mock<IMapper>();

        _getRepository = new Mock<IGetRepository<ApplicationFees>>();
        _createRepository = new Mock<ICreateRepository<ApplicationFees>>();

        _createApplicationFees = new CreateApplicationFeesService(_mapper.Object, _getRepository.Object, _createRepository.Object);
    }

    [Fact]
    public async Task CreateApplictaionFees_ReqObjIsNull_ThrowsArgumentException()
    {
        //Arrange && Act
        Func<Task> action = async () => await _createApplicationFees.CreateAsync(null);

        //Assest

        await action.Should().ThrowAsync<ArgumentNullException>();
    }


    [Theory]
    [InlineData(0)]

    public async Task CreateApplicationFees_TypeIdIsInvalid_ThrowsArgumentOutOfRangeException(byte id)
    {
        //Arrange
        ApplicationFeesDTO createReq = _fixture.Build<ApplicationFeesDTO>()
            .With(app => app.ApplicationTypeId, id)
            .Create();
        //Act
        Func<Task> action = async () => await _createApplicationFees.CreateAsync(createReq);

        //Assert
        await action.Should().ThrowAsync<ArgumentOutOfRangeException>();

    }


    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task CreateApplicationFees_ForIdIsInvalid_ThrowsArgumentOutOfRangeException(int id)
    {
        //Arrange
        ApplicationFeesDTO createReq = _fixture.Build<ApplicationFeesDTO>()
               .With(app => app.ApplicationForId, id)
               .Create();

        //Act
        Func<Task> action = async () => await _createApplicationFees.CreateAsync(createReq);

        //Assert
        await action.Should().ThrowAsync<ArgumentOutOfRangeException>();
    }

    [Fact]
    public async Task CreateApplicationFees_FeesValueIsInvalid_ThrowsArgumentOutOfRangeException()
    {
        //Arrange
        ApplicationFeesDTO createReq = _fixture.Build<ApplicationFeesDTO>()
               .With(app => app.ApplicationForId, -4)
               .Create();

        //Act
        Func<Task> action = async () => await _createApplicationFees.CreateAsync(createReq);

        //Assert
        await action.Should().ThrowAsync<ArgumentOutOfRangeException>();
    }


    [Fact]
    public async Task CreateApplicationFees_ApplicationFeesIsAlreadyExists_ThrowsException()
    {
        //Arrange
        ApplicationFeesDTO createReq = _fixture.Create<ApplicationFeesDTO>();
        ApplicationFees existApplicationFees = _fixture.Build<ApplicationFees>()
            .With(app => app.ApplicationFor, null as ApplicationFor)
            .With(app => app.ApplicationType, null as ApplicationType)
            .With(app => app.Applications, null as ICollection<Application>)
            .Create();

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationFees, bool>>>()))
            .ReturnsAsync(existApplicationFees);

        //Act
        Func<Task> action = async () => await _createApplicationFees.CreateAsync(createReq);

        //Assert
        await action.Should().ThrowAsync<Exception>();
    }



    [Fact]
    public async Task CreateApplicationFees_FailureToMappingFromDtoToModel_AutoMapperMappingException()
    {
        //Arrange
        ApplicationFeesDTO createReq = _fixture.Create<ApplicationFeesDTO>();

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationFees, bool>>>()))
            .ReturnsAsync(null as ApplicationFees);

        _mapper.Setup(temp => temp.Map<ApplicationFees>(It.IsAny<ApplicationFeesDTO>()))
            .Returns(null as ApplicationFees);

        //Act
        Func<Task> action = async () => await _createApplicationFees.CreateAsync(createReq);

        //Assert
        await action.Should().ThrowAsync<AutoMapperMappingException>();
    }

    [Fact]
    public async Task CreateApplicationFees_FailureToCreateNewApplicationFees_ThrowsException()
    {
        //Arrange
        ApplicationFeesDTO createReq = _fixture.Create<ApplicationFeesDTO>();
        ApplicationFees mappedCreatedReq = _fixture.Build<ApplicationFees>()
            .With(app => app.ApplicationFor, null as ApplicationFor)
            .With(app => app.ApplicationType, null as ApplicationType)
            .With(app => app.Applications, null as ICollection<Application>)
            .Create();

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationFees, bool>>>()))
            .ReturnsAsync(null as ApplicationFees);

        _mapper.Setup(temp => temp.Map<ApplicationFees>(It.IsAny<ApplicationFeesDTO>()))
            .Returns(mappedCreatedReq);


        _createRepository.Setup(temp => temp.CreateAsync(It.IsAny<ApplicationFees>()))
            .ReturnsAsync(null as ApplicationFees);

        //Act
        Func<Task> action = async () => await _createApplicationFees.CreateAsync(createReq);

        //Assert
        await action.Should().ThrowAsync<Exception>();
    }


    [Fact]
    public async Task CreateApplicationFees_FailureToMappingFromModelToDTO_AutoMapperMappingException()
    {
        //Arrange
        ApplicationFeesDTO createReq = _fixture.Create<ApplicationFeesDTO>();
        ApplicationFees mappedCreatedReq = new()
        {
            ApplicationForId = createReq.ApplicationForId,
            ApplicationTypeId = createReq.ApplicationTypeId,
            Fees = createReq.Fees,
            LastUpdate = createReq.LastUdpate
        };

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationFees, bool>>>()))
            .ReturnsAsync(null as ApplicationFees);

        _mapper.Setup(temp => temp.Map<ApplicationFees>(It.IsAny<ApplicationFeesDTO>()))
            .Returns(mappedCreatedReq);

        _createRepository.Setup(temp => temp.CreateAsync(It.IsAny<ApplicationFees>()))
            .ReturnsAsync(mappedCreatedReq);

        _mapper.Setup(temp => temp.Map<ApplicationFeesDTO>(It.IsAny<ApplicationFees>()))
             .Returns<ApplicationFeesDTO>(null);

        //Act
        Func<Task> action = async () => await _createApplicationFees.CreateAsync(createReq);

        //Assert
        await action.Should().ThrowAsync<AutoMapperMappingException>();
    }


    [Fact]
    public async Task CreateApplicationFees_CreateNewApplicationFees_ReturnApplicationDTO()
    {
        //Arrange
        ApplicationFeesDTO createReq = _fixture.Create<ApplicationFeesDTO>();

        ApplicationFees mappedCreatedReq = new ApplicationFees
        {
            ApplicationForId = createReq.ApplicationForId,
            ApplicationTypeId = createReq.ApplicationTypeId,
            Fees = createReq.Fees,
            LastUpdate = createReq.LastUdpate
        };

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationFees, bool>>>()))
            .ReturnsAsync(null as ApplicationFees);

        _mapper.Setup(temp => temp.Map<ApplicationFees>(It.IsAny<ApplicationFeesDTO>()))
            .Returns(mappedCreatedReq);

        _createRepository.Setup(temp => temp.CreateAsync(It.IsAny<ApplicationFees>()))
            .ReturnsAsync(mappedCreatedReq);


        _mapper.Setup(temp => temp.Map<ApplicationFeesDTO>(It.IsAny<ApplicationFees>()))
           .Returns((ApplicationFees source) => new ApplicationFeesDTO
           {
               ApplicationForId = source.ApplicationForId,
               ApplicationTypeId = source.ApplicationTypeId,
               Fees = source.Fees,
               LastUdpate = source.LastUpdate
           });


        //Act
        var result = await _createApplicationFees.CreateAsync(createReq);

        //Assert
        result.Should().BeEquivalentTo(createReq);

    }
}
