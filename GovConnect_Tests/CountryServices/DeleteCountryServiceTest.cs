using AutoFixture;
using AutoMapper;
using DataConfigurations;
using FluentAssertions;
using IServices.ICountryServices;
using Models.Users;
using Moq;
using Services.CountryServices;
using System.Linq.Expressions;
using Models.Countries;
using Models.LicenseModels;
using Web.Mapper;
using IRepository.IGenericRepositories;
namespace GovConnect_Tests.CountryServices
{
    public class DeleteCountrySeviceTest
    {
        private readonly IFixture _fixture;

        private readonly IDeleteCountry _deleteCountry;
        private readonly IDeleteRepository<Country> _deleteCountryRepository;
        private readonly Mock<IDeleteRepository<Country>> _deleteRepositoryMock;
        private readonly Mock<IGetRepository<Country>> _getRepositoryMock;

        //private readonly IMapper _mapper;
        public DeleteCountrySeviceTest()
        {
            _fixture = new Fixture();

            //var mapperCfg = new MapperConfiguration(cfg => cfg.AddProfile(typeof(GovConnectMapperConfig)));

            _deleteRepositoryMock = new Mock<IDeleteRepository<Country>>();

            _getRepositoryMock = new Mock<IGetRepository<Country>>();

            _deleteCountry = new DeleteCountryService(_deleteRepositoryMock.Object);
        }


        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowInvalidOperationException()
        {
            _getRepositoryMock.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<Country, bool>>>())).ReturnsAsync(null as Country);


            Func<Task> res = async () => await _deleteCountry.DeleteAsync(0);

            await res.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task DeleteAsync_ConutryDoesNotExist_ReturnFalse()
        {
            _getRepositoryMock.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<Country, bool>>>())).ReturnsAsync(null as Country);


            var res = await _deleteCountry.DeleteAsync(2);


            res.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteAsync_ConutryDoesExist_ReturnTrue()
        {
            Country country = _fixture.Build<Country>()
                .With(c => c.Users, null as List<User>) 
                .With(c => c.localDrivingLicenses, null as List<LocalDrivingLicense>)
                .Create();

            _getRepositoryMock.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<Country, bool>>>())).ReturnsAsync(country);

            _deleteRepositoryMock.Setup(temp => temp.DeleteAsync(It.IsAny<Expression<Func<Country, bool>>>())).ReturnsAsync(1);

            var res = await _deleteCountry.DeleteAsync(1);


            res.Should().BeTrue();
        }



    }
}
