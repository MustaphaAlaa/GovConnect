using IRepository.IGenericRepositories;
using IServices.IApplicationServices.IServiceCategoryApplications.ILocalDrivingLicenseApplication;
using IServices.IApplicationServices.User;
using IServices.IValidtors.ILocalDrivingLicenseApplications;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Models.LicenseModels;
using Services.ApplicationServices.Services.UserAppServices;
using Services.Exceptions;

namespace Services.ApplicationServices.ServiceCategoryApplications;

public class NewLocalDrivingLicenseApplicationValidator : CreateApplicationServiceValidator, INewLocalDrivingLicenseApplicationValidator
{
    private readonly ICheckApplicationExistenceService _checkApplicationExistenceService;
    private readonly IFirstTimeApplicationCheckable<CreateLocalDrivingLicenseApplicationRequest> _firstTimeChecker;
    private readonly IPendingOrInProgressApplicationStatus _pendingOrInProgressApplicationStatus;
    private readonly IGetLocalDrivingLicenseApplication _getLocalDrivingLicenseApplication;
    private readonly ILogger<INewLocalDrivingLicenseApplicationValidator> _logger;

    public NewLocalDrivingLicenseApplicationValidator(
        IGetRepository<Application> getApplicationRepository,
        IFirstTimeApplicationCheckable<CreateLocalDrivingLicenseApplicationRequest> firstTimeChecker,
        ICheckApplicationExistenceService checkApplicationExistenceService,
        IGetLocalDrivingLicenseApplication getLocalDrivingLicenseApplication,
        IPendingOrInProgressApplicationStatus pendingOrInProgressApplicationStatus,
        ILogger<INewLocalDrivingLicenseApplicationValidator> logger) : base(logger)
    {
        _firstTimeChecker = firstTimeChecker;
        _checkApplicationExistenceService = checkApplicationExistenceService;
        _pendingOrInProgressApplicationStatus = pendingOrInProgressApplicationStatus;
        _getLocalDrivingLicenseApplication = getLocalDrivingLicenseApplication;
        _logger = logger;
    }



    public override async Task ValidateRequest(CreateApplicationRequest request)
    {
        _logger.LogInformation("---------------- Validate processes for creating new local driving license application is starting ");

        try
        {

            await base.ValidateRequest(request);

            var application = await _checkApplicationExistenceService.CheckApplicationExistence(request);

            CreateLocalDrivingLicenseApplicationRequest localDrivingLicenseApplicationRequest = request as CreateLocalDrivingLicenseApplicationRequest;

            if (application != null)
            {
                var lc = await _getLocalDrivingLicenseApplication.GetByAsync(ldl => ldl.LicenseClassId == localDrivingLicenseApplicationRequest.LicenseClassId);

                if (lc is not null)
                {
                    _logger.LogWarning("!-!-!-! there is already an application");
                    _pendingOrInProgressApplicationStatus.CheckApplicationStatus((EnApplicationStatus)application.ApplicationStatus);
                }
            }



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
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message, "!!! Request Validate Failed");
            //throw new InvalidRequestException("Request Validate Failed");
            throw;
        }


    }
}
