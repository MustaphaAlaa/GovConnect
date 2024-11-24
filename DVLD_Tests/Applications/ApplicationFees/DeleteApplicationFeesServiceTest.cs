using FluentAssertions;
using IRepository;
using IServices.Application.Fees;
using ModelDTO.Application.Fees;
using Models.ApplicationModels;
using Moq;
using Services.Application.Fees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Tests.Applications.Fees;

public class DeleteApplicationFeesServiceTest
{

    private readonly Mock<IDeleteRepository<ApplicationFees>> _deleteRepository;
    private readonly IDeleteApplicationFees _deleteApplicationFees;

    public DeleteApplicationFeesServiceTest()
    {
        _deleteRepository = new Mock<IDeleteRepository<ApplicationFees>>();

        _deleteApplicationFees = new DeleteApplicationFeesService(_deleteRepository.Object);
    }

    [Fact]
    public async Task DeleteAsync_ApplicationFeesDoesNotExist_ReturnFalse()
    {
        //Arrange
        _deleteRepository.Setup(temp => temp.DeleteAsync(It.IsAny<Expression<Func<ApplicationFees, bool>>>()))
            .ReturnsAsync(0);

        //Act
        var result = await _deleteApplicationFees.DeleteAsync(It.IsAny<CompositeKeyForApplicationFees>());

        //Assert
        result.Should().BeFalse();

    }
    [Fact]
    public async Task DeleteAsync_ApplicationFeesExist_ReturnTrue()
    {
        //Arrange
        _deleteRepository.Setup(temp => temp.DeleteAsync(It.IsAny<Expression<Func<ApplicationFees, bool>>>()))
            .ReturnsAsync(1);

        //Act
        var result = await _deleteApplicationFees.DeleteAsync(It.IsAny<CompositeKeyForApplicationFees>());

        //Assert
        result.Should().BeTrue();

    }
}
