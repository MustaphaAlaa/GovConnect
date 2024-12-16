using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.Category;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices.Purpose;
using System.Linq.Expressions;
using IServices.IApplicationServices.Purpose;


namespace GovConnect_Tests.ApplicationServices;

public class DeleteApplicationPurposeServicesTest
{
    private readonly IDeleteApplicationPurpose _deleteApplicationPurpose;
    private readonly Mock<IDeleteRepository<ApplicationPurpose>> _deleteRepositoryMock;

    public DeleteApplicationPurposeServicesTest()
    {
        _deleteRepositoryMock = new Mock<IDeleteRepository<ApplicationPurpose>>();
        _deleteApplicationPurpose = new DeleteApplicationPurposeService(_deleteRepositoryMock.Object);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task DeleteAsync_InvalidId_ThrowArgumentOutOfRangeException(int id)
    {
        Func<Task> result = async () => await _deleteApplicationPurpose.DeleteAsync(id);

        //Assert
        await result.Should().ThrowAsync<ArgumentOutOfRangeException>();

    }

    [Fact]
    public async Task DeleteAsync_ValidId_ReturnNumber1()
    {

        _deleteRepositoryMock.Setup(temp => temp.DeleteAsync(It.IsAny<Expression<Func<ApplicationPurpose, bool>>>())).ReturnsAsync(1);

        var result = await _deleteApplicationPurpose.DeleteAsync(2);

        //Assert
        result.Should().BeTrue();

    }

}
