using IServices.IApplicationServices.IPurpose;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelDTO.API;
using ModelDTO.ApplicationDTOs.Purpose;
using Models;
using System.Net;

namespace Web.Controllers.Stuff;

[ApiController]
[Route("api/stuff/servicesPurpose")]
public class StuffServicePurposeController : ControllerBase
{

    private readonly ICreateServicePurpose _createServicePurpose;
    private readonly IUpdateServicePurpose _updateServicePurpose;
    private readonly IDeleteServicePurpose _deleteServicePurpose;

    private readonly ILogger<StuffServicePurposeController> _logger;

    private ApiResponse _apiResponse;

    public StuffServicePurposeController(ICreateServicePurpose createServicePurpose,
        IUpdateServicePurpose updateServicePurpose,
        IDeleteServicePurpose deleteServicePurpose,
        ILogger<StuffServicePurposeController> logger,
        ApiResponse apiResponse)
    {
        _createServicePurpose = createServicePurpose;
        _updateServicePurpose = updateServicePurpose;
        _deleteServicePurpose = deleteServicePurpose;
        _logger = logger;
        _apiResponse = apiResponse;
    }

    [HttpPost]
    [Authorize(Policy = "JWT", Roles = nameof(EnUserRoles.Admin))]
    public async Task<IActionResult> CreateServicePurpose(CreateServicePurposeRequest request)
    {
        _logger.LogInformation($"{this.GetType().Name} -- {nameof(this.CreateServicePurpose)}");
        try
        {
            var servicePurpose = await _createServicePurpose.CreateAsync(request);
            _apiResponse = new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = servicePurpose,
                ErrorMessages = null,

            };
        }
        catch (Exception ex)
        {

            _logger.LogWarning($"Failer In {this.GetType().Name} -- {nameof(this.CreateServicePurpose)}");

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
    public async Task<IActionResult> UpdateServicePurpose(ServicePurposeDTO updateRequest)
    {
        _logger.LogInformation($"{this.GetType().Name} -- {nameof(this.UpdateServicePurpose)}");

        try
        {
            var servicePurpose = await _updateServicePurpose.UpdateAsync(updateRequest);

            _apiResponse = new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = servicePurpose != null,
                Result = servicePurpose,
                ErrorMessages = null,

            };
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Failer In {this.GetType().Name} -- {nameof(this.UpdateServicePurpose)}");

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
    public async Task<IActionResult> DeleteServicePurpose(int SerivcePurposeId)
    {
        try
        {
            var isDeleted = await _deleteServicePurpose.DeleteAsync(SerivcePurposeId);

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
