using ModelDTO;

namespace IServices.Country;

public interface ICreateCountry : ICreateService<CreateCountryRequest, CountryDTO>
{

}