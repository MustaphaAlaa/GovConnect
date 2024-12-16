
using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.Fees;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices;
using Services.ApplicationServices.Fees;
using System.Linq.Expressions;

namespace GovConnect_Tests.ApplicationServices;

public class GetApplicationFeesTest
{
    private readonly Mock<IMapper> _mapper;

    private readonly Mock<IGetRepository<ServiceFees>> _getRepository;
    private readonly IGetApplicationFees _getApplicationFees;
    public GetApplicationFeesTest()
    {
        _mapper = new Mock<IMapper>();
        _getRepository = new Mock<IGetRepository<ServiceFees>>();

        _getApplicationFees = new GetServiceFeesService(_getRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetApplicationFees_ApplicationFeesDoesNotExist_ReturnNull()
    {
        //Arrange
        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ServiceFees, bool>>>()))
           .ReturnsAsync(null as ServiceFees);

        //Act
        var result = await _getApplicationFees.GetByAsync(app => app.Fees > 500);

        //Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetApplicationFees_ApplicationFeesDoesExist_ReturnApplicationFeesObj()
    {
        //Arrange
        ServiceFees serviceFees = new()
        {
            ServiceCategoryId = 1,
            ApplicationTypeId = 2,
            Fees = 100,
            LastUpdate = DateTime.Now,
        };

        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ServiceFees, bool>>>()))
           .ReturnsAsync(serviceFees);

        //Act
        var result = await _getApplicationFees.GetByAsync(app => app.Fees > 500);

        //Assert
        result.Should().BeEquivalentTo(serviceFees);
    }
}
