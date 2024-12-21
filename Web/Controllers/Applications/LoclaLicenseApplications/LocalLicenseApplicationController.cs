using System.Net;
using IServices.IApplicationServices.IServiceCategoryApplications.ILocalDrivingLicenseApplication;
using Microsoft.AspNetCore.Mvc;
using ModelDTO.API;
using ModelDTO.ApplicationDTOs.User;

namespace Web.Controllers.Applications.LocalLicenseApplications;

[ApiController]
[Route("Applications/LocalLicenseApplications")]
public class LocalLicenseApplicationController : ControllerBase
{

    private readonly ICreateLocalDrivingLicenseApplicationService _createLocalDrivingLicenseApplicationService;
    private readonly INewLocalDrivingLicenseApplicationValidator _newLocalDrivingLicenseApplicationValidator;

    public LocalLicenseApplicationController(ICreateLocalDrivingLicenseApplicationService createLocalDrivingLicenseApplicationService
    , INewLocalDrivingLicenseApplicationValidator newLocalDrivingLicenseApplicationValidator)
    {
        _createLocalDrivingLicenseApplicationService = createLocalDrivingLicenseApplicationService;
        _newLocalDrivingLicenseApplicationValidator = newLocalDrivingLicenseApplicationValidator;
    }


    [HttpPost("new")]
    public async Task<ActionResult> New(CreateLocalDrivingLicenseApplicationRequest request)
    {

        request.UserId = Guid.NewGuid();

        var ldlApp = await _createLocalDrivingLicenseApplicationService.Create(request, _newLocalDrivingLicenseApplicationValidator);

        var apiResponse = new ApiResponse();
        apiResponse.ErrorMessages = null;
        apiResponse.IsSuccess = true;
        apiResponse.statusCode = HttpStatusCode.Created;
        apiResponse.Result = ldlApp;

        return Ok(apiResponse);
    }

    [HttpPost("renew")]
    public async Task<ActionResult> Renew(CreateLocalDrivingLicenseApplicationRequest request)
    {
        throw new NotImplementedException();
    }


    [HttpPost("release")]
    public async Task<ActionResult> Release(CreateLocalDrivingLicenseApplicationRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpPost("replacement/lost")]
    public async Task<ActionResult> ReplacementForLost(CreateLocalDrivingLicenseApplicationRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpPost("replacement/damage")]
    public async Task<ActionResult> ReplacementForDamage(CreateLocalDrivingLicenseApplicationRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpPost("retakeTest")]
    public async Task<ActionResult> RetakeTheTest(CreateLocalDrivingLicenseApplicationRequest request)
    {
        throw new NotImplementedException();
    }

}