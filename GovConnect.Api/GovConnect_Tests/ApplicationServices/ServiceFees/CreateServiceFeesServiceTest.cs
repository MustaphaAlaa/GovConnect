using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IRepository.IGenericRepositories;
using IServices.IApplicationServices.Fees;
using ModelDTO.ApplicationDTOs.Fees;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices;
using Services.ApplicationServices.Fees;
using System.Linq.Expressions;

namespace GovConnect_Tests.ApplicationServices;

public class CreateServiceFeesServiceTest
{
    private readonly IFixture _fixture;
    private readonly Mock<IMapper> _mapper;

    private readonly Mock<IGetRepository<ServiceFees>> _getRepository;
    private readonly Mock<ICreateRepository<ServiceFees>> _createRepository;

    private readonly ICreateServiceFees _iCreateServiceFees;

    public CreateServiceFeesServiceTest()
    {
        _fixture = new Fixture();
        _mapper = new Mock<IMapper>();

        _getRepository = new Mock<IGetRepository<ServiceFees>>();
        _createRepository = new Mock<ICreateRepository<ServiceFees>>();

        _iCreateServiceFees = new CreateServiceFeesService(_mapper.Object, _getRepository.Object, _createRepository.Object);
    }

    [Fact]
    public async Task CreateApplictaionFees_WhenCreateRequestObjIsNull_ThrowsArgumentException()
    {
        //Arrange && Act
        Func<Task> action = async () => await _iCreateServiceFees.CreateAsync(null);

        //Assest

        await action.Should().ThrowAsync<ArgumentNullException>();
    }


    [Theory]
    [InlineData(0)]

    public async Task CreateApplicationFees_WhenTypeIdIsInvalid_ThrowsArgumentOutOfRangeException(byte id)
    {
        //Arrange
        ServiceFeesDTO createReq = _fixture.Build<ServiceFeesDTO>()
            .With(app => app.ServicePurposeId, id)
            .Create();
        //Act
        Func<Task> action = async () => await _iCreateServiceFees.CreateAsync(createReq);

        //Assert
        await action.Should().ThrowAsync<ArgumentOutOfRangeException>();

    }


    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task CreateApplicationFees_WhenForIdIsInvalid_ThrowsArgumentOutOfRangeException(int id)
    {
        //Arrange
        ServiceFeesDTO createReq = _fixture.Build<ServiceFeesDTO>()
               .With(app => app.ServiceCategoryId, id)
               .Create();

        //Act
        Func<Task> action = async () => await _iCreateServiceFees.CreateAsync(createReq);

        //Assert
        await action.Should().ThrowAsync<ArgumentOutOfRangeException>();
    }

    [Fact]
    public async Task CreateApplicationFees_WhenFeesValueIsInvalid_ThrowsArgumentOutOfRangeException()
    {
        //Arrange
        ServiceFeesDTO createReq = _fixture.Build<ServiceFeesDTO>()
               .With(app => app.Fees, -4)
               .Create();

        //Act
        Func<Task> action = async () => await _iCreateServiceFees.CreateAsync(createReq);

        //Assert
        await action.Should().ThrowAsync<ArgumentOutOfRangeException>();
    }


