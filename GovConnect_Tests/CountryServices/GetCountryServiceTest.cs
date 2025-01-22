using AutoFixture;
using AutoMapper;
using DataConfigurations;
using FluentAssertions;
using IRepository.IGenericRepositories;
using IServices.ICountryServices;
using Models.Countries;
using Moq;
using Services.CountryServices;
using System.Linq.Expressions;
using Web.Mapper;

namespace GovConnect_Tests.CountryServices;

public class GetCountryServiceTest
{
    private readonly IGetCountry _getCountry;
    private readonly Mock<IGetRepository<Country>> _getRepositoryMock;
    private readonly IMapper _mapper;

    public GetCountryServiceTest()
    {
        var mapperCfg = new MapperConfiguration(cfg => cfg.AddProfile(typeof(GovConnectMapperConfig)));
        _mapper = new Mapper(mapperCfg);

        _getRepositoryMock = new Mock<IGetRepository<Country>>();
        _getCountry = new GetCountryService(_getRepositoryMock.Object, _mapper);
    }

    [Fact]
    public async Task GetCountryAsync_CountryDoesNotExist_ReturnsNull()
    {
        // Arrange
        Expression<Func<Country?, bool>> predicate = c => c.CountryId == 0;
        _getRepositoryMock.Setup(temp => temp.GetAsync(predicate)).ReturnsAsync(null as Country);

        // Act
        var result = await _getCountry.GetByAsync(predicate);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCountryAsync_CountryExists_ReturnsCountryObject()
    {
        // Arrange
        Country country = new Country() { CountryName = "Russia", CountryId = 5555 };
        Expression<Func<Country?, bool>> predicate = c => c.CountryId == country.CountryId;
        _getRepositoryMock.Setup(temp => temp.GetAsync(predicate)).ReturnsAsync(country);

        // Act
        var result = await _getCountry.GetByAsync(predicate);

        // Assert
        result.Should().BeEquivalentTo(country);
    }

    [Fact]
    public async Task GetCountryAsync_ThrowsException()
    {
        // Arrange
        Country country = new Country() { CountryName = "Russia", CountryId = 5555 };
        Expression<Func<Country?, bool>> predicate = c => c.CountryId == country.CountryId;
        _getRepositoryMock.Setup(temp => temp.GetAsync(predicate)).ThrowsAsync(new Exception());

        // Act
        Func<Task> result = async () => await _getCountry.GetByAsync(predicate);

        // Assert
        await result.Should().ThrowAsync<Exception>();
    }
}
