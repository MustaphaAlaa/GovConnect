using IRepository;
using IServices.IApplicationServices.Category;
using IServices.IApplicationServices.IServiceCategoryApplications.ILocalDrivingLicenseApplication;
using IServices.IApplicationServices.User;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Models.LicenseModels;
using Services.ApplicationServices.Services.UserAppServices;
using Services.Execptions;

namespace Services.ApplicationServices.ServiceCategoryApplications;

public class RenewLocalDrivingLicenseApplicationValidator : CreateApplicationServiceValidator, INewLocalDrivingLicenseApplicationValidator
{
    private const byte renew = (byte)EnServicePurpose.Renew;
    private readonly ICheckApplicationExistenceService _checkApplicationExistenceService;
    private readonly IFirstTimeCheckable<CreateLocalDrivingLicenseApplicationRequest> _firstTimeChecker;
    private readonly IPendingOrInProgressApplicationStatus _pendingOrInProgressApplicationStatus;

    private readonly IGetLocalDrivingLicenseByUserId _getLocalDrivingLicenseByUserId;

    public RenewLocalDrivingLicenseApplicationValidator(
        IFirstTimeCheckable<CreateLocalDrivingLicenseApplicationRequest> firstTimeChecker,
        ICheckApplicationExistenceService checkApplicationExistenceService,
        IPendingOrInProgressApplicationStatus pendingOrInProgressApplicationStatus,
        IGetLocalDrivingLicenseByUserId getLocalDrivingLicenseByUserId)
    {

        _firstTimeChecker = firstTimeChecker;
        _checkApplicationExistenceService = checkApplicationExistenceService;
        _pendingOrInProgressApplicationStatus = pendingOrInProgressApplicationStatus;
        _getLocalDrivingLicenseByUserId = getLocalDrivingLicenseByUserId;
    }

    public override async void ValidateRequest(CreateApplicationRequest request)
    {
        base.ValidateRequest(request);

        var application = await _checkApplicationExistenceService.CheckApplicationExistence(request);

        if (application != null)
        {
            _pendingOrInProgressApplicationStatus.CheckApplicationStatus((EnApplicationStatus)application.ApplicationStatus);
        }


        CreateLocalDrivingLicenseApplicationRequest? localDrivingLicenseApplicationRequest = request as CreateLocalDrivingLicenseApplicationRequest;

        if (localDrivingLicenseApplicationRequest != null)
        {
            localDrivingLicenseApplicationRequest.ServicePurposeId = renew;
        }

        var licenses = await _getLocalDrivingLicenseByUserId.Get(localDrivingLicenseApplicationRequest.UserId);

        var LDLicense = licenses.FirstOrDefault(l =>
         l.localDrivingLicense.LicenseClassId == localDrivingLicenseApplicationRequest.LicenseClassId);

        if (LDLicense == null)
        {
            throw new DoesNotExistException("user does not have this license class");
        }

        if (LDLicense.localDrivingLicense.ExpiryDate > DateTime.Now)
        {
            throw new InvalidRequestException("license is not expired");
        }

    }
}
