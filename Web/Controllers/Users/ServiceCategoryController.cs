using AutoMapper;
using IServices.IApplicationServices.Category;
using Microsoft.AspNetCore.Mvc;
using ModelDTO.API;
using ModelDTO.ApplicationDTOs.Category;
using System.Net;

namespace Web.Controllers.Users;

[ApiController]
[Route("api/ServiceCategory")]
public class ServiceCategoryController : ControllerBase
{
    private IGetServiceCategory _getServiceCategory;
    private IGetAllServiceCategory _getAllServiceCategory;
    private IMapper _mapper;

    private ILogger<ServiceCategoryController> _logger;

    private ApiResponse _apiResponse;

    public ServiceCategoryController(IGetServiceCategory getServiceCategory,
        IGetAllServiceCategory getAllServiceCategory,
        ILogger<ServiceCategoryController> logger, IMapper mapper)
    {
        _getServiceCategory = getServiceCategory;
        _getAllServiceCategory = getAllServiceCategory;
        _mapper = mapper;
        _logger = logger;
    }


    [HttpGet("Get")]
    public async Task<IActionResult> GetServiceCategory([FromQuery] string ServiceCategoryName)
    {
        _logger.LogInformation($"{this.GetType().Name} -- {nameof(this.GetServiceCategory)}");

        try
        {
            var serviceCategory = await _getServiceCategory.GetByAsync(x => x.Category.ToLower() == ServiceCategoryName.ToLower());

            if (serviceCategory is null)
            {
                _apiResponse = new ApiResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = true,
                    Result = null,

                };
                _apiResponse.ErrorMessages.Add("There's no service category found with this name.");
                return NotFound(_apiResponse);
            }

            ServiceCategoryDTO serviceCategoryDTO = _mapper.Map<ServiceCategoryDTO>(serviceCategory);

            _apiResponse = new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = serviceCategoryDTO,
                ErrorMessages = null,

            };
        }
        catch (Exception ex)
        {

            _logger.LogWarning($"Failer In {this.GetType().Name} -- {nameof(this.GetServiceCategory)}");

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
    public async Task<IActionResult> GetAllServiceCategory()
    {
        _logger.LogInformation($"{this.GetType().Name} -- {nameof(this.GetAllServiceCategory)}");

        try
        {
            var serviceCategories = await _getAllServiceCategory.GetAllAsync();

            if (serviceCategories is null || serviceCategories.Count < 1)
                return NoContent();


            List<ServiceCategoryDTO> serviceCategoryDTOs = _mapper.Map<List<ServiceCategoryDTO>>(serviceCategories);

            _apiResponse = new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = serviceCategoryDTOs,
                ErrorMessages = null,

            };
        }
        catch (Exception ex)
        {

            _logger.LogWarning($"Failer In {this.GetType().Name} -- {nameof(this.GetAllServiceCategory)}");

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
