using IServices.Country;
using IServices.ICountryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelDTO.CountryDTOs;

namespace Web.Controllers;
 
[Route("CountryDTOs")]
[ApiController]
public class CountryController : ControllerBase
{
    private readonly ICreateCountry _createCountry;
    private readonly IGetCountry _getCountry;
    private readonly IGetAllCountries _getAllCountries;

   

    public CountryController(ICreateCountry createCountry, IGetCountry getCountry, IGetAllCountries getAllCountries)
    {
        _createCountry = createCountry;
        _getCountry = getCountry;
        _getAllCountries = getAllCountries;
    }

    [Authorize(Roles = "admin")]
    [HttpPost("add")]
    public async Task<IActionResult> AddCountry(CreateCountryRequest request)
    {
        var c = await _createCountry.CreateAsync(request);

        if (c is null)
            return BadRequest();

        return Ok();
    }

    [HttpGet("{InternationalDrivingLicenseId:int}")]
    public async Task<IActionResult> GetCountry([FromRoute] int Id)

    {
        var c = await _getCountry.GetByAsync(c => c.Id == Id);

        if (c == null)
            return NotFound(); 
        return Ok();
    }
}