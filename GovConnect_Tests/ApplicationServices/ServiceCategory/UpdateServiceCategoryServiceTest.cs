using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IRepository.IGenericRepositories;
using IServices.IApplicationServices.Category;
using Microsoft.Extensions.Configuration;
using ModelDTO.ApplicationDTOs.Category;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices.For;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GovConnect_Tests.ApplicationServices;

public class UpdateServiceCategoryServiceTest
{
    private readonly Mock<IMapper> _mapper;
    private readonly IFixture _fixture;

    private readonly IGetRepository<ServiceCategory> _getRepository;
    private readonly Mock<IGetRepository<ServiceCategory>> _getRepositoryMock;

    private readonly IUpdateServiceCategory _iUpdateServiceCategory;
    private readonly IUpdateRepository<ServiceCategory> _updateRepository;
    private readonly Mock<IUpdateRepository<ServiceCategory>> _updateRepositoryMock;


    public UpdateServiceCategoryServiceTest()
    {
        _fixture = new Fixture();
        _mapper = new Mock<IMapper>();
        _getRepositoryMock = new Mock<IGetRepository<ServiceCategory>>();

        _updateRepositoryMock = new Mock<IUpdateRepository<ServiceCategory>>();

        _iUpdateServiceCategory =
            new UpdateServiceCategoryService(_updateRepositoryMock.Object, _mapper.Object, _getRepositoryMock.Object);
    }

    [Fact]
    public async Task UpdateAsync_UpdateRequstObjIsNull_thowsArgumentNullException()
    {
        //Arrange
        ServiceCategoryDTO applicationDTO = null;

        //Act
        Func<Task> result = async () => await _iUpdateServiceCategory.UpdateAsync(applicationDTO);

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
        ServiceCategoryDTO applicationDTO = new() { ServiceCategoryId = 1, Category = For };

        //Act
        Func<Task> result = async () => await _iUpdateServiceCategory.UpdateAsync(applicationDTO);

        //Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task UpdateAsync_InvalidId_ThrowsArgumentException(short id)
    {
        //Arrange
        ServiceCategoryDTO applicationDTO = new()
        {
            ServiceCategoryId = id,
            Category = "dummy"
        };

        //Act
        Func<Task> result = async () => await _iUpdateServiceCategory.UpdateAsync(applicationDTO);

        //Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task UpdateAsync_ForDoesNotExist_ThrowsException()
    {
        //Arrange
        ServiceCategoryDTO applicationDTO = new()
        {
            ServiceCategoryId = 2,
            Category = "dummy"
        };

        _getRepositoryMock.Setup(temp =>
                         temp.GetAsync(It.IsAny<Expression<Func<ServiceCategory, bool>>>()))
                        .ReturnsAsync(null as ServiceCategory);

        //Act
        Func<Task> result = async () => await _iUpdateServiceCategory.UpdateAsync(applicationDTO);

        //Assert
        await result.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task UpdateAsync_FailureToMapping_ThrowsAutoMapperMappingException()
    {
        //Arrange
        var existingFor = new ServiceCategory()
        {
            ServiceCategoryId = 2,
            Category = "dummy"
        };

        var updatedEntity = new ServiceCategory()
        {
            ServiceCategoryId = existingFor.ServiceCategoryId,
            Category = $"update {existingFor.Category}"
        };

        var updatedDTO = new ServiceCategoryDTO()
        {
            ServiceCategoryId = existingFor.ServiceCategoryId,
            Category = updatedEntity.Category
        };

        _getRepositoryMock.Setup(temp =>
                         temp.GetAsync(It.IsAny<Expression<Func<ServiceCategory, bool>>>()))
                        .ReturnsAsync(existingFor);

        _updateRepositoryMock.Setup(temp => temp.UpdateAsync(It.IsAny<ServiceCategory>()))
            .ReturnsAsync(updatedEntity);

        _mapper.Setup(temp => temp.Map<ServiceCategoryDTO>(It.IsAny<ServiceCategory>()))
            .Returns(null as ServiceCategoryDTO);

        //Act
        Func<Task> result = async () => await _iUpdateServiceCategory.UpdateAsync(updatedDTO);

        //Assert
        await result.Should().ThrowAsync<AutoMapperMappingException>();
    }

    [Fact]
    public async Task UpdateAsync_DoesNotUpdate_ThrowsException()
    {
        //Arrange
        var existingFor = new ServiceCategory()
        {
            ServiceCategoryId = 2,
            Category = "dummy"
        };

        var updatedDTO = new ServiceCategoryDTO()
        {
            ServiceCategoryId = existingFor.ServiceCategoryId,
            Category = $"update {existingFor.Category}"
        };

        _getRepositoryMock.Setup(temp =>
                         temp.GetAsync(It.IsAny<Expression<Func<ServiceCategory, bool>>>()))
                        .ReturnsAsync(existingFor);

        _updateRepositoryMock.Setup(temp => temp.UpdateAsync(It.IsAny<ServiceCategory>()))
            .ReturnsAsync(null as ServiceCategory);

        //Act
        Func<Task> result = async () => await _iUpdateServiceCategory.UpdateAsync(updatedDTO);

        //Assert
        await result.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task UpdateAsync_updateRequestIsValid_ReturnApplicationForDTO()
    {
        //Arrange 
        var exsistingFor = new ServiceCategory()
        {
            ServiceCategoryId = 2,
            Category = "dummy"
        };

        var updateEntity = new ServiceCategory()
        {
            ServiceCategoryId = exsistingFor.ServiceCategoryId,
            Category = $"updated {exsistingFor.Category}"
        };

        var updatedDTO = new ServiceCategoryDTO()
        {
            ServiceCategoryId = exsistingFor.ServiceCategoryId,
            Category = updateEntity.Category
        };

        _getRepositoryMock.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ServiceCategory, bool>>>()))
            .ReturnsAsync(exsistingFor);

        _updateRepositoryMock.Setup(temp => temp.UpdateAsync(It.IsAny<ServiceCategory>()))
            .ReturnsAsync(updateEntity);

        _mapper.Setup(temp =>
                     temp.Map<ServiceCategoryDTO>(It.IsAny<ServiceCategory>()))
                     .Returns(updatedDTO);
        //Act
        var result = await _iUpdateServiceCategory.UpdateAsync(updatedDTO);

        //Assert
        result.Should().BeEquivalentTo(updatedDTO);
    }





}
