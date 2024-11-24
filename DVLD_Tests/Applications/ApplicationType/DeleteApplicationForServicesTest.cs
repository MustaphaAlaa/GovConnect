using FluentAssertions;
using IRepository;
using IServices.Application.Type;
using Models.ApplicationModels;
using Moq;
using Services.Application.Type;
using System.Linq.Expressions;


namespace DVLD_Tests.Applications;

public class DeleteApplicationTypeServicesTest
{
    private readonly IDeleteApplicationType _deleteApplicationType;
    private readonly Mock<IDeleteRepository<ApplicationType>> _deleteRepositoryMock;

    public DeleteApplicationTypeServicesTest()
    {
        _deleteRepositoryMock = new Mock<IDeleteRepository<ApplicationType>>();
        _deleteApplicationType = new DeleteApplicationTypeService(_deleteRepositoryMock.Object);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task DeleteAsync_InvalidId_ThrowArgumentOutOfRangeException(int id)
    {
        Func<Task> result = async () => await _deleteApplicationType.DeleteAsync(id);

        //Assert
        await result.Should().ThrowAsync<ArgumentOutOfRangeException>();

    }

    [Fact]
    public async Task DeleteAsync_ValidId_ReturnNumber1()
    {

        _deleteRepositoryMock.Setup(temp => temp.DeleteAsync(It.IsAny<Expression<Func<ApplicationType, bool>>>())).ReturnsAsync(1);

        var result = await _deleteApplicationType.DeleteAsync(2);

        //Assert
        result.Should().BeTrue();

    }

}
