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
using Models.Users;
using Moq;
using Repositorties;
using Services.CountryServices;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using Web.Mapper;
namespace DVLD_Tests.CountryServices
{
    public class GetAllCountriesSeviceTest
    {
        private readonly IFixture _fixture;

        private readonly IGetAllCountries _getCountries;
        private readonly IGetAllRepository<Country> _getAllCountriesRepository;
        private readonly Mock<IGetAllRepository<Country>> _getAllRepositoryMock;

        private readonly Mock<IMapper> _mapper;

        public GetAllCountriesSeviceTest()
        {
            _fixture = new Fixture();
            _mapper = new Mock<IMapper>();

            var dbContextMock = new Mock<DataConfigurations.DVLDDbContext>();

            _getAllRepositoryMock = new Mock<IGetAllRepository<Country>>();

            _getCountries = new GetAllCountriesService(_getAllRepositoryMock.Object, _mapper.Object);

        }


        [Fact]
        public async Task GetAll_DbHasData_ReturnListData()
        {
            //Arrange
            List<Country> countries = new(){
                    _fixture.Build<Country>().With(c=>c.Users, null as List<User>).Create(),
                    _fixture.Build<Country>().With(c=>c.Users, null as List<User>).Create(),
                    _fixture.Build<Country>().With(c=>c.Users, null as List<User>).Create(),
                    _fixture.Build<Country>().With(c=>c.Users, null as List<User>).Create(),

            };

            List<CountryDTO> countryDTOs = countries
                              .Select(c => new CountryDTO { Id = c.Id, CountryName = c.CountryName })
                              .ToList();

            _getAllRepositoryMock.Setup(temp => temp.GetAllAsync()).ReturnsAsync(countries);

            _mapper.Setup(temp => temp.Map<CountryDTO>(It.IsAny<Country>()))
                   .Returns((Country source) => new CountryDTO
                   {
                       CountryName = source.CountryName,
                       Id = source.Id
                   });


            //Act
            var result = await _getCountries.GetAllAsync();

            //Assert
            result.Should().BeEquivalentTo(countryDTOs);
        }


        [Fact]
        public async Task GetAll_DbEmpty_ReturnEmptyList()
        {
            //Arrange
            List<Country> countries = new()
            {

            };

            _getAllRepositoryMock.Setup(temp => temp.GetAllAsync()).ReturnsAsync(countries);

            //Act
            var result = await _getCountries.GetAllAsync();

            //Assert
            result.Should().BeEmpty();

        }



        [Fact]
        public async Task GetAll_DbHasProblem_ThrowException()
        {
            //Arrange
            _getAllRepositoryMock.Setup(temp => temp.GetAllAsync()).ThrowsAsync(new Exception());

            //Act
            Func<Task> result = async () => await _getCountries.GetAllAsync();

            //Assert
            await result.Should().ThrowAsync<Exception>();

        }


        [Fact]
        public async Task GetAllExpr_DbErorr_ThrowException()
        {
            //Arrange
            Expression<Func<Country, bool>> expression = country => country.Id > 45;

            _getAllRepositoryMock.Setup(temp => temp.GetAllAsync(expression)).ThrowsAsync(new Exception());

            //Act
            Func<Task> action = async () => await _getCountries.GetAllAsync(expression);

            //Assert
            await action.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task GetAllExpr_MatchExpression_ReturnQueryablefMatchedCountries()
        {
            //Arrange
            List<Country> countries = new() {
                _fixture.Build<Country>().With( c => c.Id, 10).With(c=>c.Users, null as List<User>).Create(),
                _fixture.Build<Country>().With( c => c.Id, 50).With(c=>c.Users, null as List<User>).Create(),
                _fixture.Build<Country>().With( c => c.Id, 90).With(c=>c.Users, null as List<User>).Create(),
                _fixture.Build<Country>().With( c => c.Id, 80).With(c=>c.Users, null as List<User>).Create(),
                _fixture.Build<Country>().With( c => c.Id, 16).With(c=>c.Users, null as List<User>).Create(),
            };


            Expression<Func<Country, bool>> expression = country => country.Id > 45;

            _mapper.Setup(m => m.Map<CountryDTO>(It.IsAny<Country>()))
                .Returns((Country countrySource) => new CountryDTO() { Id = countrySource.Id, CountryName = countrySource.CountryName });

            IQueryable<Country> SelectedCountries = countries.AsQueryable().Where(expression);

            IQueryable<CountryDTO> Expected = countries.AsQueryable()
                                                        .Where(expression)
                                                        .Select(c => new CountryDTO { Id = c.Id, CountryName = c.CountryName });

            _getAllRepositoryMock.Setup(temp => temp.GetAllAsync(expression)).ReturnsAsync(SelectedCountries);

            //Act
            IQueryable<CountryDTO> res = await _getCountries.GetAllAsync(expression);


            //Assert
            res.Should().BeEquivalentTo(Expected);
        }

        [Fact]
        public async Task GetAllExpr_DoesnotMatchExpression_ReturnEmptyQueryableCountries()
        {
            //Arrange
            List<Country> countries = new() {
                _fixture.Build<Country>().With( c => c.Id, 10).With(c=>c.Users, null as List<User>).Create(),
                _fixture.Build<Country>().With( c => c.Id, 50).With(c=>c.Users, null as List<User>).Create(),
                _fixture.Build<Country>().With( c => c.Id, 90).With(c=>c.Users, null as List<User>).Create(),
                _fixture.Build<Country>().With( c => c.Id, 80).With(c=>c.Users, null as List<User>).Create(),
                _fixture.Build<Country>().With( c => c.Id, 16).With(c=>c.Users, null as List<User>).Create(),
            };


            Expression<Func<Country, bool>> expression = country => country.Id > 145;

            IQueryable<Country> SelectedCountries = countries.AsQueryable().Where(expression);

            _mapper.Setup(m => m.Map<CountryDTO>(It.IsAny<Country>()))
                .Returns((Country countrySource) => new CountryDTO() { Id = countrySource.Id, CountryName = countrySource.CountryName });

            _getAllRepositoryMock.Setup(temp => temp.GetAllAsync(expression)).ReturnsAsync(SelectedCountries);

            //Act
            IQueryable<CountryDTO> res = await _getCountries.GetAllAsync(expression);

            //Assert
            res.Should().BeEmpty();
        }
    }
}
