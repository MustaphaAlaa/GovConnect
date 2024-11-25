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

namespace DVLD_Tests.ApplicationServices
{
    public class GetAllApplicationFeesServiceTest
    {
        private readonly IFixture _fixture;
        private readonly IGetAllApplicationFees _getAllApplicationFees;
        private readonly Mock<IGetAllRepository<ApplicationFees>> _getAllRepository;
        private readonly Mock<IMapper> _mapper;

        public GetAllApplicationFeesServiceTest()
        {
            _fixture = new Fixture();
            _getAllRepository = new Mock<IGetAllRepository<ApplicationFees>>();
            _mapper = new Mock<IMapper>();

            _getAllApplicationFees = new GetAllApplicationsFeesService(_getAllRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task GetAllApplicationsFees_EmptyDb_ReturnEmptyList()
        {
            //Arrange
            _getAllRepository.Setup(temp => temp.GetAllAsync()).ReturnsAsync(new List<ApplicationFees>() { });

            //Act
            var result = await _getAllApplicationFees.GetAllAsync();

            //Assert
            result.Should().BeEmpty();
        }


        [Fact]
        public async Task GetAllApplicationsFees_DbHasData_ReturnApplicationFeesList()
        {
            //Arrange
            List<ApplicationFees> applicationFeesList = new()
            {
                new ApplicationFees() { ApplicationTypeId = 1,  ApplicationForId = 2, Fees = 200, LastUpdate = DateTime.Now},
                new ApplicationFees() { ApplicationTypeId = 1,  ApplicationForId = 1, Fees = 300, LastUpdate = DateTime.Now},
                new ApplicationFees() { ApplicationTypeId = 3,  ApplicationForId = 2, Fees = 100, LastUpdate = DateTime.Now},
                new ApplicationFees() { ApplicationTypeId = 1,  ApplicationForId = 3, Fees = 500, LastUpdate = DateTime.Now},
            };

            List<ApplicationFeesDTO> applicationFeesDTOs = applicationFeesList
                .Select(appFees => new ApplicationFeesDTO
                {
                    ApplicationForId = appFees.ApplicationForId,
                    ApplicationTypeId = appFees.ApplicationTypeId,
                    Fees = appFees.Fees,
                    LastUdpate = appFees.LastUpdate
                }).ToList();

            _getAllRepository.Setup(temp => temp.GetAllAsync()).ReturnsAsync(applicationFeesList);


            _mapper.Setup(temp => temp.Map<ApplicationFeesDTO>(It.IsAny<ApplicationFees>()))
                .Returns((ApplicationFees source) => new ApplicationFeesDTO
                {
                    ApplicationForId = source.ApplicationForId,
                    ApplicationTypeId = source.ApplicationTypeId,
                    Fees = source.Fees,
                    LastUdpate = source.LastUpdate
                });

            //Act
            var result = await _getAllApplicationFees.GetAllAsync();

            //Assert
            result.Should().BeEquivalentTo(applicationFeesDTOs);
        }

        [Fact]
        public async Task GetAllApplicationsFeesExpr_EmptyDb_ReturnEmptyList()
        {
            //Arrange
            IQueryable<ApplicationFees> emptyQueryable = new List<ApplicationFees>() { }.AsQueryable();

            _getAllRepository.Setup(temp => temp.GetAllAsync(It.IsAny<Expression<Func<ApplicationFees, bool>>>()))
                .ReturnsAsync(emptyQueryable);

            //Act
            var result = await _getAllApplicationFees.GetAllAsync(It.IsAny<Expression<Func<ApplicationFees, bool>>>());

            //Assert
            result.Should().BeEmpty();
        }


        [Fact]
        public async Task GetAllApplicationsFeesExpr_DbHasData_ReturnApplicationFeesQueryable()
        {
            //Arrange
            IQueryable<ApplicationFees> applicationFeesQueryable = new List<ApplicationFees>()
            {
                new ApplicationFees() { ApplicationTypeId = 1,  ApplicationForId = 2, Fees = 200, LastUpdate = DateTime.Now},
                new ApplicationFees() { ApplicationTypeId = 1,  ApplicationForId = 1, Fees = 300, LastUpdate = DateTime.Now},
                new ApplicationFees() { ApplicationTypeId = 3,  ApplicationForId = 2, Fees = 100, LastUpdate = DateTime.Now},
                new ApplicationFees() { ApplicationTypeId = 1,  ApplicationForId = 3, Fees = 500, LastUpdate = DateTime.Now},
            }.AsQueryable();


            IQueryable<ApplicationFeesDTO> expectedApplicationFeesQueryable = applicationFeesQueryable
                .Select(appFees => new ApplicationFeesDTO()
                {
                    ApplicationForId = appFees.ApplicationForId,
                    ApplicationTypeId = appFees.ApplicationTypeId,
                    Fees = appFees.Fees,
                    LastUdpate = appFees.LastUpdate
                }).AsQueryable();

            _mapper.Setup(temp => temp.Map<ApplicationFeesDTO>(It.IsAny<ApplicationFees>()))
                .Returns((ApplicationFees source) => new ApplicationFeesDTO
                {
                    ApplicationForId = source.ApplicationForId,
                    ApplicationTypeId = source.ApplicationTypeId,
                    Fees = source.Fees,
                    LastUdpate = source.LastUpdate

                });


            _getAllRepository.Setup(temp => temp.GetAllAsync(It.IsAny<Expression<Func<ApplicationFees, bool>>>()))
               .ReturnsAsync(applicationFeesQueryable);

            //Act
            var result = await _getAllApplicationFees.GetAllAsync(It.IsAny<Expression<Func<ApplicationFees, bool>>>());

            //Assert
            result.Should().BeEquivalentTo(expectedApplicationFeesQueryable);
        }
    }
}
