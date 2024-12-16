using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.Fees;
using ModelDTO.ApplicationDTOs.Fees;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices.Fees;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GovConnect_Tests.ApplicationServices
{
    public class GetAllApplicationFeesServiceTest
    {
        private readonly IFixture _fixture;
        private readonly IGetAllServiceFees _iGetAllServiceFees;
        private readonly Mock<IGetAllRepository<ServiceFees>> _getAllRepository;
        private readonly Mock<IMapper> _mapper;

        public GetAllApplicationFeesServiceTest()
        {
            _fixture = new Fixture();
            _getAllRepository = new Mock<IGetAllRepository<ServiceFees>>();
            _mapper = new Mock<IMapper>();

            _iGetAllServiceFees = new IGetAllServicesFeesService(_getAllRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task GetAllApplicationsFees_EmptyDb_ReturnEmptyList()
        {
            //Arrange
            _getAllRepository.Setup(temp => temp.GetAllAsync()).ReturnsAsync(new List<ServiceFees>() { });

            //Act
            var result = await _iGetAllServiceFees.GetAllAsync();

            //Assert
            result.Should().BeEmpty();
        }


        [Fact]
        public async Task GetAllApplicationsFees_DbHasData_ReturnApplicationFeesList()
        {
            //Arrange
            List<ServiceFees> applicationFeesList = new()
            {
                new ServiceFees() { ApplicationTypeId = 1,  ServiceCategoryId = 2, Fees = 200, LastUpdate = DateTime.Now},
                new ServiceFees() { ApplicationTypeId = 1,  ServiceCategoryId = 1, Fees = 300, LastUpdate = DateTime.Now},
                new ServiceFees() { ApplicationTypeId = 3,  ServiceCategoryId = 2, Fees = 100, LastUpdate = DateTime.Now},
                new ServiceFees() { ApplicationTypeId = 1,  ServiceCategoryId = 3, Fees = 500, LastUpdate = DateTime.Now},
            };

            List<ServiceFeesDTO> applicationFeesDTOs = applicationFeesList
                .Select(appFees => new ServiceFeesDTO
                {
                    ServiceCategoryId = appFees.ServiceCategoryId,
                    ApplicationPuropseId = appFees.ApplicationTypeId,
                    Fees = appFees.Fees,
                    LastUpdate = appFees.LastUpdate
                }).ToList();

            _getAllRepository.Setup(temp => temp.GetAllAsync()).ReturnsAsync(applicationFeesList);


            _mapper.Setup(temp => temp.Map<ServiceFeesDTO>(It.IsAny<ServiceFees>()))
                .Returns((ServiceFees source) => new ServiceFeesDTO
                {
                    ServiceCategoryId = source.ServiceCategoryId,
                    ApplicationPuropseId = source.ApplicationTypeId,
                    Fees = source.Fees,
                    LastUpdate = source.LastUpdate
                });

            //Act
            var result = await _iGetAllServiceFees.GetAllAsync();

            //Assert
            result.Should().BeEquivalentTo(applicationFeesDTOs);
        }

        [Fact]
        public async Task GetAllApplicationsFeesExpr_EmptyDb_ReturnEmptyList()
        {
            //Arrange
            IQueryable<ServiceFees> emptyQueryable = new List<ServiceFees>() { }.AsQueryable();

            _getAllRepository.Setup(temp => temp.GetAllAsync(It.IsAny<Expression<Func<ServiceFees, bool>>>()))
                .ReturnsAsync(emptyQueryable);

            //Act
            var result = await _iGetAllServiceFees.GetAllAsync(It.IsAny<Expression<Func<ServiceFees, bool>>>());

            //Assert
            result.Should().BeEmpty();
        }


        [Fact]
        public async Task GetAllApplicationsFeesExpr_DbHasData_ReturnApplicationFeesQueryable()
        {
            //Arrange
            IQueryable<ServiceFees> applicationFeesQueryable = new List<ServiceFees>()
            {
                new ServiceFees() { ApplicationTypeId = 1,  ServiceCategoryId = 2, Fees = 200, LastUpdate = DateTime.Now},
                new ServiceFees() { ApplicationTypeId = 1,  ServiceCategoryId = 1, Fees = 300, LastUpdate = DateTime.Now},
                new ServiceFees() { ApplicationTypeId = 3,  ServiceCategoryId = 2, Fees = 100, LastUpdate = DateTime.Now},
                new ServiceFees() { ApplicationTypeId = 1,  ServiceCategoryId = 3, Fees = 500, LastUpdate = DateTime.Now},
            }.AsQueryable();


            IQueryable<ServiceFeesDTO> expectedApplicationFeesQueryable = applicationFeesQueryable
                .Select(appFees => new ServiceFeesDTO()
                {
                    ServiceCategoryId = appFees.ServiceCategoryId,
                    ApplicationPuropseId = appFees.ApplicationTypeId,
                    Fees = appFees.Fees,
                    LastUpdate = appFees.LastUpdate
                }).AsQueryable();

            _mapper.Setup(temp => temp.Map<ServiceFeesDTO>(It.IsAny<ServiceFees>()))
                .Returns((ServiceFees source) => new ServiceFeesDTO
                {
                    ServiceCategoryId = source.ServiceCategoryId,
                    ApplicationPuropseId = source.ApplicationTypeId,
                    Fees = source.Fees,
                    LastUpdate = source.LastUpdate

                });


            _getAllRepository.Setup(temp => temp.GetAllAsync(It.IsAny<Expression<Func<ServiceFees, bool>>>()))
               .ReturnsAsync(applicationFeesQueryable);

            //Act
            var result = await _iGetAllServiceFees.GetAllAsync(It.IsAny<Expression<Func<ServiceFees, bool>>>());

            //Assert
            result.Should().BeEquivalentTo(expectedApplicationFeesQueryable);
        }
    }
}
