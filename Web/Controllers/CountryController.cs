using IServices.ICountryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelDTO.API;
using ModelDTO.CountryDTOs;
using Models.Countries;
using IServices.ICountryServices;
using System.Net;

namespace Web.Controllers;

[Route("Country")]
[ApiController]
public class CountryController : ControllerBase
{
    private readonly ICreateCountry _createCountry;
    private readonly IGetCountry _getCountry;
    private readonly IGetAllCountries _getAllCountries;
    private readonly ILogger<Country> _logger;


    public CountryController(ICreateCountry createCountry,
        IGetCountry getCountry,
        IGetAllCountries getAllCountries, ILogger<Country> logger)
    {
        _createCountry = createCountry;
        _getCountry = getCountry;
        _getAllCountries = getAllCountries;
        _logger = logger;
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


    [HttpGet("{Id:int}")]
    public async Task<IActionResult> GetCountry([FromRoute] int Id)

    {
        var c = await _getCountry.GetByAsync(c => c.CountryId == Id);

        if (c == null)
            return NotFound();
        return Ok();
    }


    [HttpGet("AllCounties")]
    public async Task<IActionResult> GetAllCountries()
    {
        _logger.LogInformation("------------------------------Started processing request to fetch all countries.");
        var c = await _getAllCountries.GetAllAsync();

        if (c == null)
        {
            _logger.LogWarning("----------------------------------No Countries are Found.");
            return NotFound();
        }


        var response = new ApiResponse()
        {
            ErrorMessages = null,
            IsSuccess = true,
            StatusCode = HttpStatusCode.OK,
            Result = c
        };


        _logger.LogInformation("-----------------------------All Countries Fetched.");

        return Ok(response);
    }
}