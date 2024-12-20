using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.Fees;
using ModelDTO.ApplicationDTOs.Fees;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices.Fees;
using System.Linq.Expressions;

namespace GovConnect_Tests.ApplicationServices;

public class UpdateeApplicationFeesServiceTEST
{
    private readonly IFixture _fixture;
    private readonly Mock<IMapper> _mapper;

    private readonly Mock<IGetRepository<ServiceFees>> _getRepository;
    private readonly Mock<IUpdateRepository<ServiceFees>> _updateRepository;

    private readonly IUpdateServiceFees _iUpdateServiceFees;

    public UpdateeApplicationFeesServiceTEST()
    {
        _fixture = new Fixture();
        _mapper = new Mock<IMapper>();

        _getRepository = new Mock<IGetRepository<ServiceFees>>();
        _updateRepository = new Mock<IUpdateRepository<ServiceFees>>();

        _iUpdateServiceFees = new UpdateServiceFeesService(_mapper.Object, _updateRepository.Object, _getRepository.Object);
    }

    [Fact]
    public async Task UpdateApplictaionFees_ReqObjIsNull_ThrowsArgumentException()
    {
        //Arrange && Act
        Func<Task> action = async () => await _iUpdateServiceFees.UpdateAsync(null);

        //Assest
        await action.Should().ThrowAsync<ArgumentNullException>();
    }


    [Theory]
    [InlineData(0)]
    public async Task UpdateApplicationFees_TypeIdIsInvalid_ThrowsArgumentOutOfRangeException(int id)
    {
        //Arrange
        ServiceFeesDTO updateRequest = _fixture.Build<ServiceFeesDTO>()
            .With(app => app.ApplicationPuropseId, id)
            .Create();

        //Act
        Func<Task> action = async () => await _iUpdateServiceFees.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<ArgumentOutOfRangeException>();

    }


    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task UpdateApplicationFees_ForIdIsInvalid_ThrowsArgumentOutOfRangeException(int id)
    {
        //Arrange
        ServiceFeesDTO updateRequest = _fixture.Build<ServiceFeesDTO>()
               .With(app => app.ServiceCategoryId, id)
               .Create();

        //Act
        Func<Task> action = async () => await _iUpdateServiceFees.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<ArgumentOutOfRangeException>();
    }

    [Fact]
    public async Task UpdateApplicationFees_FeesValueIsInvalid_ThrowsArgumentOutOfRangeException()
    {
        //Arrange
        ServiceFeesDTO createReq = _fixture.Build<ServiceFeesDTO>()
               .With(app => app.ServiceCategoryId, -4)
               .Create();

        //Act
        Func<Task> action = async () => await _iUpdateServiceFees.UpdateAsync(createReq);

        //Assert
        await action.Should().ThrowAsync<ArgumentOutOfRangeException>();
    }

