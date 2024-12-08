using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.For;
using IServices.IApplicationServices.Type;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices.For;
using Services.ApplicationServices.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GovConnect_Tests.ApplicationServices
{
    public class GetApplicationTypeServiceTest
    {
        private readonly IGetApplicationType _getApplicationTypeServices;
        private readonly IGetRepository<ApplicationType> _getRepository;
        private readonly Mock<IGetRepository<ApplicationType>> _getRepositoryMock;
        private readonly Mock<IMapper> _mapper;


        public GetApplicationTypeServiceTest()
        {
            _getRepositoryMock = new Mock<IGetRepository<ApplicationType>>();
            _mapper = new Mock<IMapper>();

            _getApplicationTypeServices = new GetApplicationTypeService(_getRepositoryMock.Object, _mapper.Object);
        }

        [Fact]
        public async Task GetAsync_ApplicationDoesNotExist_ReturnNull()
        {
            //Arrange
            Expression<Func<ApplicationType, bool>> expression = app => app.Id == 1;

            _getRepositoryMock.Setup(temp => temp.GetAsync(expression)).ReturnsAsync(null as ApplicationType);

            //Act
            var result = await _getApplicationTypeServices.GetByAsync(expression);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAsync_ApplicationDoesExist_ReturnApplicationFor()
        {
            //Arrange
            ApplicationType app = new ApplicationType()
            {
                Id = 8,
                Type = "New"
            };

            _getRepositoryMock.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationType, bool>>>())).ReturnsAsync(app);

            //Act
            var result = await _getApplicationTypeServices.GetByAsync(app => app.Id == 1);

            //Assert
            result.Should().BeEquivalentTo(app);
        }





    }
}
