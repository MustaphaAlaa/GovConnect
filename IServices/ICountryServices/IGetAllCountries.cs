using System.Linq.Expressions;
 
using ModelDTO.CountryDTOs;
 
namespace IServices.Country;

public   interface IGetAllCountries : IGetAllService< Models.Types.Country,CountryDTO  >
{
     
}