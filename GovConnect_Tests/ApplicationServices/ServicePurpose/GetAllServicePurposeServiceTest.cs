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

public class GetAllServicePurposeServiceTest
{
    private readonly IFixture _fixture;
    private readonly IGetAllServicePurpose _getAllServicesPurposes;
    private readonly Mock<IGetAllRepository<ServicePurpose>> _getAllRepositoryMock;

    private readonly Mock<IMapper> _mapper;
    private readonly IMapper _mapperObj;

    public GetAllServicePurposeServiceTest()
    {
        _fixture = new Fixture();
        _getAllRepositoryMock = new Mock<IGetAllRepository<ServicePurpose>>();
        _mapper = new Mock<IMapper>();
        _mapperObj = _mapper.Object;
        _getAllServicesPurposes = new GetAllApplicationPurposesService(_getAllRepositoryMock.Object, _mapperObj);
    }

    [Fact]
    public async Task GetAllAsync_EmptyDb_ReturnEmptyList()
    {
        //Arrange
        List<ServicePurpose> applicationTypeDTOs = new List<ServicePurpose>() { };
        _getAllRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(applicationTypeDTOs);

        //Act
        List<ServicePurposeDTO> result = await _getAllServicesPurposes.GetAllAsync();

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_DbHasData_ReturnApplicationDTOsList()
    {
        //Arrange
        List<ServicePurpose> servicesPurposesList = new() {
          _fixture.Create<ServicePurpose>(),
            _fixture.Create<ServicePurpose>(),
            _fixture.Create<ServicePurpose>(),
            _fixture.Create<ServicePurpose>(),
        };


        List<ServicePurposeDTO> servicePurposeDTOs = servicesPurposesList
            .Select(app => new ServicePurposeDTO() { Purpose = app.Purpose, ServicePurposeId = app.ServicePurposeId })
            .ToList();

        _mapper.Setup(temp => temp.Map<ServicePurposeDTO>(It.IsAny<ServicePurpose>()))
            .Returns((ServicePurpose source) => new ServicePurposeDTO()
            {
                Purpose = source.Purpose,
                ServicePurposeId = source.ServicePurposeId
            });

        _getAllRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(servicesPurposesList);

        //Act
        List<ServicePurposeDTO> result = await _getAllServicesPurposes.GetAllAsync();

        //Assert
        result.Should().BeEquivalentTo(servicePurposeDTOs);
    }
    [Fact]
    public async Task GetAllExpressionAsync_EmptyDb_ReturnEmptyList()
    {
        //Arrange
        List<ServicePurpose> servicePurposelist = new List<ServicePurpose>() { };
        IQueryable<ServicePurpose> servicePurposeQuerable = servicePurposelist.AsQueryable();

        Expression<Func<ServicePurpose, bool>> expression = app => app.Purpose == "duumy";

        _getAllRepositoryMock.Setup(temp =>
                                temp.GetAllAsync(expression))
                           .ReturnsAsync(servicePurposeQuerable);

        //Act
        IQueryable<ServicePurposeDTO> result = await _getAllServicesPurposes.GetAllAsync(expression);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllExpressingAsync_DbHasData_ReturnApplicationDTOsList()
    {
        //Arrange
        List<ServicePurpose> servicePurposelist = new() {
          _fixture.Create<ServicePurpose>(),
            _fixture.Create<ServicePurpose>(),
            _fixture.Create<ServicePurpose>(),
            _fixture.Create<ServicePurpose>(),
        };


        IQueryable<ServicePurpose> servicePurposeQuerable = servicePurposelist.AsQueryable();



        _mapper.Setup(temp => temp.Map<ServicePurposeDTO>(It.IsAny<ServicePurpose>()))
            .Returns((ServicePurpose source) => new ServicePurposeDTO()
            {
                Purpose = source.Purpose,
                ServicePurposeId = source.ServicePurposeId
            });

        Expression<Func<ServicePurpose, bool>> expression = app => app.Purpose == "duumy";

        _getAllRepositoryMock.Setup(x => x.GetAllAsync(expression)).ReturnsAsync(servicePurposeQuerable);

        //Act
        IQueryable<ServicePurposeDTO> result = await _getAllServicesPurposes.GetAllAsync(expression);

        //Assert
        result.Should().BeEquivalentTo(servicePurposeQuerable);
    }

}
