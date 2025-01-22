using AutoMapper;
using FluentAssertions;
using IRepository.IGenericRepositories;
using IServices.IApplicationServices.Category;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices.For;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GovConnect_Tests.ApplicationServices
{
    public class GetServiceCategoryServiceTest
    {
        private readonly IGetServiceCategory _iGetServiceCategoryServices;
        private readonly IGetRepository<ServiceCategory> _getRepository;
        private readonly Mock<IGetRepository<ServiceCategory>> _getRepositoryMock;
        private readonly Mock<IMapper> _mapper;


        public GetServiceCategoryServiceTest()
        {
            _getRepositoryMock = new Mock<IGetRepository<ServiceCategory>>();
            _mapper = new Mock<IMapper>();

            _iGetServiceCategoryServices = new GetServiceCategoryService(_getRepositoryMock.Object, _mapper.Object);
        }

        [Fact]
        public async Task GetAsync_ApplicationDoesNotExist_ReturnNull()
        {
            //Arrange
            Expression<Func<ServiceCategory, bool>> expression = app => app.ServiceCategoryId == 1;

            _getRepositoryMock.Setup(temp => temp.GetAsync(expression)).ReturnsAsync(null as ServiceCategory);

            //Act
            var result = await _iGetServiceCategoryServices.GetByAsync(expression);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAsync_ApplicationDoesExist_ReturnApplicationFor()
        {
            //Arrange
            ServiceCategory app = new ServiceCategory()
            {
                ServiceCategoryId = 8,
                Category = "DrivingLicense"
            };

            _getRepositoryMock.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ServiceCategory, bool>>>())).ReturnsAsync(app);

            //Act
            var result = await _iGetServiceCategoryServices.GetByAsync(app => app.ServiceCategoryId == 1);

            //Assert
            result.Should().BeEquivalentTo(app);
        }





    }
}
