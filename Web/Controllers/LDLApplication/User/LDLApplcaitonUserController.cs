using IServices.IApplicationServices.IServiceCategoryApplications.ILocalDrivingLicenseApplication;
using IServices.IApplicationServices.IServiceCategoryApplications.IRetakeTestApplication;
using IServices.ILDLApplicationsAllowedToRetakeATestServices;
using IServices.IValidators;
using IServices.IValidtors.ILocalDrivingLicenseApplications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ModelDTO.API;
using ModelDTO.ApplicationDTOs.User;
using System.Net;

namespace Web.Controllers.LDLApplication.User
{
    [Route("api/LDLAppliaction/user")]
    [ApiController]
    public class LDLApplcaitonUserController : ControllerBase
    {
        private ApiResponse _apiResponse;


        private readonly ICreateLocalDrivingLicenseApplicationOrchestrator _createLocalDrivingLicenseApplicationService;
        //LDL Purpose Validators
        private readonly INewLocalDrivingLicenseApplicationValidator _newLocalDrivingLicenseApplicationValidator;


        private readonly IRetakeTestApplicationCreation _retakeTestApplicationCreation;
        private readonly IRenewLocalDrivingLicenseApplicationValidator _renewLocalDrivingLicenseApplicationValidator;
        private readonly IReleaseLocalDrivingLicenseApplicationValidator _releaseLocalDrivingLicenseApplicationValidator;
        private readonly IReplacementForLostLocalDrivingLicenseApplicationValidator _replacementForLostLocalDrivingLicenseApplicationValidator;
        private readonly IReplacementForDamageLocalDrivingLicenseApplicationValidator _replacementForDamageLocalDrivingLicenseApplicationValidator;
        private readonly ICreateRetakeTestApplicationValidation _createRetakeTestApplicationValidation;


        //for events 
        private readonly ILDLTestRetakeApplicationSubscriber _lDLTestRetakeApplicationSubscriber;

        public LDLApplcaitonUserController(ICreateLocalDrivingLicenseApplicationOrchestrator createLocalDrivingLicenseApplicationService,
            INewLocalDrivingLicenseApplicationValidator newLocalDrivingLicenseApplicationValidator,
            IRenewLocalDrivingLicenseApplicationValidator renewLocalDrivingLicenseApplicationValidator,
            IReleaseLocalDrivingLicenseApplicationValidator releaseLocalDrivingLicenseApplicationValidator,
            IReplacementForLostLocalDrivingLicenseApplicationValidator replacementForLostLocalDrivingLicenseApplicationValidator,
            IReplacementForDamageLocalDrivingLicenseApplicationValidator replacementForDamageLocalDrivingLicenseApplicationValidator,
            ICreateRetakeTestApplicationValidation createRetakeTestApplicationValidation,
            IRetakeTestApplicationCreation retakeTestApplicationCreation,
            ILDLTestRetakeApplicationSubscriber lDLTestRetakeApplicationSubscriber)
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

            _lDLTestRetakeApplicationSubscriber = lDLTestRetakeApplicationSubscriber;

            _apiResponse = new ApiResponse();
        }




        [HttpPost("new")]
        public async Task<ActionResult> New(CreateLocalDrivingLicenseApplicationRequest request)
        {

            //request.UserId = Guid.NewGuid();
            request.UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"); //for testing purpose

            request.ApplicationId = 0;
            try
            {
                var ldlApp = await _createLocalDrivingLicenseApplicationService.Create(request, _newLocalDrivingLicenseApplicationValidator);


                _apiResponse.ErrorMessages = null;
                _apiResponse.IsSuccess = true;
                _apiResponse.StatusCode = HttpStatusCode.Created;
                _apiResponse.Result = ldlApp;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.ErrorMessages.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.Result = null;
                _apiResponse.IsSuccess = false;
                return BadRequest(_apiResponse);
            }
        }


    }
}
