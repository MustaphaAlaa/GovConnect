using IRepository;
using IServices.IApplicationServices.User;
using IServices.IValidtors.ILocalDrivingLicenseApplications;
using Microsoft.Extensions.Logging;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Models.LicenseModels;
using Services.ApplicationServices.Services.UserAppServices;
using Services.Execptions;

namespace Services.ApplicationServices.ServiceCategoryApplications;

public class NewLocalDrivingLicenseApplicationValidator : CreateApplicationServiceValidator, INewLocalDrivingLicenseApplicationValidator
{
    private readonly ICheckApplicationExistenceService _checkApplicationExistenceService;
    private readonly IFirstTimeCheckable<CreateLocalDrivingLicenseApplicationRequest> _firstTimeChecker;
    private readonly IPendingOrInProgressApplicationStatus _pendingOrInProgressApplicationStatus;
    private readonly ILogger<INewLocalDrivingLicenseApplicationValidator> _logger;
    public NewLocalDrivingLicenseApplicationValidator(
        IGetRepository<Application> getApplicationRepository,
        IFirstTimeCheckable<CreateLocalDrivingLicenseApplicationRequest> firstTimeChecker,
        ICheckApplicationExistenceService checkApplicationExistenceService,
        IPendingOrInProgressApplicationStatus pendingOrInProgressApplicationStatus,
        ILogger<INewLocalDrivingLicenseApplicationValidator> logger) : base(logger)
    {
        _firstTimeChecker = firstTimeChecker;
        _checkApplicationExistenceService = checkApplicationExistenceService;
        _pendingOrInProgressApplicationStatus = pendingOrInProgressApplicationStatus;
        _logger = logger;
    }



    public override async Task ValidateRequest(CreateApplicationRequest request)
    {
        _logger.LogInformation("---------------- Validation processes for creating new local driving license application is starting ");

        try
        {

            await base.ValidateRequest(request);

            var application = await _checkApplicationExistenceService.CheckApplicationExistence(request);

            if (application != null)
            {
                _logger.LogWarning("!-!-!-! there is already an application");
                _pendingOrInProgressApplicationStatus.CheckApplicationStatus((EnApplicationStatus)application.ApplicationStatus);
            }


            CreateLocalDrivingLicenseApplicationRequest localDrivingLicenseApplicationRequest = request as CreateLocalDrivingLicenseApplicationRequest;

            if (localDrivingLicenseApplicationRequest != null)
             localDrivingLicenseApplicationRequest.ServicePurposeId = (byte)EnServicePurpose.New; 
             
            if (!Enum.IsDefined(typeof(EnLicenseClasses), (int)localDrivingLicenseApplicationRequest?.LicenseClassId))
            {
                _logger.LogError("!!! License class id does not exist");
                throw new DoesNotExistException("License class id does not exist");
            }

            var firstLocalDrivingLicense = await _firstTimeChecker.IsFirstTime(localDrivingLicenseApplicationRequest);

            if (!firstLocalDrivingLicense)
            {
                _logger.LogError("!!! The applicant is Already has the license class.");
                throw new AlreadyExistException("The applicant is Already has the license class.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "!!! Request Validation Failed");
            throw new InvalidRequestException("Request Validation Failed");
        }


    }
}
