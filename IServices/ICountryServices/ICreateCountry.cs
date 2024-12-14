 
using ModelDTO.CountryDTOs;

namespace IServices.Country;

public interface ICreateCountry : ICreateService<CreateCountryRequest, CountryDTO>
{

}