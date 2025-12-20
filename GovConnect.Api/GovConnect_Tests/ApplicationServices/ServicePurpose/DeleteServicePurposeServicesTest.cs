using FluentAssertions;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices.Purpose;
using System.Linq.Expressions;
using IServices.IApplicationServices.IPurpose;
using IRepository.IGenericRepositories;


namespace GovConnect_Tests.ApplicationServices;

public class DeleteServicePurposeServicesTest
{
    private readonly IDeleteServicePurpose _deleteServicePurpose;
    private readonly Mock<IDeleteRepository<ServicePurpose>> _deleteRepositoryMock;

    public DeleteServicePurposeServicesTest()
    {
        _deleteRepositoryMock = new Mock<IDeleteRepository<ServicePurpose>>();
        _deleteServicePurpose = new DeleteApplicationPurposeService(_deleteRepositoryMock.Object);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task DeleteAsync_InvalidId_ThrowArgumentOutOfRangeException(int id)
    {
        Func<Task> result = async () => await _deleteServicePurpose.DeleteAsync(id);

        //Assert
        await result.Should().ThrowAsync<ArgumentOutOfRangeException>();

    }

    [Fact]
    public async Task DeleteAsync_ValidId_ReturnNumber1()
    {

        _deleteRepositoryMock.Setup(temp => temp.DeleteAsync(It.IsAny<Expression<Func<ServicePurpose, bool>>>())).ReturnsAsync(1);

        var result = await _deleteServicePurpose.DeleteAsync(2);

        //Assert
        result.Should().BeTrue();

    }

}
