using AutoMapper;
using IRepository.IGenericRepositories;
using IServices.ICountryServices;
using Models.Countries;

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