using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.Category;
using ModelDTO.ApplicationDTOs.Category;
using ModelDTO.ApplicationDTOs.Purpose;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices.For;
using Services.ApplicationServices.Purpose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IServices.IApplicationServices.Purpose;

namespace GovConnect_Tests.ApplicationServices;

public class GetAllApplicationPurposeServiceTest
{
    private readonly IFixture _fixture;
    private readonly IGetAllApplicationPurpose _getAllApplicationPurposes;
    private readonly IGetAllRepository<ApplicationPurpose> _getAllRepository;
    private readonly Mock<IGetAllRepository<ApplicationPurpose>> _getAllRepositoryMock;

    private readonly Mock<IMapper> _mapper;
    private readonly IMapper _mapperObj;

    public GetAllApplicationPurposeServiceTest()
    {
        _fixture = new Fixture();
        _getAllRepositoryMock = new Mock<IGetAllRepository<ApplicationPurpose>>();
        _mapper = new Mock<IMapper>();
        _mapperObj = _mapper.Object;
        _getAllApplicationPurposes = new GetAllApplicationPurposesService(_getAllRepositoryMock.Object, _mapperObj);
    }

    [Fact]
    public async Task GetAllAsync_EmptyDb_ReturnEmptyList()
    {
        //Arrange
        List<ApplicationPurpose> applicationTypeDTOs = new List<ApplicationPurpose>() { };
        _getAllRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(applicationTypeDTOs);

        //Act
        List<ApplicationPurposeDTO> result = await _getAllApplicationPurposes.GetAllAsync();

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_DbHasData_ReturnApplicationDTOsList()
    {
        //Arrange
        List<ApplicationPurpose> applicationTypeList = new() {
          _fixture.Create<ApplicationPurpose>(),
            _fixture.Create<ApplicationPurpose>(),
            _fixture.Create<ApplicationPurpose>(),
            _fixture.Create<ApplicationPurpose>(),
        };


        List<ApplicationPurposeDTO> applicationTypeDTOs = applicationTypeList
            .Select(app => new ApplicationPurposeDTO() { Purpose = app.Purpose, ApplicationPurposeId = app.ApplicationPurposeId })
            .ToList();

        _mapper.Setup(temp => temp.Map<ApplicationPurposeDTO>(It.IsAny<ApplicationPurpose>()))
            .Returns((ApplicationPurpose source) => new ApplicationPurposeDTO()
            {
                Purpose = source.Purpose,
                ApplicationPurposeId = source.ApplicationPurposeId
            });

        _getAllRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(applicationTypeList);

        //Act
        List<ApplicationPurposeDTO> result = await _getAllApplicationPurposes.GetAllAsync();

        //Assert
        result.Should().BeEquivalentTo(applicationTypeDTOs);
    }
    [Fact]
    public async Task GetAllExpressionAsync_EmptyDb_ReturnEmptyList()
    {
        //Arrange
        List<ApplicationPurpose> applicationTypelist = new List<ApplicationPurpose>() { };
        IQueryable<ApplicationPurpose> applicationTypeQuerable = applicationTypelist.AsQueryable();

        Expression<Func<ApplicationPurpose, bool>> expression = app => app.Purpose == "duumy";

        _getAllRepositoryMock.Setup(temp =>
                                temp.GetAllAsync(expression))
                           .ReturnsAsync(applicationTypeQuerable);

        //Act
        IQueryable<ApplicationPurposeDTO> result = await _getAllApplicationPurposes.GetAllAsync(expression);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllExpressingAsync_DbHasData_ReturnApplicationDTOsList()
    {
        //Arrange
        List<ApplicationPurpose> applicationTypelist = new() {
          _fixture.Create<ApplicationPurpose>(),
            _fixture.Create<ApplicationPurpose>(),
            _fixture.Create<ApplicationPurpose>(),
            _fixture.Create<ApplicationPurpose>(),
        };


        IQueryable<ApplicationPurpose> applicationTypeQuerable = applicationTypelist.AsQueryable();



        _mapper.Setup(temp => temp.Map<ApplicationPurposeDTO>(It.IsAny<ApplicationPurpose>()))
            .Returns((ApplicationPurpose source) => new ApplicationPurposeDTO()
            {
                Purpose = source.Purpose,
                ApplicationPurposeId = source.ApplicationPurposeId
            });

        Expression<Func<ApplicationPurpose, bool>> expression = app => app.Purpose == "duumy";

        _getAllRepositoryMock.Setup(x => x.GetAllAsync(expression)).ReturnsAsync(applicationTypeQuerable);

        //Act
        IQueryable<ApplicationPurposeDTO> result = await _getAllApplicationPurposes.GetAllAsync(expression);

        //Assert
        result.Should().BeEquivalentTo(applicationTypeQuerable);
    }

}
