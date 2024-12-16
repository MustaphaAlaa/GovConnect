using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.Fees;
using ModelDTO.ApplicationDTOs.Fees;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices.Fees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GovConnect_Tests.ApplicationServices.Fees;

public class IDeleteServiceFeesServiceTest
{

    private readonly Mock<IDeleteRepository<ServiceFees>> _deleteRepository;
    private readonly IDeleteServiceFees _iDeleteServiceFees;

    public IDeleteServiceFeesServiceTest()
    {
        _deleteRepository = new Mock<IDeleteRepository<ServiceFees>>();

        _iDeleteServiceFees = new IDeleteServiceFeesService(_deleteRepository.Object);
    }

    [Fact]
    public async Task DeleteAsync_ApplicationFeesDoesNotExist_ReturnFalse()
    {
        //Arrange
        _deleteRepository.Setup(temp => temp.DeleteAsync(It.IsAny<Expression<Func<ServiceFees, bool>>>()))
            .ReturnsAsync(0);

        //Act
        var result = await _iDeleteServiceFees.DeleteAsync(It.IsAny<CompositeKeyForServiceFees>());
     
        //Assert
        result.Should().BeFalse();

    }
    [Fact]
    public async Task DeleteAsync_ApplicationFeesExist_ReturnTrue()
    {
        //Arrange
        _deleteRepository.Setup(temp => temp.DeleteAsync(It.IsAny<Expression<Func<ServiceFees, bool>>>()))
            .ReturnsAsync(1);

        //Act
        var result = await _iDeleteServiceFees.DeleteAsync(It.IsAny<CompositeKeyForServiceFees>());

        //Assert
        result.Should().BeTrue();

    }
}
