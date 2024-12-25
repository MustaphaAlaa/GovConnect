 
using ModelDTO.CountryDTOs;

namespace IServices.ICountryServices;
public interface IUpdateCountry : IUpdateService<UpdateCountryRequest,CountryDTO>
{
    
}