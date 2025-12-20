using AutoMapper;
using IServices.IApplicationServices.IPurpose;
using Microsoft.AspNetCore.Mvc;
using ModelDTO.API;
using ModelDTO.ApplicationDTOs.Purpose;
using System.Net;

namespace Web.Controllers.Users;

[ApiController]
[Route("api/ServicePurpose")]
public class ServicePurposeController : ControllerBase
{
    private IGetServicePurpose _getServicePurpose;
    private IGetAllServicePurpose _getAllServicePurpose;
    private IMapper _mapper;

    private ILogger<ServicePurposeController> _logger;

    private ApiResponse _apiResponse;

    public ServicePurposeController(IGetServicePurpose getServicePurpose,
        IGetAllServicePurpose getAllServicePurpose,
        ILogger<ServicePurposeController> logger, IMapper mapper)
    {
        _getServicePurpose = getServicePurpose;
        _getAllServicePurpose = getAllServicePurpose;
        _mapper = mapper;
        _logger = logger;
    }


    [HttpGet("Get")]
    public async Task<IActionResult> GetServicePurpose([FromQuery] string ServicePurposeName)
    {
        _logger.LogInformation($"{this.GetType().Name} -- {nameof(this.GetServicePurpose)}");

        try
        {
            var servicePurpose = await _getServicePurpose.GetByAsync(x => x.Purpose.ToLower() == ServicePurposeName.ToLower());

            if (servicePurpose is null)
            {
                _apiResponse = new ApiResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = true,
                    Result = null,

                };
                _apiResponse.ErrorMessages.Add("There's no service Purpose found with this name.");
                return NotFound(_apiResponse);
            }

            ServicePurposeDTO servicePurposeDTO = _mapper.Map<ServicePurposeDTO>(servicePurpose);

            _apiResponse = new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = servicePurposeDTO,
                ErrorMessages = null,

            };
        }
        catch (Exception ex)
        {

            _logger.LogWarning($"Failer In {this.GetType().Name} -- {nameof(this.GetServicePurpose)}");

            _apiResponse = new ApiResponse
            {
                StatusCode = HttpStatusCode.ExpectationFailed,
                IsSuccess = false,
                Result = null,
            };

            _apiResponse.ErrorMessages.Add(ex.Message);
            return BadRequest(_apiResponse);
        }

        return Ok(_apiResponse);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllServicePurpose()
    {
        _logger.LogInformation($"{this.GetType().Name} -- {nameof(this.GetAllServicePurpose)}");

        try
        {
            var serviceCategories = await _getAllServicePurpose.GetAllAsync();

            if (serviceCategories is null || serviceCategories.Count < 1)
                return NoContent();


            List<ServicePurposeDTO> servicePurposeDTOs = _mapper.Map<List<ServicePurposeDTO>>(serviceCategories);

            _apiResponse = new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = servicePurposeDTOs,
                ErrorMessages = null,

            };
        }
        catch (Exception ex)
        {

            _logger.LogWarning($"Failer In {this.GetType().Name} -- {nameof(this.GetAllServicePurpose)}");

            _apiResponse = new ApiResponse
            {
                StatusCode = HttpStatusCode.ExpectationFailed,
                IsSuccess = false,
                Result = null,
            };

            _apiResponse.ErrorMessages.Add(ex.Message);
            return BadRequest(_apiResponse);
        }

        return Ok(_apiResponse);
    }
}
