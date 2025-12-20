using IServices.IApplicationServices.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelDTO.API;
using ModelDTO.ApplicationDTOs.Category;
using Models;
using System.Net;

namespace Web.Controllers.Stuff;

[ApiController]
[Route("api/stuff/servicesCategory")]
public class StuffServiceCategoryController : ControllerBase
{

    private readonly ICreateServiceCategory _createServiceCategory;
    private readonly IUpdateServiceCategory _updateServiceCategory;
    private readonly IDeleteServiceCategory _deleteServiceCategory;

    private readonly ILogger<StuffServiceCategoryController> _logger;

    private ApiResponse _apiResponse;

    public StuffServiceCategoryController(ICreateServiceCategory createServiceCategory,
        IUpdateServiceCategory updateServiceCategory,
        IDeleteServiceCategory deleteServiceCategory,
        ILogger<StuffServiceCategoryController> logger,
        ApiResponse apiResponse)
    {
        _createServiceCategory = createServiceCategory;
        _updateServiceCategory = updateServiceCategory;
        _deleteServiceCategory = deleteServiceCategory;
        _logger = logger;
        _apiResponse = apiResponse;
    }

    [HttpPost]
    [Authorize(Policy = "JWT", Roles = nameof(EnUserRoles.Admin))]
    public async Task<IActionResult> CreateServiceCategory(CreateServiceCategoryRequest request)
    {
        _logger.LogInformation($"{this.GetType().Name} -- {nameof(this.CreateServiceCategory)}");
        try
        {
            var serviceCategory = await _createServiceCategory.CreateAsync(request);
            _apiResponse = new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = serviceCategory,
                ErrorMessages = null,

            };
        }
        catch (Exception ex)
        {

            _logger.LogWarning($"Failer In {this.GetType().Name} -- {nameof(this.CreateServiceCategory)}");

            _apiResponse = new ApiResponse
            {
                StatusCode = HttpStatusCode.ExpectationFailed,
                IsSuccess = false,
                Result = null,
            };

            _apiResponse.ErrorMessages.Add(ex.Message);
        }

        return Ok(_apiResponse);
    }

    [HttpPut]
    [Authorize(Policy = "JWT", Roles = nameof(EnUserRoles.Admin))]
    public async Task<IActionResult> UpdateServiceCategory(ServiceCategoryDTO updateRequest)
    {
        _logger.LogInformation($"{this.GetType().Name} -- {nameof(this.UpdateServiceCategory)}");

        try
        {
            var serviceCategory = await _updateServiceCategory.UpdateAsync(updateRequest);

            _apiResponse = new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = serviceCategory != null,
                Result = serviceCategory,
                ErrorMessages = null,

            };
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Failer In {this.GetType().Name} -- {nameof(this.UpdateServiceCategory)}");

            _apiResponse = new ApiResponse
            {
                StatusCode = HttpStatusCode.ExpectationFailed,
                IsSuccess = false,
                Result = null,
            };

            _apiResponse.ErrorMessages.Add(ex.Message);
        }

        return Ok(_apiResponse);
    }

    [HttpDelete]
    [Authorize(Policy = "JWT", Roles = nameof(EnUserRoles.Admin))]
    public async Task<IActionResult> DeleteServiceCategory(int SerivceCategoryId)
    {
        try
        {
            var isDeleted = await _deleteServiceCategory.DeleteAsync(SerivceCategoryId);

            _apiResponse = new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = isDeleted,
                Result = isDeleted ? "deleted" : "Something wrong happend",
                ErrorMessages = null,

            };
        }
        catch (Exception ex)
        {
            _apiResponse = new ApiResponse
            {
                StatusCode = HttpStatusCode.ExpectationFailed,
                IsSuccess = false,
                Result = null,
            };

            _apiResponse.ErrorMessages.Add(ex.Message);
        }

        return Ok(_apiResponse);
    }



}
