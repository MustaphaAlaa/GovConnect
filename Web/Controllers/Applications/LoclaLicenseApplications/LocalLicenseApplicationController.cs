using System.Net;
using IServices.IApplicationServices.IServiceCategoryApplications.ILocalDrivingLicenseApplication;
using IServices.IApplicationServices.IServiceCategoryApplications.IRetakeTestApplication;
using IServices.IBookingServices;
using IServices.IValidators;
using IServices.IValidtors.ILocalDrivingLicenseApplications;
using Microsoft.AspNetCore.Mvc;
using ModelDTO.API;
using ModelDTO.ApplicationDTOs.User;
using ModelDTO.BookingDTOs;
using Repositorties.SPs.AppointmentReps;
using Services.BookingServices;

namespace Web.Controllers.Applications.LocalLicenseApplications;


/*
     !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
     !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
     !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
     !!!!!!!!!!!!!!!!!!!!!!!!!!
     * I'll clean and and restructre all endpoints later these for testing purpose
     *
     */


[ApiController]
[Route("Applications/LocalLicenseApplications")]
public class LocalLicenseApplicationController : ControllerBase
{

    private readonly ICreateLocalDrivingLicenseApplicationOrchestrator _createLocalDrivingLicenseApplicationService;
    private readonly IRetakeTestApplicationCreation _retakeTestApplicationCreation;
    //LDL Purpose Validators
    private readonly INewLocalDrivingLicenseApplicationValidator _newLocalDrivingLicenseApplicationValidator;
    private readonly IRenewLocalDrivingLicenseApplicationValidator _renewLocalDrivingLicenseApplicationValidator;
    private readonly IReleaseLocalDrivingLicenseApplicationValidator _releaseLocalDrivingLicenseApplicationValidator;
    private readonly IReplacementForLostLocalDrivingLicenseApplicationValidator _replacementForLostLocalDrivingLicenseApplicationValidator;
    private readonly IReplacementForDamageLocalDrivingLicenseApplicationValidator _replacementForDamageLocalDrivingLicenseApplicationValidator;

    private readonly ICreateRetakeTestApplicationValidation _createRetakeTestApplicationValidation;

    public LocalLicenseApplicationController(ICreateLocalDrivingLicenseApplicationOrchestrator createLocalDrivingLicenseApplicationService,
        INewLocalDrivingLicenseApplicationValidator newLocalDrivingLicenseApplicationValidator,
        IRenewLocalDrivingLicenseApplicationValidator renewLocalDrivingLicenseApplicationValidator,
        IReleaseLocalDrivingLicenseApplicationValidator releaseLocalDrivingLicenseApplicationValidator,
        IReplacementForLostLocalDrivingLicenseApplicationValidator replacementForLostLocalDrivingLicenseApplicationValidator,
        IReplacementForDamageLocalDrivingLicenseApplicationValidator replacementForDamageLocalDrivingLicenseApplicationValidator,
    ICreateRetakeTestApplicationValidation createRetakeTestApplicationValidation,
    IRetakeTestApplicationCreation retakeTestApplicationCreation)
    {
        _createLocalDrivingLicenseApplicationService = createLocalDrivingLicenseApplicationService;
        //LDL Purpose Validators
        _newLocalDrivingLicenseApplicationValidator = newLocalDrivingLicenseApplicationValidator;
        _renewLocalDrivingLicenseApplicationValidator = renewLocalDrivingLicenseApplicationValidator;
        _releaseLocalDrivingLicenseApplicationValidator = releaseLocalDrivingLicenseApplicationValidator;
        _replacementForLostLocalDrivingLicenseApplicationValidator = replacementForLostLocalDrivingLicenseApplicationValidator;
        _replacementForDamageLocalDrivingLicenseApplicationValidator = replacementForDamageLocalDrivingLicenseApplicationValidator;

        _createRetakeTestApplicationValidation = createRetakeTestApplicationValidation;
        _retakeTestApplicationCreation = retakeTestApplicationCreation;
    }




    [HttpPost("new")]
    public async Task<ActionResult> New(CreateLocalDrivingLicenseApplicationRequest request)
    {

        //request.UserId = Guid.NewGuid();
        request.UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"); //for testing purpose
        var apiResponse = new ApiResponse();
        request.ApplicationId = 0;
        try
        {
            var ldlApp = await _createLocalDrivingLicenseApplicationService.Create(request, _newLocalDrivingLicenseApplicationValidator);


            apiResponse.ErrorMessages = null;
            apiResponse.IsSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.Created;
            apiResponse.Result = ldlApp;
            return Ok(apiResponse);
        }
        catch (Exception ex)
        {
            apiResponse.ErrorMessages.Add(ex.Message);
            apiResponse.StatusCode = HttpStatusCode.BadRequest;
            apiResponse.Result = null;
            apiResponse.IsSuccess = false;
            return BadRequest(apiResponse);
        }
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
    public async Task<ActionResult> RetakeTheTest(CreateRetakeTestApplicationRequest request)
    {
        ApiResponse res = new ApiResponse();
        try
        {
            //should marked as not allowed if it already created;

            await _createRetakeTestApplicationValidation.Validate(request);

            var booked = await _retakeTestApplicationCreation.CreateAsync(request);

            res.StatusCode = HttpStatusCode.OK;
            res.Result = booked;
            return Ok(res);

        }
        catch (Exception ex)
        {
            res.ErrorMessages.Add(ex.Message);
            res.Result = null;
            res.StatusCode = HttpStatusCode.NotAcceptable;
            return BadRequest(res);
        }
    }

}