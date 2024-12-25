using AutoMapper;
using IRepository;
using IServices.ICountryServices; 
using ModelDTO.CountryDTOs;
using Models.Countries; 

namespace Services.CountryServices;

public class CreateCountryService : ICreateCountry
{
    public CreateCountryService(ICreateRepository<Country> repository, IGetCountry getCountry,
        IMapper mapper)
    {
        _createCountry = repository;
        _getCountry = getCountry;
        _mapper = mapper;
    }

    ICreateRepository<Country> _createCountry;
    IGetCountry _getCountry;
    IMapper _mapper;

    public async Task<CountryDTO> CreateAsync(CreateCountryRequest entity)
    {
        if (entity == null)
            throw new ArgumentNullException($" {typeof(CreateCountryRequest)} is Null");

        if (string.IsNullOrEmpty(entity.Name))
            throw new ArgumentException($"CountryDTOs Name cannot by null.");


        entity.Name = entity.Name?.Trim().ToLower();


        var country = await _getCountry.GetByAsync(c => c.CountryName == entity.Name);

        if (country != null)
            throw new InvalidOperationException("CountryDTOs is already exist, cant duplicate country.");


        var newCountry = await _createCountry.CreateAsync(_mapper.Map<Country>(entity));

        var createdCountryDTO = _mapper.Map<CountryDTO>(newCountry);

        return createdCountryDTO;
    }
}