using System.Net;
using IServices.IApplicationServices.IServiceCategoryApplications.ILocalDrivingLicenseApplication;
 using IServices.IValidtors.ILocalDrivingLicenseApplications;
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
    private readonly IRenewLocalDrivingLicenseApplicationValidator _renewLocalDrivingLicenseApplicationValidator;
    private readonly IReleaseLocalDrivingLicenseApplicationValidator _releaseLocalDrivingLicenseApplicationValidator;
    private readonly IReplacementForLostLocalDrivingLicenseApplicationValidator _replacementForLostLocalDrivingLicenseApplicationValidator;
    private readonly IReplacementForDamageLocalDrivingLicenseApplicationValidator _replacementForDamageLocalDrivingLicenseApplicationValidator;

    public LocalLicenseApplicationController(ICreateLocalDrivingLicenseApplicationService createLocalDrivingLicenseApplicationService,
        INewLocalDrivingLicenseApplicationValidator newLocalDrivingLicenseApplicationValidator,
        IRenewLocalDrivingLicenseApplicationValidator renewLocalDrivingLicenseApplicationValidator,
        IReleaseLocalDrivingLicenseApplicationValidator releaseLocalDrivingLicenseApplicationValidator,
        IReplacementForLostLocalDrivingLicenseApplicationValidator replacementForLostLocalDrivingLicenseApplicationValidator,
        IReplacementForDamageLocalDrivingLicenseApplicationValidator replacementForDamageLocalDrivingLicenseApplicationValidator)
    {
        _createLocalDrivingLicenseApplicationService = createLocalDrivingLicenseApplicationService;
        _newLocalDrivingLicenseApplicationValidator = newLocalDrivingLicenseApplicationValidator;
        _renewLocalDrivingLicenseApplicationValidator = renewLocalDrivingLicenseApplicationValidator;
        _releaseLocalDrivingLicenseApplicationValidator = releaseLocalDrivingLicenseApplicationValidator;
        _replacementForLostLocalDrivingLicenseApplicationValidator = replacementForLostLocalDrivingLicenseApplicationValidator;
        _replacementForDamageLocalDrivingLicenseApplicationValidator = replacementForDamageLocalDrivingLicenseApplicationValidator;
    }




    [HttpPost("new")]
    public async Task<ActionResult> New(CreateLocalDrivingLicenseApplicationRequest request)
    {

        //request.UserId = Guid.NewGuid();
        request.UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"); //for testing purpose
        request.ApplicationId = 0;
        var ldlApp = await _createLocalDrivingLicenseApplicationService.Create(request, _newLocalDrivingLicenseApplicationValidator);

        var apiResponse = new ApiResponse();
        apiResponse.ErrorMessages = null;
        apiResponse.IsSuccess = true;
        apiResponse.StatusCode = HttpStatusCode.Created;
        apiResponse.Result = ldlApp;

        return Ok(apiResponse);
    }

    [HttpPost("renew")]
    public async Task<ActionResult> Renew(CreateLocalDrivingLicenseApplicationRequest request)
    {

        //request.UserId = Guid.NewGuid();
        request.UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"); //for testing purpose
        request.ApplicationId = 0;
        var ldlApp = await _createLocalDrivingLicenseApplicationService.Create(request, _renewLocalDrivingLicenseApplicationValidator);

        var apiResponse = new ApiResponse();
        apiResponse.ErrorMessages = null;
        apiResponse.IsSuccess = true;
        apiResponse.StatusCode = HttpStatusCode.Created;
        apiResponse.Result = ldlApp;

        return Ok(apiResponse);
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