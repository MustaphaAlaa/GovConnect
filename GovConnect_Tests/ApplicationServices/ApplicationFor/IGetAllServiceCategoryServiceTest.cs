using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.Category;
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

public class IGetAllServiceCategoryServiceTest
{
    private readonly IFixture _fixture;
    private readonly IGetAllServiceCategory _iGetAllServiceCategory;
    private readonly IGetAllRepository<ServiceCategory> _getAllRepository;
    private readonly Mock<IGetAllRepository<ServiceCategory>> _getAllRepositoryMock;

    private readonly Mock<IMapper> _mapper;
    private readonly IMapper _mapperObj;

    public IGetAllServiceCategoryServiceTest()
    {
        _fixture = new Fixture();
        _getAllRepositoryMock = new Mock<IGetAllRepository<ServiceCategory>>();
        _mapper = new Mock<IMapper>();
        _mapperObj = _mapper.Object;
        _iGetAllServiceCategory = new IGetAllServiceCategoryService(_getAllRepositoryMock.Object, _mapperObj);
    }

    [Fact]
    public async Task GetAllAsync_EmptyDb_ReturnEmptyList()
    {
        //Arrange
        List<ServiceCategory> applicationForDTOs = new List<ServiceCategory>() { };
        _getAllRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(applicationForDTOs);

        //Act
        List<ServiceCategoryDTO> result = await _iGetAllServiceCategory.GetAllAsync();

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_DbHasData_ReturnApplicationDTOsList()
    {
        //Arrange
        List<ServiceCategory> applicationForList = new() {
          _fixture.Create<ServiceCategory>(),
            _fixture.Create<ServiceCategory>(),
            _fixture.Create<ServiceCategory>(),
            _fixture.Create<ServiceCategory>(),
        };


        List<ServiceCategoryDTO> applicationForDTOs = applicationForList
            .Select(app => new ServiceCategoryDTO() { Category = app.Category, ServiceCategoryId = app.ServiceCategoryId })
            .ToList();

        _mapper.Setup(temp => temp.Map<ServiceCategoryDTO>(It.IsAny<ServiceCategory>()))
            .Returns((ServiceCategory source) => new ServiceCategoryDTO()
            {
                Category = source.Category,
                ServiceCategoryId = source.ServiceCategoryId
            });

        _getAllRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(applicationForList);

        //Act
        List<ServiceCategoryDTO> result = await _iGetAllServiceCategory.GetAllAsync();

        //Assert
        result.Should().BeEquivalentTo(applicationForDTOs);
    }
    [Fact]
    public async Task GetAllExpressionAsync_EmptyDb_ReturnEmptyList()
    {
        //Arrange
        List<ServiceCategory> applicationForlist = new List<ServiceCategory>() { };
        IQueryable<ServiceCategory> applicationForQuerable = applicationForlist.AsQueryable();

        Expression<Func<ServiceCategory, bool>> expression = app => app.Category == "duumy";

        _getAllRepositoryMock.Setup(temp =>
                                temp.GetAllAsync(expression))
                           .ReturnsAsync(applicationForQuerable);

        //Act
        IQueryable<ServiceCategoryDTO> result = await _iGetAllServiceCategory.GetAllAsync(expression);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllExpressingAsync_DbHasData_ReturnApplicationDTOsList()
    {
        //Arrange
        List<ServiceCategory> applicationForlist = new() {
          _fixture.Create<ServiceCategory>(),
            _fixture.Create<ServiceCategory>(),
            _fixture.Create<ServiceCategory>(),
            _fixture.Create<ServiceCategory>(),
        };


        IQueryable<ServiceCategory> applicationForQuerable = applicationForlist.AsQueryable();



        _mapper.Setup(temp => temp.Map<ServiceCategoryDTO>(It.IsAny<ServiceCategory>()))
            .Returns((ServiceCategory source) => new ServiceCategoryDTO()
            {
                Category = source.Category,
                ServiceCategoryId = source.ServiceCategoryId
            });

        Expression<Func<ServiceCategory, bool>> expression = app => app.Category == "duumy";

        _getAllRepositoryMock.Setup(x => x.GetAllAsync(expression)).ReturnsAsync(applicationForQuerable);

        //Act
        IQueryable<ServiceCategoryDTO> result = await _iGetAllServiceCategory.GetAllAsync(expression);

        //Assert
        result.Should().BeEquivalentTo(applicationForQuerable);
    }

}
