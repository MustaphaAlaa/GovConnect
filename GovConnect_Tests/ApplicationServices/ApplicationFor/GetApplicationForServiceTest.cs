using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.For;
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
    public class GetApplicationForServiceTest
    {
        private readonly IGetApplicationFor _getApplicationForServices;
        private readonly IGetRepository<ApplicationFor> _getRepository;
        private readonly Mock<IGetRepository<ApplicationFor>> _getRepositoryMock;
        private readonly Mock<IMapper> _mapper;


        public GetApplicationForServiceTest()
        {
            _getRepositoryMock = new Mock<IGetRepository<ApplicationFor>>();
            _mapper = new Mock<IMapper>();

            _getApplicationForServices = new GetApplicationForService(_getRepositoryMock.Object, _mapper.Object);
        }

        [Fact]
        public async Task GetAsync_ApplicationDoesNotExist_ReturnNull()
        {
            //Arrange
            Expression<Func<ApplicationFor, bool>> expression = app => app.Id == 1;

            _getRepositoryMock.Setup(temp => temp.GetAsync(expression)).ReturnsAsync(null as ApplicationFor);

            //Act
            var result = await _getApplicationForServices.GetByAsync(expression);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAsync_ApplicationDoesExist_ReturnApplicationFor()
        {
            //Arrange
            ApplicationFor app = new ApplicationFor()
            {
                Id = 8,
                For = "DrivingLicense"
            };

            _getRepositoryMock.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationFor, bool>>>())).ReturnsAsync(app);

            //Act
            var result = await _getApplicationForServices.GetByAsync(app => app.Id == 1);

            //Assert
            result.Should().BeEquivalentTo(app);
        }





    }
}
