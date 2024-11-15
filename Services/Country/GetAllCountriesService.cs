using System.Linq.Expressions;
using AutoMapper;
using IRepository;
using IServices.Country;
using ModelDTO;
using Models.Types;

namespace Services.CountryServices;

public class GetAllCountriesService : IGetAllCountries

{
    private readonly IGetAllRepository<Country> _getCountries;
    private IMapper _mapper;


    public GetAllCountriesService(IGetAllRepository<Country> getCountries, IMapper mapper)
    {
        _getCountries = getCountries;
        _mapper = mapper;
    }


    public async Task<List<CountryDTO>> GetAllAsync()
    {
        List<Country> countries = await _getCountries.GetAllAsync();

        return countries
            .Select(country => _mapper.Map<CountryDTO>(country))
            .ToList();
    }


    public async Task<IQueryable<CountryDTO>> GetAllAsync(Expression<Func<Country, bool>> predicate)
    {
        var countries = await _getCountries.GetAllAsync(predicate);
        var queryCountries = countries.Select(country => _mapper.Map<CountryDTO>(country));
        return queryCountries;
    }
}