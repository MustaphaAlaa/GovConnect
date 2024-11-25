using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.For;
using IServices.IApplicationServices.Type;
using ModelDTO.ApplicationDTOs.For;
using ModelDTO.ApplicationDTOs.Type;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices.For;
using Services.ApplicationServices.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Tests.ApplicationServices;

public class GetAllApplicationTypeServiceTest
{
    private readonly IFixture _fixture;
    private readonly IGetAllApplicationTypes _getAllApplicationTypes;
    private readonly IGetAllRepository<ApplicationType> _getAllRepository;
    private readonly Mock<IGetAllRepository<ApplicationType>> _getAllRepositoryMock;

    private readonly Mock<IMapper> _mapper;
    private readonly IMapper _mapperObj;

    public GetAllApplicationTypeServiceTest()
    {
        _fixture = new Fixture();
        _getAllRepositoryMock = new Mock<IGetAllRepository<ApplicationType>>();
        _mapper = new Mock<IMapper>();
        _mapperObj = _mapper.Object;
        _getAllApplicationTypes = new GetAllApplicationTypesService(_getAllRepositoryMock.Object, _mapperObj);
    }

    [Fact]
    public async Task GetAllAsync_EmptyDb_ReturnEmptyList()
    {
        //Arrange
        List<ApplicationType> applicationTypeDTOs = new List<ApplicationType>() { };
        _getAllRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(applicationTypeDTOs);

        //Act
        List<ApplicationTypeDTO> result = await _getAllApplicationTypes.GetAllAsync();

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_DbHasData_ReturnApplicationDTOsList()
    {
        //Arrange
        List<ApplicationType> applicationTypeList = new() {
          _fixture.Create<ApplicationType>(),
            _fixture.Create<ApplicationType>(),
            _fixture.Create<ApplicationType>(),
            _fixture.Create<ApplicationType>(),
        };


        List<ApplicationTypeDTO> applicationTypeDTOs = applicationTypeList
            .Select(app => new ApplicationTypeDTO() { Type = app.Type, Id = app.Id })
            .ToList();

        _mapper.Setup(temp => temp.Map<ApplicationTypeDTO>(It.IsAny<ApplicationType>()))
            .Returns((ApplicationType source) => new ApplicationTypeDTO()
            {
                Type = source.Type,
                Id = source.Id
            });

        _getAllRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(applicationTypeList);

        //Act
        List<ApplicationTypeDTO> result = await _getAllApplicationTypes.GetAllAsync();

        //Assert
        result.Should().BeEquivalentTo(applicationTypeDTOs);
    }
    [Fact]
    public async Task GetAllExpressionAsync_EmptyDb_ReturnEmptyList()
    {
        //Arrange
        List<ApplicationType> applicationTypelist = new List<ApplicationType>() { };
        IQueryable<ApplicationType> applicationTypeQuerable = applicationTypelist.AsQueryable();

        Expression<Func<ApplicationType, bool>> expression = app => app.Type == "duumy";

        _getAllRepositoryMock.Setup(temp =>
                                temp.GetAllAsync(expression))
                           .ReturnsAsync(applicationTypeQuerable);

        //Act
        IQueryable<ApplicationTypeDTO> result = await _getAllApplicationTypes.GetAllAsync(expression);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllExpressingAsync_DbHasData_ReturnApplicationDTOsList()
    {
        //Arrange
        List<ApplicationType> applicationTypelist = new() {
          _fixture.Create<ApplicationType>(),
            _fixture.Create<ApplicationType>(),
            _fixture.Create<ApplicationType>(),
            _fixture.Create<ApplicationType>(),
        };


        IQueryable<ApplicationType> applicationTypeQuerable = applicationTypelist.AsQueryable();



        _mapper.Setup(temp => temp.Map<ApplicationTypeDTO>(It.IsAny<ApplicationType>()))
            .Returns((ApplicationType source) => new ApplicationTypeDTO()
            {
                Type = source.Type,
                Id = source.Id
            });

        Expression<Func<ApplicationType, bool>> expression = app => app.Type == "duumy";

        _getAllRepositoryMock.Setup(x => x.GetAllAsync(expression)).ReturnsAsync(applicationTypeQuerable);

        //Act
        IQueryable<ApplicationTypeDTO> result = await _getAllApplicationTypes.GetAllAsync(expression);

        //Assert
        result.Should().BeEquivalentTo(applicationTypeQuerable);
    }

}