    [Fact]
    public async Task CreateApplicationFees_WhenApplicationFeesIsAlreadyExists_ThrowsException()
    {
        //Arrange
        ServiceFeesDTO createReq = _fixture.Create<ServiceFeesDTO>();
        ServiceFees existServiceFees = _fixture.Build<ServiceFees>()
            .With(app => app.ServiceCategory, null as ServiceCategory)
            .With(app => app.ServicePurpose, null as ServicePurpose)
            .With(app => app.Applications, null as ICollection<Application>)
            .Create();

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ServiceFees, bool>>>()))
            .ReturnsAsync(existServiceFees);

        //Act
        Func<Task> action = async () => await _iCreateServiceFees.CreateAsync(createReq);

        //Assert
        await action.Should().ThrowAsync<Exception>();
    }



    [Fact]
    public async Task CreateApplicationFees_WhenFailedToMappingFromDtoToModel_AutoMapperMappingException()
    {
        //Arrange
        ServiceFeesDTO createReq = _fixture.Create<ServiceFeesDTO>();

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ServiceFees, bool>>>()))
            .ReturnsAsync(null as ServiceFees);

        _mapper.Setup(temp => temp.Map<ServiceFees>(It.IsAny<ServiceFeesDTO>()))
            .Returns(null as ServiceFees);

        //Act
        Func<Task> action = async () => await _iCreateServiceFees.CreateAsync(createReq);

        //Assert
        await action.Should().ThrowAsync<AutoMapperMappingException>();
    }

    [Fact]
    public async Task CreateApplicationFees_WhenFailedToCreateNewApplicationFees_ThrowsException()
    {
        //Arrange
        ServiceFeesDTO createReq = _fixture.Create<ServiceFeesDTO>();
        ServiceFees mappedCreatedReq = _fixture.Build<ServiceFees>()
            .With(app => app.ServiceCategory, null as ServiceCategory)
            .With(app => app.ServicePurpose, null as ServicePurpose)
            .With(app => app.Applications, null as ICollection<Application>)
            .Create();

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ServiceFees, bool>>>()))
            .ReturnsAsync(null as ServiceFees);

        _mapper.Setup(temp => temp.Map<ServiceFees>(It.IsAny<ServiceFeesDTO>()))
            .Returns(mappedCreatedReq);


        _createRepository.Setup(temp => temp.CreateAsync(It.IsAny<ServiceFees>()))
            .ReturnsAsync(null as ServiceFees);

        //Act
        Func<Task> action = async () => await _iCreateServiceFees.CreateAsync(createReq);

        //Assert
        await action.Should().ThrowAsync<Exception>();
    }


    [Fact]
    public async Task CreateApplicationFees_WhenFailedToMappingFromModelToDTO_AutoMapperMappingException()
    {
        //Arrange
        ServiceFeesDTO createReq = _fixture.Create<ServiceFeesDTO>();
        ServiceFees mappedCreatedReq = new()
        {
            ServiceCategoryId = createReq.ServiceCategoryId,
            ServicePurposeId = createReq.ServicePurposeId,
            Fees = createReq.Fees,
            LastUpdate = createReq.LastUpdate
        };

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ServiceFees, bool>>>()))
            .ReturnsAsync(null as ServiceFees);

        _mapper.Setup(temp => temp.Map<ServiceFees>(It.IsAny<ServiceFeesDTO>()))
            .Returns(mappedCreatedReq);

        _createRepository.Setup(temp => temp.CreateAsync(It.IsAny<ServiceFees>()))
            .ReturnsAsync(mappedCreatedReq);

        _mapper.Setup(temp => temp.Map<ServiceFeesDTO>(It.IsAny<ServiceFees>()))
             .Returns<ServiceFeesDTO>(null);

        //Act
        Func<Task> action = async () => await _iCreateServiceFees.CreateAsync(createReq);

        //Assert
        await action.Should().ThrowAsync<AutoMapperMappingException>();
    }


    [Fact]
    public async Task CreateApplicationFees_WhenSuccessfullyCreateNewApplicationFees_ReturnApplicationDTO()
    {
        //Arrange
        ServiceFeesDTO createReq = _fixture.Create<ServiceFeesDTO>();

        ServiceFees mappedCreatedReq = new ServiceFees
        {
            ServiceCategoryId = createReq.ServiceCategoryId,
            ServicePurposeId = createReq.ServicePurposeId,
            Fees = createReq.Fees,
            LastUpdate = createReq.LastUpdate
        };

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ServiceFees, bool>>>()))
            .ReturnsAsync(null as ServiceFees);

        _mapper.Setup(temp => temp.Map<ServiceFees>(It.IsAny<ServiceFeesDTO>()))
            .Returns(mappedCreatedReq);

        _createRepository.Setup(temp => temp.CreateAsync(It.IsAny<ServiceFees>()))
            .ReturnsAsync(mappedCreatedReq);


        _mapper.Setup(temp => temp.Map<ServiceFeesDTO>(It.IsAny<ServiceFees>()))
           .Returns((ServiceFees source) => new ServiceFeesDTO
           {
               ServiceCategoryId = source.ServiceCategoryId,
               ServicePurposeId = source.ServicePurposeId,
               Fees = source.Fees,
               LastUpdate = source.LastUpdate
           });


        //Act
        var result = await _iCreateServiceFees.CreateAsync(createReq);

        //Assert
        result.Should().BeEquivalentTo(createReq);

    }
}
