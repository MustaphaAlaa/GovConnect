using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.Category;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices.For;
using Services.ApplicationServices.Purpose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IServices.IApplicationServices.Purpose;

namespace GovConnect_Tests.ApplicationServices
{
    public class GetApplicationPurposeServiceTest
    {
        private readonly IGetApplicationPurpose _getApplicationPurposeServices;
        private readonly IGetRepository<ApplicationPurpose> _getRepository;
        private readonly Mock<IGetRepository<ApplicationPurpose>> _getRepositoryMock;
        private readonly Mock<IMapper> _mapper;


        public GetApplicationPurposeServiceTest()
        {
            _getRepositoryMock = new Mock<IGetRepository<ApplicationPurpose>>();
            _mapper = new Mock<IMapper>();

            _getApplicationPurposeServices = new GetApplicationPurposeService(_getRepositoryMock.Object, _mapper.Object);
        }

        [Fact]
        public async Task GetAsync_ApplicationDoesNotExist_ReturnNull()
        {
            //Arrange
            Expression<Func<ApplicationPurpose, bool>> expression = app => app.ApplicationPurposeId == 1;

            _getRepositoryMock.Setup(temp => temp.GetAsync(expression)).ReturnsAsync(null as ApplicationPurpose);

            //Act
            var result = await _getApplicationPurposeServices.GetByAsync(expression);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAsync_ApplicationDoesExist_ReturnApplicationFor()
        {
            //Arrange
            ApplicationPurpose app = new ApplicationPurpose()
            {
                ApplicationPurposeId = 8,
                Purpose = "New"
            };

            _getRepositoryMock.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationPurpose, bool>>>())).ReturnsAsync(app);

            //Act
            var result = await _getApplicationPurposeServices.GetByAsync(app => app.ApplicationPurposeId == 1);

            //Assert
            result.Should().BeEquivalentTo(app);
        }





    }
}
