using System.Linq.Expressions;
using ModelDTO;
using Models.Types;
namespace IServices.Country;

public   interface IGetAllCountries : IGetAllService< CountryDTO,Models.Types.Country>
{
     
}