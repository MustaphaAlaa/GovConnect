using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.Application.Fees;
using ModelDTO.Application.Fees;
using Models.ApplicationModels;
using Moq;
using Services.Application.Fees;
using System.Linq.Expressions;

namespace DVLD_Tests.Applications;

public class UpdateeApplicationFeesServiceTest
{
    private readonly IFixture _fixture;
    private readonly Mock<IMapper> _mapper;

    private readonly Mock<IGetRepository<ApplicationFees>> _getRepository;
    private readonly Mock<IUpdateRepository<ApplicationFees>> _updateRepository;

    private readonly IUpdateApplicationFees _updateApplicationFees;

    public UpdateeApplicationFeesServiceTest()
    {
        _fixture = new Fixture();
        _mapper = new Mock<IMapper>();

        _getRepository = new Mock<IGetRepository<ApplicationFees>>();
        _updateRepository = new Mock<IUpdateRepository<ApplicationFees>>();

        _updateApplicationFees = new UpdateApplicationFeesService(_mapper.Object, _updateRepository.Object, _getRepository.Object);
    }

    [Fact]
    public async Task UpdateApplictaionFees_ReqObjIsNull_ThrowsArgumentException()
    {
        //Arrange && Act
        Func<Task> action = async () => await _updateApplicationFees.UpdateAsync(null);

        //Assest
        await action.Should().ThrowAsync<ArgumentNullException>();
    }


    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task UpdateApplicationFees_TypeIdIsInvalid_ThrowsArgumentOutOfRangeException(int id)
    {
        //Arrange
        ApplicationFeesDTO updateRequest = _fixture.Build<ApplicationFeesDTO>()
            .With(app => app.ApplicationTypeId, id)
            .Create();

        //Act
        Func<Task> action = async () => await _updateApplicationFees.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<ArgumentOutOfRangeException>();

    }


    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task UpdateApplicationFees_ForIdIsInvalid_ThrowsArgumentOutOfRangeException(int id)
    {
        //Arrange
        ApplicationFeesDTO updateRequest = _fixture.Build<ApplicationFeesDTO>()
               .With(app => app.ApplicationForId, id)
               .Create();

        //Act
        Func<Task> action = async () => await _updateApplicationFees.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<ArgumentOutOfRangeException>();
    }

    [Fact]
    public async Task UpdateApplicationFees_FeesValueIsInvalid_ThrowsArgumentOutOfRangeException()
    {
        //Arrange
        ApplicationFeesDTO createReq = _fixture.Build<ApplicationFeesDTO>()
               .With(app => app.ApplicationForId, -4)
               .Create();

        //Act
        Func<Task> action = async () => await _updateApplicationFees.UpdateAsync(createReq);

        //Assert
        await action.Should().ThrowAsync<ArgumentOutOfRangeException>();
    }

