using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.Application.For;
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

public class GetAllApplicationForServiceTest
{
    private readonly IFixture _fixture;
    private readonly IGetAllApplicationFor _getAllApplicationFor;
    private readonly IGetAllRepository<ApplicationFor> _getAllRepository;
    private readonly Mock<IGetAllRepository<ApplicationFor>> _getAllRepositoryMock;

    private readonly Mock<IMapper> _mapper;
    private readonly IMapper _mapperObj;

    public GetAllApplicationForServiceTest()
    {
        _fixture = new Fixture();
        _getAllRepositoryMock = new Mock<IGetAllRepository<ApplicationFor>>();
        _mapper = new Mock<IMapper>();
        _mapperObj = _mapper.Object;
        _getAllApplicationFor = new GetAllApplicationForService(_getAllRepositoryMock.Object, _mapperObj);
    }

    [Fact]
    public async Task GetAllAsync_EmptyDb_ReturnEmptyList()
    {
        //Arrange
        List<ApplicationFor> applicationForDTOs = new List<ApplicationFor>() { };
        _getAllRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(applicationForDTOs);

        //Act
        List<ApplicationForDTO> result = await _getAllApplicationFor.GetAllAsync();

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_DbHasData_ReturnApplicationDTOsList()
    {
        //Arrange
        List<ApplicationFor> applicationForList = new() {
          _fixture.Create<ApplicationFor>(),
            _fixture.Create<ApplicationFor>(),
            _fixture.Create<ApplicationFor>(),
            _fixture.Create<ApplicationFor>(),
        };


        List<ApplicationForDTO> applicationForDTOs = applicationForList
            .Select(app => new ApplicationForDTO() { For = app.For, Id = app.Id })
            .ToList();

        _mapper.Setup(temp => temp.Map<ApplicationForDTO>(It.IsAny<ApplicationFor>()))
            .Returns((ApplicationFor source) => new ApplicationForDTO()
            {
                For = source.For,
                Id = source.Id
            });

        _getAllRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(applicationForList);

        //Act
        List<ApplicationForDTO> result = await _getAllApplicationFor.GetAllAsync();

        //Assert
        result.Should().BeEquivalentTo(applicationForDTOs);
    }
    [Fact]
    public async Task GetAllExpressionAsync_EmptyDb_ReturnEmptyList()
    {
        //Arrange
        List<ApplicationFor> applicationForlist = new List<ApplicationFor>() { };
        IQueryable<ApplicationFor> applicationForQuerable = applicationForlist.AsQueryable();

        Expression<Func<ApplicationFor, bool>> expression = app => app.For == "duumy";

        _getAllRepositoryMock.Setup(temp =>
                                temp.GetAllAsync(expression))
                           .ReturnsAsync(applicationForQuerable);

        //Act
        IQueryable<ApplicationForDTO> result = await _getAllApplicationFor.GetAllAsync(expression);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllExpressingAsync_DbHasData_ReturnApplicationDTOsList()
    {
        //Arrange
        List<ApplicationFor> applicationForlist = new() {
          _fixture.Create<ApplicationFor>(),
            _fixture.Create<ApplicationFor>(),
            _fixture.Create<ApplicationFor>(),
            _fixture.Create<ApplicationFor>(),
        };


        IQueryable<ApplicationFor> applicationForQuerable = applicationForlist.AsQueryable();



        _mapper.Setup(temp => temp.Map<ApplicationForDTO>(It.IsAny<ApplicationFor>()))
            .Returns((ApplicationFor source) => new ApplicationForDTO()
            {
                For = source.For,
                Id = source.Id
            });

        Expression<Func<ApplicationFor, bool>> expression = app => app.For == "duumy";

        _getAllRepositoryMock.Setup(x => x.GetAllAsync(expression)).ReturnsAsync(applicationForQuerable);

        //Act
        IQueryable<ApplicationForDTO> result = await _getAllApplicationFor.GetAllAsync(expression);

        //Assert
        result.Should().BeEquivalentTo(applicationForQuerable);
    }

}
