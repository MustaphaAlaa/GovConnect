using System.Linq.Expressions;
using AutoMapper;
using IRepository;
using IServices.Country;
using ModelDTO;
using Models.Types;

namespace Services.CountryServices;

public class UpdateCountryService : IUpdateCountry
{
    public UpdateCountryService(IUpdateRepository<Country> updateRepository,
        IGetRepository<Country> getRepository,
        IMapper mapper)
    {
        _updateRepository = updateRepository;
        _getRepository = getRepository;
        _mapper = mapper;
    }

    private readonly IUpdateRepository<Country> _updateRepository;
    private readonly IGetRepository<Country> _getRepository;
    private readonly IMapper _mapper;





    public async Task<CountryDTO> UpdateAsync(UpdateCountryRequest request)
    {
        if (request is null)
            throw new ArgumentNullException($" {nameof(request)} is null");

        if (String.Empty == request.Name)
            throw new ArgumentException("Cannot assign empty name");

        if (String.IsNullOrEmpty(request.Name))
            throw new ArgumentException("Cannot assign null value.");



        if (request.Id <= 0)
            throw new InvalidOperationException("Id Is out of boundry");


        var country = await _getRepository.GetAsync(country => country.Id == request.Id);

        if (country is null)
            throw new InvalidOperationException("Country isn't exist in db");

        country.CountryName = request.Name;
        await _updateRepository.UpdateAsync(country);

        var result = _mapper.Map<CountryDTO>(country);

        return result;
    }
}