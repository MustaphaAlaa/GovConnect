using GovConnect.IServices.ILicensesServices.IDetainLicenses;
using IServices.IApplicationServices.Category;
using IServices.IApplicationServices.IServiceCategoryApplications.ILocalDrivingLicenseApplication;
using IServices.IApplicationServices.User;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Services.ApplicationServices.Services.UserAppServices;
using Services.Execptions;

namespace Services.ApplicationServices.ServiceCategoryApplications;

public class ReplacementForLostLocalDrivingLicenseApplicationValidator : CreateApplicationServiceValidator, IReplacementForLostLocalDrivingLicenseApplicationValidator
{
    private const byte replacementLost = (byte)EnServicePurpose.ReplacementForLost;
    private readonly ICheckApplicationExistenceService _checkApplicationExistenceService;
    private readonly IPendingOrInProgressApplicationStatus _pendingOrInProgressApplicationStatus;
    private readonly IGetLocalDrivingLicenseByUserId _getLocalDrivingLicenseByUserId;
    private readonly IGetDetainLicense _getDetainLicense;
    public ReplacementForLostLocalDrivingLicenseApplicationValidator(
        ICheckApplicationExistenceService checkApplicationExistenceService,
        IPendingOrInProgressApplicationStatus pendingOrInProgressApplicationStatus,
        IGetLocalDrivingLicenseByUserId getLocalDrivingLicenseByUserId,
        IGetDetainLicense getDetainLicense)
    {
        _checkApplicationExistenceService = checkApplicationExistenceService;
        _pendingOrInProgressApplicationStatus = pendingOrInProgressApplicationStatus;
        _getLocalDrivingLicenseByUserId = getLocalDrivingLicenseByUserId;
        _getDetainLicense = getDetainLicense;
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
            localDrivingLicenseApplicationRequest.ServicePurposeId = replacementLost;
        }


        var licenses = await _getLocalDrivingLicenseByUserId.Get(localDrivingLicenseApplicationRequest.UserId);

        var LDLicense = licenses.FirstOrDefault(l =>
         l.localDrivingLicense.LicenseClassId == localDrivingLicenseApplicationRequest.LicenseClassId);

        if (LDLicense == null)
        {
            throw new DoesNotExistException("user does not have this license class");
        }

        if (LDLicense.localDrivingLicense.ExpiryDate < DateTime.Now)
        {
            throw new InvalidRequestException("license is expired");
        }

        var detainedLicense = await _getDetainLicense.GetByAsync(licenses => licenses.LicenseId == LDLicense.localDrivingLicense.LocalDrivingLicenseId);

        if (detainedLicense != null)
            throw new DetainedLicenseException("license is detained");
    }
}