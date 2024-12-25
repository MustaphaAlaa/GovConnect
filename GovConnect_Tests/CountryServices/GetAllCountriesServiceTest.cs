using AutoFixture;
using AutoMapper;
using DataConfigurations;
using FluentAssertions;
using IRepository; 
using IServices.ICountryServices; 
using ModelDTO.CountryDTOs;
using Models.Countries;
using Models.Users;
using Moq; 
using Services.CountryServices; 
using System.Linq.Expressions;
using Models.LicenseModels;

namespace GovConnect_Tests.CountryServices
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

            var dbContextMock = new Mock<DataConfigurations.GovConnectDbContext>();

            _getAllRepositoryMock = new Mock<IGetAllRepository<Country>>();

            _getCountries = new GetAllCountriesService(_getAllRepositoryMock.Object, _mapper.Object);

        }


        [Fact]
        public async Task GetAll_DbHasData_ReturnListData()
        {
            //Arrange
            
            List<Country> countries = new(){
                
                 _fixture.Build<Country>()
                .With(c => c.Users, null as List<User>)
                .With(c => c.Users, null as List<User>)
                .With(c => c.localDrivingLicenses, null as List<LocalDrivingLicense>)
                .Create(), _fixture.Build<Country>()
                .With(c => c.Users, null as List<User>)
                .With(c => c.Users, null as List<User>)
                .With(c => c.localDrivingLicenses, null as List<LocalDrivingLicense>)
                .Create(), _fixture.Build<Country>()
                .With(c => c.Users, null as List<User>)
                .With(c => c.Users, null as List<User>)
                .With(c => c.localDrivingLicenses, null as List<LocalDrivingLicense>)
                .Create(), _fixture.Build<Country>()
                .With(c => c.Users, null as List<User>)
                .With(c => c.Users, null as List<User>)
                .With(c => c.localDrivingLicenses, null as List<LocalDrivingLicense>)
                .Create(),
                
 
            };

            
            
            List<CountryDTO> countryDTOs = countries
                              .Select(c => new CountryDTO { CountryCode = c.CountryCode, CountryName = c.CountryName })
                              .ToList();

            _getAllRepositoryMock.Setup(temp => temp.GetAllAsync()).ReturnsAsync(countries);

            _mapper.Setup(temp => temp.Map<CountryDTO>(It.IsAny<Country>()))
                   .Returns((Country source) => new CountryDTO
                   {
                       CountryName = source.CountryName,
                       CountryCode = source.CountryCode
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
            Expression<Func<Country, bool>> expression = country => country.CountryId > 45;

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
            List<Country> countries = new(){
                
                _fixture.Build<Country>()
                    .With(c => c.Users, null as List<User>) 
                    .With(c => c.localDrivingLicenses, null as List<LocalDrivingLicense>)
                    .Create(), _fixture.Build<Country>()
                    .With(c => c.Users, null as List<User>) 
                    .With(c => c.localDrivingLicenses, null as List<LocalDrivingLicense>)
                    .Create(), _fixture.Build<Country>()
                    .With(c => c.Users, null as List<User>) 
                    .With(c => c.localDrivingLicenses, null as List<LocalDrivingLicense>)
                    .Create(), _fixture.Build<Country>()
                    .With(c => c.Users, null as List<User>) 
                    .With(c => c.localDrivingLicenses, null as List<LocalDrivingLicense>)
                    .Create() 
            };
 

            Expression<Func<Country, bool>> expression = country => country.CountryId > 45;

            _mapper.Setup(m => m.Map<CountryDTO>(It.IsAny<Country>()))
                .Returns((Country countrySource) => new CountryDTO() { CountryCode = countrySource.CountryCode, CountryName = countrySource.CountryName });

            IQueryable<Country> SelectedCountries = countries.AsQueryable().Where(expression);

            IQueryable<CountryDTO> Expected = countries.AsQueryable()
                                                        .Where(expression)
                                                        .Select(c => new CountryDTO { CountryCode = c.CountryCode, CountryName = c.CountryName });

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
                   
            List<Country> countries = new(){
                
                
                
 
            }; 
            
            Expression<Func<Country, bool>> expression = country => country.CountryId > 145;

            IQueryable<Country> SelectedCountries = countries.AsQueryable().Where(expression);

            _mapper.Setup(m => m.Map<CountryDTO>(It.IsAny<Country>()))
                .Returns((Country countrySource) => new CountryDTO() { CountryCode = countrySource.CountryCode, CountryName = countrySource.CountryName });

            _getAllRepositoryMock.Setup(temp => temp.GetAllAsync(expression)).ReturnsAsync(SelectedCountries);

            //Act
            IQueryable<CountryDTO> res = await _getCountries.GetAllAsync(expression);

            //Assert
            res.Should().BeEmpty();
        }
    }
}
