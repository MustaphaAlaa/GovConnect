using AutoMapper;
using IRepository;
using IServices.Country;
using ModelDTO;
using Models.Types;

namespace Services.CountryServices;

public class DeleteCountryService : IDeleteCountry
{
    public DeleteCountryService(IDeleteRepository<Country> deleteRepository)
    {
        _deleteRepository = deleteRepository;

    }

    private readonly IDeleteRepository<Country> _deleteRepository;


    public async Task<bool> DeleteAsync(int id)
    {
        if (id <= 0)
            throw new InvalidOperationException($"Invalid InternationalDrivingLicenseId.");

        //I didn't write get country because DeleteAsync will check if country exist or not

        return await _deleteRepository.DeleteAsync(country => country.CountryId == id) > 0;
    }
}