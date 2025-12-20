using AutoMapper;
using FluentAssertions;
using IServices.ICountryServices;
using Microsoft.Extensions.Logging;
using ModelDTO.TestsDTOs;
using Models.Countries;
using Models.Tests;
using Moq;
using Services;
using System.Linq.Expressions;
using Services.TestServices;
using IServices.ITests.ITestTypes;
using Services.TestTypeServices;
using IRepository.IGenericRepositories;

namespace GovConnect_Tests.TestServices.TestTypesServices;

public class GetTestTypeServiceTEST
{

    private readonly ITestTypeRetrievalService _getTestType;
    private readonly Mock<IGetRepository<TestType>> _getRepositoryMock;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<ILogger<TestType>> _logger;

    public GetTestTypeServiceTEST()
    {
        _mapper = new Mock<IMapper>();
        _logger = new Mock<ILogger<TestType>>();
        _getRepositoryMock = new Mock<IGetRepository<TestType>>();
        _getTestType = new GetTestTypesService(_getRepositoryMock.Object, _logger.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetTestTypeAsync_TestTypeDoesNotExist_ReturnsNull()
    {
        // Arrange
        Expression<Func<TestType?, bool>> predicate = tt => tt.TestTypeId == 0;
        _getRepositoryMock.Setup(temp => temp.GetAsync(predicate)).ReturnsAsync(null as TestType);

        // Act
        var result = await _getTestType.GetByAsync(predicate);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetTestTypeAsync_TestTypeExists_ReturnsTestTypeObject()
    {
        // Arrange
        TestType testType = new() { TestTypeId = 1, TestTypeTitle = "koko", TestTypeDescription = "koko1", TestTypeFees = 22 };
        Expression<Func<TestType?, bool>> predicate = tt => tt.TestTypeId == testType.TestTypeId;
        _getRepositoryMock.Setup(temp => temp.GetAsync(predicate)).ReturnsAsync(testType);
        _mapper.Setup(temp => temp.Map<TestTypeDTO>(testType)).Returns((TestType tt) => new TestTypeDTO()
        {
            TestTypeId = tt.TestTypeId,
            TestTypeTitle = tt.TestTypeTitle,
            TestTypeDescription = tt.TestTypeDescription,
            TestTypeFees = tt.TestTypeFees

        });
        // Act
        var result = await _getTestType.GetByAsync(predicate);

        // Assert
        result.Should().BeEquivalentTo(_mapper.Object.Map<TestTypeDTO>(testType));
    }

    [Fact]
    public async Task GetTestTypeAsync_ThrowsException()
    {
        // Arrange
        TestType testType = new() { TestTypeId = 1, TestTypeTitle = "koko", TestTypeDescription = "koko1", TestTypeFees = 22 };
        Expression<Func<TestType?, bool>> predicate = tt => tt.TestTypeId == testType.TestTypeId;
        _getRepositoryMock.Setup(temp => temp.GetAsync(predicate)).ThrowsAsync(new Exception());

        // Act
        Func<Task> result = async () => await _getTestType.GetByAsync(predicate);

        // Assert
        await result.Should().ThrowAsync<Exception>();
    }



}