    [Fact]
    public async Task UpdateApplicationFees_ApplicationFeesDoesNotExists_ThrowsException()
    {
        //Arrange
        ServiceFeesDTO updateRequest = _fixture.Create<ServiceFeesDTO>();


        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ServiceFees, bool>>>()))
            .ReturnsAsync(null as ServiceFees);

        //Act
        Func<Task> action = async () => await _iUpdateServiceFees.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<Exception>();
    }


    [Fact]
    public async Task UpdateApplicationFees_InvalidDate_ThrowsException()
    {
        //Arrange
        ServiceFees existServiceFees = new ServiceFees()
        {
            ServicePurposeId = 1,
            ServiceCategoryId = 200,
            Fees = 10000,
            LastUpdate = new DateTime(2005, 11, 30)
        };

        ServiceFeesDTO updateRequest = _fixture.Build<ServiceFeesDTO>()
            .With(appFees => appFees.ApplicationPuropseId, existServiceFees.ServicePurposeId)
            .With(appFees => appFees.ServiceCategoryId, existServiceFees.ServiceCategoryId)
            .With(appFees => appFees.LastUpdate, existServiceFees.LastUpdate - TimeSpan.FromDays(10))
            .Create();


        //Act
        Func<Task> action = async () => await _iUpdateServiceFees.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<Exception>();
    }



    [Fact]
    public async Task UpdateApplicationFees_FailureToMappingFromDtoToModel_AutoMapperMappingException()
    {

        //Arrange
        ServiceFees existServiceFees = new ServiceFees()
        {
            ServicePurposeId = 1,
            ServiceCategoryId = 2,
            Fees = 12,
            LastUpdate = new DateTime(2021, 5, 16)
        };

        ServiceFeesDTO updateRequest = new()
        {
            ApplicationPuropseId = existServiceFees.ServicePurposeId,
            ServiceCategoryId = existServiceFees.ServiceCategoryId,
            Fees = 120,
            LastUpdate = DateTime.Now,
        };

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ServiceFees, bool>>>()))
            .ReturnsAsync(existServiceFees);

        _mapper.Setup(temp => temp.Map<ServiceFees>(It.IsAny<ServiceFeesDTO>()))
            .Returns(null as ServiceFees);

        //Act
        Func<Task> action = async () => await _iUpdateServiceFees.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<AutoMapperMappingException>();
    }

    [Fact]
    public async Task UpdateApplicationFees_FailureToUpdateApplicationFees_ThrowsException()
    {
        //Arrange
        ServiceFeesDTO updateRequest = _fixture.Create<ServiceFeesDTO>();
        ServiceFees existServiceFees = new ServiceFees()
        {
            ServicePurposeId = 1,
            ServiceCategoryId = 5,
            Fees = 102,
            LastUpdate = updateRequest.LastUpdate - TimeSpan.FromDays(2)
        };

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ServiceFees, bool>>>()))
            .ReturnsAsync(existServiceFees);

        _mapper.Setup(temp => temp.Map<ServiceFees>(It.IsAny<ServiceFeesDTO>()))
            .Returns(existServiceFees);


        _updateRepository.Setup(temp => temp.UpdateAsync(It.IsAny<ServiceFees>()))
            .ReturnsAsync(null as ServiceFees);

        //Act
        Func<Task> action = async () => await _iUpdateServiceFees.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<Exception>();
    }


    [Fact]
    public async Task UpdateApplicationFees_FailureToMappingFromModelToDTO_AutoMapperMappingException()
    {
        //Arrange
        ServiceFeesDTO updateRequest = new()
        {
            ApplicationPuropseId = 1,
            ServiceCategoryId = 5,
            Fees = 80,
            LastUpdate = DateTime.Now
        };
        ServiceFees existServiceFees = new ServiceFees()
        {
            ServicePurposeId = 1,
            ServiceCategoryId = 5,
            Fees = 102,
            LastUpdate = updateRequest.LastUpdate - TimeSpan.FromDays(2)
        };


        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ServiceFees, bool>>>()))
            .ReturnsAsync(existServiceFees);

        _mapper.Setup(temp => temp.Map<ServiceFees>(It.IsAny<ServiceFeesDTO>()))
            .Returns((ServiceFeesDTO source) => new ServiceFees
            {
                ServiceCategoryId = source.ServiceCategoryId,
                ServicePurposeId = source.ApplicationPuropseId,
                Fees = source.Fees,
                LastUpdate = source.LastUpdate
            });

        _updateRepository.Setup(temp => temp.UpdateAsync(It.IsAny<ServiceFees>()))
            .ReturnsAsync(new ServiceFees()
            {
                ServicePurposeId = updateRequest.ApplicationPuropseId,
                ServiceCategoryId = updateRequest.ServiceCategoryId,
                Fees = updateRequest.Fees,
                LastUpdate = updateRequest.LastUpdate
            });

        _mapper.Setup(temp => temp.Map<ServiceFeesDTO>(It.IsAny<ServiceFees>()))
             .Returns<ServiceFeesDTO>(null);

        //Act
        Func<Task> action = async () => await _iUpdateServiceFees.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<AutoMapperMappingException>();
    }


    [Fact]
    public async Task UpdateApplicationFees_updateApplicationFees_ReturnApplicationDTO()
    {
        //Arrange
        //Arrange
        ServiceFeesDTO updateRequest = new()
        {
            ApplicationPuropseId = 1,
            ServiceCategoryId = 5,
            Fees = 80,
            LastUpdate = DateTime.Now
        };
        ServiceFees existServiceFees = new ServiceFees()
        {
            ServicePurposeId = updateRequest.ApplicationPuropseId,
            ServiceCategoryId = updateRequest.ServiceCategoryId,
            Fees = 102,
            LastUpdate = updateRequest.LastUpdate - TimeSpan.FromDays(2)
        };

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ServiceFees, bool>>>()))
            .ReturnsAsync(existServiceFees);

        _mapper.Setup(temp => temp.Map<ServiceFees>(It.IsAny<ServiceFeesDTO>()))
            .Returns((ServiceFeesDTO source) => new ServiceFees
            {
                ServiceCategoryId = source.ServiceCategoryId,
                ServicePurposeId = source.ApplicationPuropseId,
                Fees = source.Fees,
                LastUpdate = source.LastUpdate
            });

        _updateRepository.Setup(temp => temp.UpdateAsync(It.IsAny<ServiceFees>()))
            .ReturnsAsync(new ServiceFees()
            {
                ServicePurposeId = updateRequest.ApplicationPuropseId,
                ServiceCategoryId = updateRequest.ServiceCategoryId,
                Fees = updateRequest.Fees,
                LastUpdate = updateRequest.LastUpdate
            });


        _mapper.Setup(temp => temp.Map<ServiceFeesDTO>(It.IsAny<ServiceFees>()))
           .Returns((ServiceFees source) => new ServiceFeesDTO
           {
               ServiceCategoryId = source.ServiceCategoryId,
               ApplicationPuropseId = source.ServicePurposeId,
               Fees = source.Fees,
               LastUpdate = source.LastUpdate
           });


        //Act
        var result = await _iUpdateServiceFees.UpdateAsync(updateRequest);

        //Assert
        result.Should().BeEquivalentTo(updateRequest);

    }
}
