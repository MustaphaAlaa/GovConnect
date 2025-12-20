using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IRepository.IGenericRepositories;
using IServices.ICountryServices;
using ModelDTO.CountryDTOs;
using Models.Countries;
using Moq;
using Services.CountryServices;
using System.Linq.Expressions;
using Web.Mapper;
namespace GovConnect_Tests.CountryServices
{
    public class CreateCountrySeviceTest
    {
        private readonly IFixture _fixture;


        private readonly ICreateCountry _createCountry;
        private readonly ICreateRepository<Country> _createRepository;
        private readonly Mock<ICreateRepository<Country>> _createRepositoryMock;

        private readonly IGetCountry _getCountry;
        private readonly IGetRepository<Country> _getCountryRepository;
        private readonly Mock<IGetRepository<Country>> _getRepositoryMock;

        public CreateCountrySeviceTest()
        {
            _fixture = new Fixture();

            var mapperCfg = new MapperConfiguration(cfg => cfg.AddProfile(typeof(GovConnectMapperConfig)));


            _createRepositoryMock = new Mock<ICreateRepository<Country>>();
            _getRepositoryMock = new Mock<IGetRepository<Country>>();

            _getCountry = new GetCountryService(_getRepositoryMock.Object, new Mapper(mapperCfg));

            _createCountry = new CreateCountryService(_createRepositoryMock.Object, _getCountry, new Mapper(mapperCfg));
        }

        [Fact]
        public async Task CreateCountry_NullReq_returnArgumentNullException()
        {
            //Arrange //Act
            Func<Task> action = async () => await _createCountry.CreateAsync(null);

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateCountry_CountryNameIsNull_returnNull()
        {
            //Arrange  
            CreateCountryRequest country = new CreateCountryRequest() { Name = null };

            //Act
            Func<Task> action = async () => await _createCountry.CreateAsync(country);


            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateCountry_CountryNameIsEmpty_returnNull()
        {
            //Arrange //Act     
            CreateCountryRequest country = new CreateCountryRequest() { Name = "" };

            //Act
            Func<Task> action = async () => await _createCountry.CreateAsync(country);


            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateCountry_CountryNameIsAlreadyExist_returnNull()
        {
            //Arrange
            CreateCountryRequest countryRequest = _fixture.Create<CreateCountryRequest>();
            countryRequest.Name = countryRequest.Name.ToLower();

            var newCountry = new Country() { CountryId = 1973, CountryName = countryRequest.Name };


            _getRepositoryMock
                .Setup(temp => temp.GetAsync(It.Is<Expression<Func<Country, bool>>>(expr =>
                        expr.Compile()(newCountry))))
                .ReturnsAsync(newCountry);

            //Act

            Func<Task> action = async () => await _createCountry.CreateAsync(countryRequest);

            //Asser
            await action.Should().ThrowAsync<InvalidOperationException>();

        }

        [Fact]
        public async Task CreateCountry_ValidCreateCountryRequest_returnCountryDTO()
        {
            //Arrange
            CreateCountryRequest countryRequest = new() { Name = "Qahera" };
            countryRequest.Name = countryRequest.Name.ToLower();

            Country newCountry = new Country() { CountryId = 1998, CountryName = countryRequest.Name };

            _createRepositoryMock.Setup(temp => temp.CreateAsync(It.IsAny<Country>())).ReturnsAsync(newCountry);
            _getRepositoryMock.Setup(temp => temp.GetAsync(c => c.CountryId == 1998)).ReturnsAsync(newCountry);

            //Act
            var result = await _createCountry.CreateAsync(countryRequest);

            //Asset
            result.CountryName.Should().Be(countryRequest.Name);
        }
    }
}
