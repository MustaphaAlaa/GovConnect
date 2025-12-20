 
using ModelDTO.CountryDTOs;

namespace IServices.ICountryServices;
public interface ICreateCountry : ICreateService<CreateCountryRequest, CountryDTO>
{

}