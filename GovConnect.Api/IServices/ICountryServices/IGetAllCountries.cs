using System.Linq.Expressions;
using ModelDTO.CountryDTOs;
using Models.Countries;

namespace IServices.ICountryServices;

public interface IGetAllCountries : IAsyncAllRecordsRetrieverService<Models.Countries.Country, CountryDTO>
{
}