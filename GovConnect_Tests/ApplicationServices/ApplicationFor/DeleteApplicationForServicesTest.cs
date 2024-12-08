using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.For;
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

public class DeleteApplicationForServicesTest
{
    private readonly IDeleteApplicationFor _deleteApplicationFor;
    private readonly Mock<IDeleteRepository<ApplicationFor>> _deleteRepositoryMock;

    public DeleteApplicationForServicesTest()
    {
        _deleteRepositoryMock = new Mock<IDeleteRepository<ApplicationFor>>();
        _deleteApplicationFor = new DeleteApplicationForService(_deleteRepositoryMock.Object);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task DeleteAsync_InvalidId_ThrowArgumentOutOfRangeException(int id)
    {
        Func<Task> result = async () => await _deleteApplicationFor.DeleteAsync(id);

        //Assert
        await result.Should().ThrowAsync<ArgumentOutOfRangeException>();

    }

    [Fact]
    public async Task DeleteAsync_ValidId_ReturnNumber1()
    {

        _deleteRepositoryMock.Setup(temp => temp.DeleteAsync(It.IsAny<Expression<Func<ApplicationFor, bool>>>())).ReturnsAsync(1);

        var result = await _deleteApplicationFor.DeleteAsync(2);

        //Assert
        result.Should().BeTrue();

    }

}
