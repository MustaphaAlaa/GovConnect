using AutoFixture;
using AutoMapper;
using DataConfigurations;
using FluentAssertions;
using IServices.ICountryServices;
using ModelDTO.CountryDTOs;
using Models.Countries;
using Models.Users;
using Moq;
using Services.CountryServices;
using System.Linq.Expressions;
using Models.LicenseModels;
using Web.Mapper;
using IRepository.IGenericRepositories;

namespace GovConnect_Tests.CountryServices
{
    public class UpdateCountrySeviceTest
    {
        private readonly IFixture _fixture;

        private readonly IUpdateCountry _updateCountry;
        private readonly IUpdateRepository<Country> _updateCountryRepository;
        private readonly Mock<IUpdateRepository<Country>> _updateRepositoryMock;

        //private readonly IGetCountry _getTestType;
        private readonly IGetRepository<Country> _getCountryRepository;
        private readonly Mock<IGetRepository<Country>> _getRepositoryMock;

        private readonly Mock<IMapper> _mapper;
        public UpdateCountrySeviceTest()
        {
            _fixture = new Fixture();
            _mapper = new Mock<IMapper>();

            var mapperCfg = new MapperConfiguration(cfg => cfg.AddProfile(typeof(GovConnectMapperConfig)));

            _updateRepositoryMock = new Mock<IUpdateRepository<Country>>();
            _getRepositoryMock = new Mock<IGetRepository<Country>>();

            _updateCountry = new UpdateCountryService(_updateRepositoryMock.Object,
                                                      _getRepositoryMock.Object,
                                                      _mapper.Object);

        }


        [Fact]
        public async Task UpdateAsync_ArgIsNull_ThrowArgumentNullException()
        {
            Func<Task> action = async () => await _updateCountry.UpdateAsync(null);

            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateAsync_ArgPropIsNull_ThrowArgumentException()
        {
            //Arrange 
            UpdateCountryRequest updateCountryRequest = new UpdateCountryRequest() { Name = null };

            Func<Task> action = async () => await _updateCountry.UpdateAsync(updateCountryRequest);

            await action.Should().ThrowAsync<ArgumentException>();
        }


        [Fact]
        public async Task UpdateAsync_NameIsEmpty_ThrowArgumentException()
        {
            //Arrange 
            UpdateCountryRequest updateCountryRequest = new UpdateCountryRequest() { Name = "" };

            Func<Task> action = async () => await _updateCountry.UpdateAsync(updateCountryRequest);

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task UpdateAsync_IdIsZero_ThrowArgumentException()
        {
            //Arrange 
            UpdateCountryRequest updateCountryRequest = new UpdateCountryRequest() { Id = 0, Name = "test" };

            Func<Task> action = async () => await _updateCountry.UpdateAsync(updateCountryRequest);

            await action.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task UpdateAsync_IdIsLessThanZero_ThrowArgumentException()
        {
            //Arrange 
            UpdateCountryRequest updateCountryRequest = new UpdateCountryRequest() { Id = -23, Name = "test" };

            //Act
            Func<Task> action = async () => await _updateCountry.UpdateAsync(updateCountryRequest);

            //Assert
            await action.Should().ThrowAsync<InvalidOperationException>();
        }



        [Fact]
        public async Task UpdateAsync_CountryDoesNotExist_ThrowInvalidException()
        {
            //Arrange 
            UpdateCountryRequest updateCountryRequest = new UpdateCountryRequest() { Id = 19975, Name = "test" };

            Expression<Func<Country, bool>> expression = c => c.CountryId == updateCountryRequest.Id;

            _getRepositoryMock.Setup(temp => temp.GetAsync(expression)).ThrowsAsync(new InvalidOperationException());

            //Act
            Func<Task> action = async () => await _updateCountry.UpdateAsync(updateCountryRequest);

            await action.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task UpdateAsync_CountryExist_Return()
        {
            //Arrange 
            Country country = _fixture.Build<Country>()
                .With(c => c.Users, null as List<User>)
                .With(c=> c.localDrivingLicenses, null as List<LocalDrivingLicense>) 
                .Create();

            _getRepositoryMock.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<Country, bool>>>())).ReturnsAsync(country);

            _updateRepositoryMock.Setup(temp => temp.UpdateAsync(It.IsAny<Country>())).ReturnsAsync(country);

            _mapper.Setup(m => m.Map<CountryDTO>(It.IsAny<Country>()))
                     .Returns((Country countrySource) => new CountryDTO() { CountryCode = countrySource.CountryCode, CountryName = countrySource.CountryName });

            UpdateCountryRequest updateCountryRequest = new UpdateCountryRequest() { Id = country.CountryId, Name = $"update test {country.CountryName}" };

            CountryDTO expected = new CountryDTO()
            {
                CountryCode = country.CountryCode,
                CountryName = updateCountryRequest.Name,
            };

            //Act
            var action = await _updateCountry.UpdateAsync(updateCountryRequest);

            //Assert
            action.Should().BeEquivalentTo(expected);
        }




    }
}
