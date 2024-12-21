using IRepository;
using IServices.IApplicationServices.IServiceCategoryApplications.ILocalDrivingLicenseApplication;
using IServices.IApplicationServices.User;
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

    public NewLocalDrivingLicenseApplicationValidator(
        IGetRepository<Application> getApplicationRepository,
        IFirstTimeCheckable<CreateLocalDrivingLicenseApplicationRequest> firstTimeChecker,
        ICheckApplicationExistenceService checkApplicationExistenceService,
        IPendingOrInProgressApplicationStatus pendingOrInProgressApplicationStatus)
    {

        _firstTimeChecker = firstTimeChecker;
        _checkApplicationExistenceService = checkApplicationExistenceService;
        _pendingOrInProgressApplicationStatus = pendingOrInProgressApplicationStatus;

    }

    public override async Task ValidateRequest(CreateApplicationRequest request)
    {
        await base.ValidateRequest(request);

        var application = await _checkApplicationExistenceService.CheckApplicationExistence(request);

        if (application != null)
        {
            _pendingOrInProgressApplicationStatus.CheckApplicationStatus((EnApplicationStatus)application.ApplicationStatus);
        }


        CreateLocalDrivingLicenseApplicationRequest localDrivingLicenseApplicationRequest = request as CreateLocalDrivingLicenseApplicationRequest;

        localDrivingLicenseApplicationRequest.ServicePurposeId = (byte)EnServicePurpose.New;

        if (!Enum.IsDefined(typeof(EnLicenseClasses), (int)localDrivingLicenseApplicationRequest?.LicenseClassId))
            throw new DoesNotExistException("License class id does not exist");


        var firstLocalDrivingLicense = await _firstTimeChecker.IsFirstTime(localDrivingLicenseApplicationRequest);

        if (!firstLocalDrivingLicense)
            throw new AlreadyExistException("The applicant is Already has the license class ");

    }
}
