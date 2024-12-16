using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.Category;
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
    private readonly IDeleteServiceCategory _iDeleteServiceCategory;
    private readonly Mock<IDeleteRepository<ServiceCategory>> _deleteRepositoryMock;

    public DeleteApplicationForServicesTest()
    {
        _deleteRepositoryMock = new Mock<IDeleteRepository<ServiceCategory>>();
        _iDeleteServiceCategory = new DeleteServiceCategoryService(_deleteRepositoryMock.Object);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task DeleteAsync_InvalidId_ThrowArgumentOutOfRangeException(int id)
    {
        Func<Task> result = async () => await _iDeleteServiceCategory.DeleteAsync(id);

        //Assert
        await result.Should().ThrowAsync<ArgumentOutOfRangeException>();

    }

    [Fact]
    public async Task DeleteAsync_ValidId_ReturnNumber1()
    {

        _deleteRepositoryMock.Setup(temp => temp.DeleteAsync(It.IsAny<Expression<Func<ServiceCategory, bool>>>())).ReturnsAsync(1);

        var result = await _iDeleteServiceCategory.DeleteAsync(2);

        //Assert
        result.Should().BeTrue();

    }

}
