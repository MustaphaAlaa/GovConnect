using AutoFixture;
using AutoMapper;
using DataConfigurations;
using FluentAssertions;
using IRepository;
using IServices.Country;
using IServices.ICountryServices;
using Microsoft.EntityFrameworkCore;
using ModelDTO;
using Models.Types;
using Moq;
using Repositorties;
using Services.CountryServices;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using Web.Mapper;
namespace GovConnect_Tests.CountryServices
{
    public class GetCountrySeviceTest
    {
        private readonly IFixture _fixture;

        private readonly IGetCountry _getCountry;
        private readonly IGetRepository<Country> _getCountryRepository;
        private readonly Mock<IGetRepository<Country>> _getRepositoryMock;

        private readonly IMapper _mapper;
        public GetCountrySeviceTest()
        {
            _fixture = new Fixture();



            var dbContextMock = new Mock<DataConfigurations.GovConnectDbContext>();
            var mapperCfg = new MapperConfiguration(cfg => cfg.AddProfile(typeof(GovConnectMapperConfig)));

            _getRepositoryMock = new Mock<IGetRepository<Country>>();

            _getCountry = new GetCountryService(_getRepositoryMock.Object, new Mapper(mapperCfg));

        }

        [Fact]
        public async Task GetCountryAsync_CountryDoesNotExist_returnNull()
        {
            //Arrange
            Expression<Func<Country?, bool>> predicate = c => c.CountryId == 0;

            _getRepositoryMock.Setup(temp => temp.GetAsync(predicate)).ReturnsAsync(null as Country);

            //Act
            var result = await _getCountry.GetByAsync(predicate);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetCountry_CountryIsExist_returnCountryObj()
        {
            //Arrange
            Country country = new Country() { CountryName = "Russia", CountryId = 5555 };

            Expression<Func<Country?, bool>> predicate = c => c.CountryId == country.CountryId;

            _getRepositoryMock.Setup(temp => temp.GetAsync(predicate)).ReturnsAsync(country);


            //act
            var result = await _getCountry.GetByAsync(predicate);

            //Assert
            result.Should().BeEquivalentTo(country);
        }

        [Fact]
        public async Task GetCountry_CountryObj_ThrowException()
        {
            //Arrange
            Country country = new Country() { CountryName = "Russia", CountryId = 5555 };

            Expression<Func<Country?, bool>> predicate = c => c.CountryId == country.CountryId;

            _getRepositoryMock.Setup(temp => temp.GetAsync(predicate)).ThrowsAsync(new Exception());


            //act
            Func<Task> result = async () => await _getCountry.GetByAsync(predicate);

            //Assert
            await result.Should().ThrowAsync<Exception>();
        }

    }
}
