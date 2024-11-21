using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.Application.Type;
using Microsoft.Extensions.Configuration;
using ModelDTO.Application.Type;
using Models.Applications;
using Moq;
using Services.Application.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Tests.Applications;

public class UpdateApplicationTypeServiceTest
{
    private readonly Mock<IMapper> _mapper;
    private readonly IFixture _fixture;

    private readonly IGetRepository<ApplicationType> _getRepository;
    private readonly Mock<IGetRepository<ApplicationType>> _getRepositoryMock;

    private readonly IUpdateApplicationType _updateApplicationType;
    private readonly IUpdateRepository<ApplicationType> _updateRepository;
    private readonly Mock<IUpdateRepository<ApplicationType>> _updateRepositoryMock;


    public UpdateApplicationTypeServiceTest()
    {
        _fixture = new Fixture();
        _mapper = new Mock<IMapper>();
        _getRepositoryMock = new Mock<IGetRepository<ApplicationType>>();

        _updateRepositoryMock = new Mock<IUpdateRepository<ApplicationType>>();

        _updateApplicationType =
            new UpdateApplicationTypeService(_updateRepositoryMock.Object, _mapper.Object, _getRepositoryMock.Object);
    }

    [Fact]
    public async Task UpdateAsync_UpdateRequstObjIsNull_thowsArgumentNullException()
    {
        //Arrange
        ApplicationTypeDTO applicationDTO = null;

        //Act
        Func<Task> result = async () => await _updateApplicationType.UpdateAsync(applicationDTO);

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
        ApplicationTypeDTO applicationDTO = new() { Id = 1, Type = type };

        //Act
        Func<Task> result = async () => await _updateApplicationType.UpdateAsync(applicationDTO);

        //Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task UpdateAsync_InvalidId_ThrowsArgumentException(int id)
    {
        //Arrange
        ApplicationTypeDTO applicationDTO = new()
        {
            Id = id,
            Type = "dummy"
        };

        //Act
        Func<Task> result = async () => await _updateApplicationType.UpdateAsync(applicationDTO);
        //Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task UpdateAsync_TypeDoesNotExist_ThrowsException()
    {
        //Arrange
        ApplicationTypeDTO applicationDTO = new()
        {
            Id = 2,
            Type = "dummy"
        };

        _getRepositoryMock.Setup(temp =>
                         temp.GetAsync(It.IsAny<Expression<Func<ApplicationType, bool>>>()))
                        .ReturnsAsync(null as ApplicationType);



        //Act
        Func<Task> result = async () => await _updateApplicationType.UpdateAsync(applicationDTO);
        //Assert
        await result.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task UpdateAsync_FailuretoMapping_ThrowsAutoMapperMappingException()
    {
        //Arrange
        var existingType = new ApplicationType()
        {
            Id = 2,
            Type = "dummy"
        };

        var updatedEntity = new ApplicationType()
        {
            Id = existingType.Id,
            Type = $"update {existingType.Type}"
        };

        var updatedDTO = new ApplicationTypeDTO()
        {
            Id = existingType.Id,
            Type = updatedEntity.Type
        };

        _getRepositoryMock.Setup(temp =>
                         temp.GetAsync(It.IsAny<Expression<Func<ApplicationType, bool>>>()))
                        .ReturnsAsync(existingType);

        _updateRepositoryMock.Setup(temp => temp.UpdateAsync(It.IsAny<ApplicationType>()))
            .ReturnsAsync(updatedEntity);

        _mapper.Setup(temp => temp.Map<ApplicationTypeDTO>(It.IsAny<ApplicationType>()))
            .Returns(null as ApplicationTypeDTO);

        //Act
        Func<Task> result = async () => await _updateApplicationType.UpdateAsync(updatedDTO);

        //Assert
        await result.Should().ThrowAsync<AutoMapperMappingException>();
    }

    [Fact]
    public async Task UpdateAsync_DoesNotUpdate_ThrowsException()
    {
        //Arrange
        var existingType = new ApplicationType()
        {
            Id = 2,
            Type = "dummy"
        };

        var updatedDTO = new ApplicationTypeDTO()
        {
            Id = existingType.Id,
            Type = $"update {existingType.Type}"
        };

        _getRepositoryMock.Setup(temp =>
                         temp.GetAsync(It.IsAny<Expression<Func<ApplicationType, bool>>>()))
                        .ReturnsAsync(existingType);

        _updateRepositoryMock.Setup(temp => temp.UpdateAsync(It.IsAny<ApplicationType>()))
            .ReturnsAsync(null as ApplicationType);



        //Act
        Func<Task> result = async () => await _updateApplicationType.UpdateAsync(updatedDTO);

        //Assert
        await result.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task UpdateAsync_updateRequestIsValid_ReturnApplicationTypeDTO()
    {
        //Arrange 
        var exsistingType = new ApplicationType()
        {
            Id = 2,
            Type = "dummy"
        };

        var updateEntity = new ApplicationType()
        {
            Id = exsistingType.Id,
            Type = $"updated {exsistingType.Type}"
        };

        var updatedDTO = new ApplicationTypeDTO()
        {
            Id = exsistingType.Id,
            Type = updateEntity.Type
        };

        _getRepositoryMock.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationType, bool>>>()))
            .ReturnsAsync(exsistingType);

        _updateRepositoryMock.Setup(temp => temp.UpdateAsync(It.IsAny<ApplicationType>()))
            .ReturnsAsync(updateEntity);

        _mapper.Setup(temp =>
                     temp.Map<ApplicationTypeDTO>(It.IsAny<ApplicationType>()))
                     .Returns(updatedDTO);
        //Act
        var result = await _updateApplicationType.UpdateAsync(updatedDTO);

        //Assert
        result.Should().BeEquivalentTo(updatedDTO);
    }





}
