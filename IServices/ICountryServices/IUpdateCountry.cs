 
using ModelDTO.CountryDTOs;

namespace IServices.Country;

public interface IUpdateCountry : IUpdateService<UpdateCountryRequest,CountryDTO>
{
    
}