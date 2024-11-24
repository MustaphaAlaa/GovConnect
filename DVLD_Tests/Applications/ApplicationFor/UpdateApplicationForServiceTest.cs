using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.Application.For;
using Microsoft.Extensions.Configuration;
using ModelDTO.Application.For;
using Models.ApplicationModels;
using Moq;
using Services.Application.For;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Tests.Applications;

public class UpdateApplicationForServiceTest
{
    private readonly Mock<IMapper> _mapper;
    private readonly IFixture _fixture;

    private readonly IGetRepository<ApplicationFor> _getRepository;
    private readonly Mock<IGetRepository<ApplicationFor>> _getRepositoryMock;

    private readonly IUpdateApplicationFor _updateApplicationFor;
    private readonly IUpdateRepository<ApplicationFor> _updateRepository;
    private readonly Mock<IUpdateRepository<ApplicationFor>> _updateRepositoryMock;


    public UpdateApplicationForServiceTest()
    {
        _fixture = new Fixture();
        _mapper = new Mock<IMapper>();
        _getRepositoryMock = new Mock<IGetRepository<ApplicationFor>>();

        _updateRepositoryMock = new Mock<IUpdateRepository<ApplicationFor>>();

        _updateApplicationFor =
            new UpdateApplicationForService(_updateRepositoryMock.Object, _mapper.Object, _getRepositoryMock.Object);
    }

    [Fact]
    public async Task UpdateAsync_UpdateRequstObjIsNull_thowsArgumentNullException()
    {
        //Arrange
        ApplicationForDTO applicationDTO = null;

        //Act
        Func<Task> result = async () => await _updateApplicationFor.UpdateAsync(applicationDTO);

        //Assert
        await result.Should().ThrowAsync<ArgumentNullException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData("     ")]
    [InlineData(null)]
    public async Task UpdateAsync_ForIsNullOrEmpty_ThrowsArgumentException(string For)
    {
        //Arrange
        ApplicationForDTO applicationDTO = new() { Id = 1, For = For };

        //Act
        Func<Task> result = async () => await _updateApplicationFor.UpdateAsync(applicationDTO);

        //Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task UpdateAsync_InvalidId_ThrowsArgumentException(short id)
    {
        //Arrange
        ApplicationForDTO applicationDTO = new()
        {
            Id = id,
            For = "dummy"
        };

        //Act
        Func<Task> result = async () => await _updateApplicationFor.UpdateAsync(applicationDTO);

        //Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task UpdateAsync_ForDoesNotExist_ThrowsException()
    {
        //Arrange
        ApplicationForDTO applicationDTO = new()
        {
            Id = 2,
            For = "dummy"
        };

        _getRepositoryMock.Setup(temp =>
                         temp.GetAsync(It.IsAny<Expression<Func<ApplicationFor, bool>>>()))
                        .ReturnsAsync(null as ApplicationFor);

        //Act
        Func<Task> result = async () => await _updateApplicationFor.UpdateAsync(applicationDTO);

        //Assert
        await result.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task UpdateAsync_FailureToMapping_ThrowsAutoMapperMappingException()
    {
        //Arrange
        var existingFor = new ApplicationFor()
        {
            Id = 2,
            For = "dummy"
        };

        var updatedEntity = new ApplicationFor()
        {
            Id = existingFor.Id,
            For = $"update {existingFor.For}"
        };

        var updatedDTO = new ApplicationForDTO()
        {
            Id = existingFor.Id,
            For = updatedEntity.For
        };

        _getRepositoryMock.Setup(temp =>
                         temp.GetAsync(It.IsAny<Expression<Func<ApplicationFor, bool>>>()))
                        .ReturnsAsync(existingFor);

        _updateRepositoryMock.Setup(temp => temp.UpdateAsync(It.IsAny<ApplicationFor>()))
            .ReturnsAsync(updatedEntity);

        _mapper.Setup(temp => temp.Map<ApplicationForDTO>(It.IsAny<ApplicationFor>()))
            .Returns(null as ApplicationForDTO);

        //Act
        Func<Task> result = async () => await _updateApplicationFor.UpdateAsync(updatedDTO);

        //Assert
        await result.Should().ThrowAsync<AutoMapperMappingException>();
    }

    [Fact]
    public async Task UpdateAsync_DoesNotUpdate_ThrowsException()
    {
        //Arrange
        var existingFor = new ApplicationFor()
        {
            Id = 2,
            For = "dummy"
        };

        var updatedDTO = new ApplicationForDTO()
        {
            Id = existingFor.Id,
            For = $"update {existingFor.For}"
        };

        _getRepositoryMock.Setup(temp =>
                         temp.GetAsync(It.IsAny<Expression<Func<ApplicationFor, bool>>>()))
                        .ReturnsAsync(existingFor);

        _updateRepositoryMock.Setup(temp => temp.UpdateAsync(It.IsAny<ApplicationFor>()))
            .ReturnsAsync(null as ApplicationFor);

        //Act
        Func<Task> result = async () => await _updateApplicationFor.UpdateAsync(updatedDTO);

        //Assert
        await result.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task UpdateAsync_updateRequestIsValid_ReturnApplicationForDTO()
    {
        //Arrange 
        var exsistingFor = new ApplicationFor()
        {
            Id = 2,
            For = "dummy"
        };

        var updateEntity = new ApplicationFor()
        {
            Id = exsistingFor.Id,
            For = $"updated {exsistingFor.For}"
        };

        var updatedDTO = new ApplicationForDTO()
        {
            Id = exsistingFor.Id,
            For = updateEntity.For
        };

        _getRepositoryMock.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationFor, bool>>>()))
            .ReturnsAsync(exsistingFor);

        _updateRepositoryMock.Setup(temp => temp.UpdateAsync(It.IsAny<ApplicationFor>()))
            .ReturnsAsync(updateEntity);

        _mapper.Setup(temp =>
                     temp.Map<ApplicationForDTO>(It.IsAny<ApplicationFor>()))
                     .Returns(updatedDTO);
        //Act
        var result = await _updateApplicationFor.UpdateAsync(updatedDTO);

        //Assert
        result.Should().BeEquivalentTo(updatedDTO);
    }





}
