using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IRepository;
using ModelDTO.ApplicationDTOs.Purpose;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices.Purpose;
using System.Linq.Expressions;
using IServices.IApplicationServices.IPurpose;

namespace GovConnect_Tests.ApplicationServices;

public class UpdateServicePurposeServiceTest
{
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IGetRepository<ServicePurpose>> _getRepositoryMock;

    private readonly IUpdateServicePurpose _updateServicePurpose;
    private readonly Mock<IUpdateRepository<ServicePurpose>> _updateRepositoryMock;


    public UpdateServicePurposeServiceTest()
    {
        _mapper = new Mock<IMapper>();
        _getRepositoryMock = new Mock<IGetRepository<ServicePurpose>>();

        _updateRepositoryMock = new Mock<IUpdateRepository<ServicePurpose>>();

        _updateServicePurpose =
            new UpdateServicePurposeService(_updateRepositoryMock.Object, _mapper.Object, _getRepositoryMock.Object);
    }

    [Fact]
    public async Task UpdateAsync_UpdateRequstObjIsNull_thowsArgumentNullException()
    {
        //Arrange
        ServicePurposeDTO applicationDTO = null;

        //Act
        Func<Task> result = async () => await _updateServicePurpose.UpdateAsync(applicationDTO);

        //Assert
        await result.Should().ThrowAsync<ArgumentNullException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData("     ")]
    [InlineData(null)]
    public async Task UpdateAsync_TypeIsNullOrEmpty_ThrowsArgumentException(string type)
    {
        //Arrange
        ServicePurposeDTO applicationDTO = new() { ServicePurposeId = 1, Purpose = type };

        //Act
        Func<Task> result = async () => await _updateServicePurpose.UpdateAsync(applicationDTO);

        //Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Theory]
    [InlineData(0)]
    public async Task UpdateAsync_InvalidId_ThrowsArgumentException(byte id)
    {
        //Arrange
        ServicePurposeDTO applicationDTO = new()
        {
            ServicePurposeId = id,
            Purpose = "dummy"
        };

        //Act
        Func<Task> result = async () => await _updateServicePurpose.UpdateAsync(applicationDTO);
        //Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task UpdateAsync_ServicePurposeDoesNotExist_ThrowsException()
    {
        //Arrange
        ServicePurposeDTO applicationDTO = new()
        {
            ServicePurposeId = 2,
            Purpose = "dummy"
        };

        _getRepositoryMock.Setup(temp =>
                         temp.GetAsync(It.IsAny<Expression<Func<ServicePurpose, bool>>>()))
                        .ReturnsAsync(null as ServicePurpose);



        //Act
        Func<Task> result = async () => await _updateServicePurpose.UpdateAsync(applicationDTO);
        //Assert
        await result.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task UpdateAsync_FailuretoMapping_ThrowsAutoMapperMappingException()
    {
        //Arrange
        var existingType = new ServicePurpose()
        {
            ServicePurposeId = 2,
            Purpose = "dummy"
        };

        var updatedEntity = new ServicePurpose()
        {
            ServicePurposeId = existingType.ServicePurposeId,
            Purpose = $"update {existingType.Purpose}"
        };

        var updatedDTO = new ServicePurposeDTO()
        {
            ServicePurposeId = existingType.ServicePurposeId,
            Purpose = updatedEntity.Purpose
        };

        _getRepositoryMock.Setup(temp =>
                         temp.GetAsync(It.IsAny<Expression<Func<ServicePurpose, bool>>>()))
                        .ReturnsAsync(existingType);

        _updateRepositoryMock.Setup(temp => temp.UpdateAsync(It.IsAny<ServicePurpose>()))
            .ReturnsAsync(updatedEntity);

        _mapper.Setup(temp => temp.Map<ServicePurposeDTO>(It.IsAny<ServicePurpose>()))
            .Returns(null as ServicePurposeDTO);

        //Act
        Func<Task> result = async () => await _updateServicePurpose.UpdateAsync(updatedDTO);

        //Assert
        await result.Should().ThrowAsync<AutoMapperMappingException>();
    }

    [Fact]
    public async Task UpdateAsync_DoesNotUpdate_ThrowsException()
    {
        //Arrange
        var existingType = new ServicePurpose()
        {
            ServicePurposeId = 2,
            Purpose = "dummy"
        };

        var updatedDTO = new ServicePurposeDTO()
        {
            ServicePurposeId = existingType.ServicePurposeId,
            Purpose = $"update {existingType.Purpose}"
        };

        _getRepositoryMock.Setup(temp =>
                         temp.GetAsync(It.IsAny<Expression<Func<ServicePurpose, bool>>>()))
                        .ReturnsAsync(existingType);

        _updateRepositoryMock.Setup(temp => temp.UpdateAsync(It.IsAny<ServicePurpose>()))
            .ReturnsAsync(null as ServicePurpose);



        //Act
        Func<Task> result = async () => await _updateServicePurpose.UpdateAsync(updatedDTO);

        //Assert
        await result.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task UpdateAsync_updateRequestIsValid_ReturnServicePurposeDTO()
    {
        //Arrange 
        var exsistingType = new ServicePurpose()
        {
            ServicePurposeId = 2,
            Purpose = "dummy"
        };

        var updateEntity = new ServicePurpose()
        {
            ServicePurposeId = exsistingType.ServicePurposeId,
            Purpose = $"updated {exsistingType.Purpose}"
        };

        var updatedDTO = new ServicePurposeDTO()
        {
            ServicePurposeId = exsistingType.ServicePurposeId,
            Purpose = updateEntity.Purpose
        };

        _getRepositoryMock.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ServicePurpose, bool>>>()))
            .ReturnsAsync(exsistingType);

        _updateRepositoryMock.Setup(temp => temp.UpdateAsync(It.IsAny<ServicePurpose>()))
            .ReturnsAsync(updateEntity);

        _mapper.Setup(temp =>
                     temp.Map<ServicePurposeDTO>(It.IsAny<ServicePurpose>()))
                     .Returns(updatedDTO);
        //Act
        var result = await _updateServicePurpose.UpdateAsync(updatedDTO);

        //Assert
        result.Should().BeEquivalentTo(updatedDTO);
    }
}
