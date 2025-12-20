using GovConnect.IServices.ILicensesServices.IDetainLicenses;
using IServices.IApplicationServices.Category;
using IServices.IApplicationServices.User;
using IServices.IValidtors.ILocalDrivingLicenseApplications;
using Microsoft.Extensions.Logging;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Services.ApplicationServices.Services.UserAppServices;
using Services.Exceptions;

namespace Services.ApplicationServices.ServiceCategoryApplications;

public class ReplacementForDamageLocalDrivingLicenseApplicationValidator : CreateApplicationServiceValidator,
    IReplacementForDamageLocalDrivingLicenseApplicationValidator
{
    private const byte replacementDamage = (byte)EnServicePurpose.Replacement_For_Damage;
    private readonly ICheckApplicationExistenceService _checkApplicationExistenceService;
    private readonly IPendingOrInProgressApplicationStatus _pendingOrInProgressApplicationStatus;
    private readonly IGetLocalDrivingLicenseByUserId _getLocalDrivingLicenseByUserId;
    private readonly IGetDetainLicense _getDetainLicense;
    private readonly ILogger<ReplacementForDamageLocalDrivingLicenseApplicationValidator> _logger;

    public ReplacementForDamageLocalDrivingLicenseApplicationValidator(
        ICheckApplicationExistenceService checkApplicationExistenceService,
        IPendingOrInProgressApplicationStatus pendingOrInProgressApplicationStatus,
        IGetLocalDrivingLicenseByUserId getLocalDrivingLicenseByUserId,
        IGetDetainLicense getDetainLicense,
        ILogger<ReplacementForDamageLocalDrivingLicenseApplicationValidator> logger) : base(logger)
    {
        _checkApplicationExistenceService = checkApplicationExistenceService;
        _pendingOrInProgressApplicationStatus = pendingOrInProgressApplicationStatus;
        _getLocalDrivingLicenseByUserId = getLocalDrivingLicenseByUserId;
        _getDetainLicense = getDetainLicense;
        _logger = logger;
    }

    public override async Task ValidateRequest(CreateApplicationRequest request)
    {
        await base.ValidateRequest(request);

        var application = await _checkApplicationExistenceService.CheckApplicationExistence(request);

        if (application != null)
        {
            _pendingOrInProgressApplicationStatus.CheckApplicationStatus(
                (EnApplicationStatus)application.ApplicationStatus);
        }


        CreateLocalDrivingLicenseApplicationRequest? localDrivingLicenseApplicationRequest =
            request as CreateLocalDrivingLicenseApplicationRequest;

        if (localDrivingLicenseApplicationRequest != null)
        {
            localDrivingLicenseApplicationRequest.ServicePurposeId = replacementDamage;
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

        var detainedLicense = await _getDetainLicense.GetByAsync(licenses =>
            licenses.LicenseId == LDLicense.localDrivingLicense.LocalDrivingLicenseId);

        if (detainedLicense != null)
            throw new DetainedLicenseException("license is detained");
    }
}