using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.Category;
using Microsoft.Extensions.Configuration;
using ModelDTO.ApplicationDTOs.Purpose;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices.Purpose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IServices.IApplicationServices.Purpose;

namespace GovConnect_Tests.ApplicationServices;

public class UpdateApplicationPurposeServiceTest
{
    private readonly Mock<IMapper> _mapper;
    private readonly IFixture _fixture;

    private readonly IGetRepository<ApplicationPurpose> _getRepository;
    private readonly Mock<IGetRepository<ApplicationPurpose>> _getRepositoryMock;

    private readonly IUpdateApplicationPurpose _updateApplicationPurpose;
    private readonly IUpdateRepository<ApplicationPurpose> _updateRepository;
    private readonly Mock<IUpdateRepository<ApplicationPurpose>> _updateRepositoryMock;


    public UpdateApplicationPurposeServiceTest()
    {
        _fixture = new Fixture();
        _mapper = new Mock<IMapper>();
        _getRepositoryMock = new Mock<IGetRepository<ApplicationPurpose>>();

        _updateRepositoryMock = new Mock<IUpdateRepository<ApplicationPurpose>>();

        _updateApplicationPurpose =
            new UpdateApplicationPurposeService(_updateRepositoryMock.Object, _mapper.Object, _getRepositoryMock.Object);
    }

    [Fact]
    public async Task UpdateAsync_UpdateRequstObjIsNull_thowsArgumentNullException()
    {
        //Arrange
        ApplicationPurposeDTO applicationDTO = null;

        //Act
        Func<Task> result = async () => await _updateApplicationPurpose.UpdateAsync(applicationDTO);

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
        ApplicationPurposeDTO applicationDTO = new() { ApplicationPurposeId = 1, Purpose = type };

        //Act
        Func<Task> result = async () => await _updateApplicationPurpose.UpdateAsync(applicationDTO);

        //Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Theory]
    [InlineData(0)]
    public async Task UpdateAsync_InvalidId_ThrowsArgumentException(byte id)
    {
        //Arrange
        ApplicationPurposeDTO applicationDTO = new()
        {
            ApplicationPurposeId = id,
            Purpose = "dummy"
        };

        //Act
        Func<Task> result = async () => await _updateApplicationPurpose.UpdateAsync(applicationDTO);
        //Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task UpdateAsync_TypeDoesNotExist_ThrowsException()
    {
        //Arrange
        ApplicationPurposeDTO applicationDTO = new()
        {
            ApplicationPurposeId = 2,
            Purpose = "dummy"
        };

        _getRepositoryMock.Setup(temp =>
                         temp.GetAsync(It.IsAny<Expression<Func<ApplicationPurpose, bool>>>()))
                        .ReturnsAsync(null as ApplicationPurpose);



        //Act
        Func<Task> result = async () => await _updateApplicationPurpose.UpdateAsync(applicationDTO);
        //Assert
        await result.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task UpdateAsync_FailuretoMapping_ThrowsAutoMapperMappingException()
    {
        //Arrange
        var existingType = new ApplicationPurpose()
        {
            ApplicationPurposeId = 2,
            Purpose = "dummy"
        };

        var updatedEntity = new ApplicationPurpose()
        {
            ApplicationPurposeId = existingType.ApplicationPurposeId,
            Purpose = $"update {existingType.Purpose}"
        };

        var updatedDTO = new ApplicationPurposeDTO()
        {
            ApplicationPurposeId = existingType.ApplicationPurposeId,
            Purpose = updatedEntity.Purpose
        };

        _getRepositoryMock.Setup(temp =>
                         temp.GetAsync(It.IsAny<Expression<Func<ApplicationPurpose, bool>>>()))
                        .ReturnsAsync(existingType);

        _updateRepositoryMock.Setup(temp => temp.UpdateAsync(It.IsAny<ApplicationPurpose>()))
            .ReturnsAsync(updatedEntity);

        _mapper.Setup(temp => temp.Map<ApplicationPurposeDTO>(It.IsAny<ApplicationPurpose>()))
            .Returns(null as ApplicationPurposeDTO);

        //Act
        Func<Task> result = async () => await _updateApplicationPurpose.UpdateAsync(updatedDTO);

        //Assert
        await result.Should().ThrowAsync<AutoMapperMappingException>();
    }

    [Fact]
    public async Task UpdateAsync_DoesNotUpdate_ThrowsException()
    {
        //Arrange
        var existingType = new ApplicationPurpose()
        {
            ApplicationPurposeId = 2,
            Purpose = "dummy"
        };

        var updatedDTO = new ApplicationPurposeDTO()
        {
            ApplicationPurposeId = existingType.ApplicationPurposeId,
            Purpose = $"update {existingType.Purpose}"
        };

        _getRepositoryMock.Setup(temp =>
                         temp.GetAsync(It.IsAny<Expression<Func<ApplicationPurpose, bool>>>()))
                        .ReturnsAsync(existingType);

        _updateRepositoryMock.Setup(temp => temp.UpdateAsync(It.IsAny<ApplicationPurpose>()))
            .ReturnsAsync(null as ApplicationPurpose);



        //Act
        Func<Task> result = async () => await _updateApplicationPurpose.UpdateAsync(updatedDTO);

        //Assert
        await result.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task UpdateAsync_updateRequestIsValid_ReturnApplicationTypeDTO()
    {
        //Arrange 
        var exsistingType = new ApplicationPurpose()
        {
            ApplicationPurposeId = 2,
            Purpose = "dummy"
        };

        var updateEntity = new ApplicationPurpose()
        {
            ApplicationPurposeId = exsistingType.ApplicationPurposeId,
            Purpose = $"updated {exsistingType.Purpose}"
        };

        var updatedDTO = new ApplicationPurposeDTO()
        {
            ApplicationPurposeId = exsistingType.ApplicationPurposeId,
            Purpose = updateEntity.Purpose
        };

        _getRepositoryMock.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationPurpose, bool>>>()))
            .ReturnsAsync(exsistingType);

        _updateRepositoryMock.Setup(temp => temp.UpdateAsync(It.IsAny<ApplicationPurpose>()))
            .ReturnsAsync(updateEntity);

        _mapper.Setup(temp =>
                     temp.Map<ApplicationPurposeDTO>(It.IsAny<ApplicationPurpose>()))
                     .Returns(updatedDTO);
        //Act
        var result = await _updateApplicationPurpose.UpdateAsync(updatedDTO);

        //Assert
        result.Should().BeEquivalentTo(updatedDTO);
    }





}
