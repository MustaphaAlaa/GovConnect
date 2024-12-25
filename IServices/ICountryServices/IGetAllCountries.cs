using System.Linq.Expressions;
using ModelDTO.CountryDTOs;
using Models.Countries;

namespace IServices.ICountryServices;

public interface IGetAllCountries : IGetAllService<Models.Countries.Country, CountryDTO>
{
}