    [Fact]
    public async Task UpdateApplicationFees_ApplicationFeesDoesNotExists_ThrowsException()
    {
        //Arrange
        ApplicationFeesDTO updateRequest = _fixture.Create<ApplicationFeesDTO>();


        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationFees, bool>>>()))
            .ReturnsAsync(null as ApplicationFees);

        //Act
        Func<Task> action = async () => await _updateApplicationFees.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<Exception>();
    }


    [Fact]
    public async Task UpdateApplicationFees_InvalidDate_ThrowsException()
    {
        //Arrange
        ApplicationFees existApplicationFees = new ApplicationFees()
        {
            ApplicationTypeId = 1,
            ApplicationForId = 200,
            Fees = 10000,
            LastUpdate = new DateTime(2005, 11, 30)
        };

        ApplicationFeesDTO updateRequest = _fixture.Build<ApplicationFeesDTO>()
            .With(appFees => appFees.ApplicationTypeId, existApplicationFees.ApplicationTypeId)
            .With(appFees => appFees.ApplicationForId, existApplicationFees.ApplicationForId)
            .With(appFees => appFees.LastUdpate, existApplicationFees.LastUpdate - TimeSpan.FromDays(10))
            .Create();


        //Act
        Func<Task> action = async () => await _updateApplicationFees.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<Exception>();
    }



    [Fact]
    public async Task UpdateApplicationFees_FailureToMappingFromDtoToModel_AutoMapperMappingException()
    {

        //Arrange
        ApplicationFees existApplicationFees = new ApplicationFees()
        {
            ApplicationTypeId = 1,
            ApplicationForId = 2,
            Fees = 12,
            LastUpdate = new DateTime(2021, 5, 16)
        };

        ApplicationFeesDTO updateRequest = new()
        {
            ApplicationTypeId = existApplicationFees.ApplicationTypeId,
            ApplicationForId = existApplicationFees.ApplicationForId,
            Fees = 120,
            LastUdpate = DateTime.Now,
        };

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationFees, bool>>>()))
            .ReturnsAsync(existApplicationFees);

        _mapper.Setup(temp => temp.Map<ApplicationFees>(It.IsAny<ApplicationFeesDTO>()))
            .Returns(null as ApplicationFees);

        //Act
        Func<Task> action = async () => await _updateApplicationFees.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<AutoMapperMappingException>();
    }

    [Fact]
    public async Task UpdateApplicationFees_FailureToUpdateApplicationFees_ThrowsException()
    {
        //Arrange
        ApplicationFeesDTO updateRequest = _fixture.Create<ApplicationFeesDTO>();
        ApplicationFees existApplicationFees = new ApplicationFees()
        {
            ApplicationTypeId = 1,
            ApplicationForId = 5,
            Fees = 102,
            LastUpdate = updateRequest.LastUdpate - TimeSpan.FromDays(2)
        };

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationFees, bool>>>()))
            .ReturnsAsync(existApplicationFees);

        _mapper.Setup(temp => temp.Map<ApplicationFees>(It.IsAny<ApplicationFeesDTO>()))
            .Returns(existApplicationFees);


        _updateRepository.Setup(temp => temp.UpdateAsync(It.IsAny<ApplicationFees>()))
            .ReturnsAsync(null as ApplicationFees);

        //Act
        Func<Task> action = async () => await _updateApplicationFees.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<Exception>();
    }


    [Fact]
    public async Task UpdateApplicationFees_FailureToMappingFromModelToDTO_AutoMapperMappingException()
    {
        //Arrange
        ApplicationFeesDTO updateRequest = new()
        {
            ApplicationTypeId = 1,
            ApplicationForId = 5,
            Fees = 80,
            LastUdpate = DateTime.Now
        };
        ApplicationFees existApplicationFees = new ApplicationFees()
        {
            ApplicationTypeId = 1,
            ApplicationForId = 5,
            Fees = 102,
            LastUpdate = updateRequest.LastUdpate - TimeSpan.FromDays(2)
        };


        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationFees, bool>>>()))
            .ReturnsAsync(existApplicationFees);

        _mapper.Setup(temp => temp.Map<ApplicationFees>(It.IsAny<ApplicationFeesDTO>()))
            .Returns((ApplicationFeesDTO source) => new ApplicationFees
            {
                ApplicationForId = source.ApplicationForId,
                ApplicationTypeId = source.ApplicationTypeId,
                Fees = source.Fees,
                LastUpdate = source.LastUdpate
            });

        _updateRepository.Setup(temp => temp.UpdateAsync(It.IsAny<ApplicationFees>()))
            .ReturnsAsync(new ApplicationFees()
            {
                ApplicationTypeId = updateRequest.ApplicationTypeId,
                ApplicationForId = updateRequest.ApplicationForId,
                Fees = updateRequest.Fees,
                LastUpdate = updateRequest.LastUdpate
            });

        _mapper.Setup(temp => temp.Map<ApplicationFeesDTO>(It.IsAny<ApplicationFees>()))
             .Returns<ApplicationFeesDTO>(null);

        //Act
        Func<Task> action = async () => await _updateApplicationFees.UpdateAsync(updateRequest);

        //Assert
        await action.Should().ThrowAsync<AutoMapperMappingException>();
    }


    [Fact]
    public async Task UpdateApplicationFees_updateApplicationFees_ReturnApplicationDTO()
    {
        //Arrange
        //Arrange
        ApplicationFeesDTO updateRequest = new()
        {
            ApplicationTypeId = 1,
            ApplicationForId = 5,
            Fees = 80,
            LastUdpate = DateTime.Now
        };
        ApplicationFees existApplicationFees = new ApplicationFees()
        {
            ApplicationTypeId = updateRequest.ApplicationTypeId,
            ApplicationForId = updateRequest.ApplicationForId,
            Fees = 102,
            LastUpdate = updateRequest.LastUdpate - TimeSpan.FromDays(2)
        };

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationFees, bool>>>()))
            .ReturnsAsync(existApplicationFees);

        _mapper.Setup(temp => temp.Map<ApplicationFees>(It.IsAny<ApplicationFeesDTO>()))
            .Returns((ApplicationFeesDTO source) => new ApplicationFees
            {
                ApplicationForId = source.ApplicationForId,
                ApplicationTypeId = source.ApplicationTypeId,
                Fees = source.Fees,
                LastUpdate = source.LastUdpate
            });

        _updateRepository.Setup(temp => temp.UpdateAsync(It.IsAny<ApplicationFees>()))
            .ReturnsAsync(new ApplicationFees()
            {
                ApplicationTypeId = updateRequest.ApplicationTypeId,
                ApplicationForId = updateRequest.ApplicationForId,
                Fees = updateRequest.Fees,
                LastUpdate = updateRequest.LastUdpate
            });


        _mapper.Setup(temp => temp.Map<ApplicationFeesDTO>(It.IsAny<ApplicationFees>()))
           .Returns((ApplicationFees source) => new ApplicationFeesDTO
           {
               ApplicationForId = source.ApplicationForId,
               ApplicationTypeId = source.ApplicationTypeId,
               Fees = source.Fees,
               LastUdpate = source.LastUpdate
           });


        //Act
        var result = await _updateApplicationFees.UpdateAsync(updateRequest);

        //Assert
        result.Should().BeEquivalentTo(updateRequest);

    }
}
