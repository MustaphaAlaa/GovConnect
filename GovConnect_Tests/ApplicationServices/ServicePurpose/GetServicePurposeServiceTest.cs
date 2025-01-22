using AutoMapper;
using FluentAssertions;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices.Purpose;
using System.Linq.Expressions;
using IServices.IApplicationServices.IPurpose;
using IRepository.IGenericRepositories;

namespace GovConnect_Tests.ApplicationServices
{
    public class GetServicePurposeServiceTest
    {
        private readonly IGetServicePurpose _getServicePurposeServices;
        private readonly Mock<IGetRepository<ServicePurpose>> _getRepositoryMock;
        private readonly Mock<IMapper> _mapper;


        public GetServicePurposeServiceTest()
        {
            _getRepositoryMock = new Mock<IGetRepository<ServicePurpose>>();
            _mapper = new Mock<IMapper>();

            _getServicePurposeServices = new GetServicePurposeService(_getRepositoryMock.Object, _mapper.Object);
        }

        [Fact]
        public async Task GetAsync_ServicePurposeDoesNotExist_ReturnNull()
        {
            //Arrange
            Expression<Func<ServicePurpose, bool>> expression = app => app.ServicePurposeId == 1;

            _getRepositoryMock.Setup(temp => temp.GetAsync(expression)).ReturnsAsync(null as ServicePurpose);

            //Act
            var result = await _getServicePurposeServices.GetByAsync(expression);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAsync_ServicePurposeDoesExist_ReturnServicePurpose()
        {
            //Arrange
            ServicePurpose app = new ServicePurpose()
            {
                ServicePurposeId = 8,
                Purpose = "New"
            };

            _getRepositoryMock.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ServicePurpose, bool>>>())).ReturnsAsync(app);

            //Act
            var result = await _getServicePurposeServices.GetByAsync(app => app.ServicePurposeId == 1);

            //Assert
            result.Should().BeEquivalentTo(app);
        }





    }
}
