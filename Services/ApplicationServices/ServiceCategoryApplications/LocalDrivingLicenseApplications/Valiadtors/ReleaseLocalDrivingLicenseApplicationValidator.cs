using GovConnect.IServices.ILicensesServices.IDetainLicenses;
using IServices.IApplicationServices.Category;
using IServices.IApplicationServices.User;
using IServices.IValidtors.ILocalDrivingLicenseApplications;
using Microsoft.Extensions.Logging;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Services.ApplicationServices.Services.UserAppServices;
using Services.Execptions;

namespace Services.ApplicationServices.ServiceCategoryApplications;

public class ReleaseLocalDrivingLicenseApplicationValidator : CreateApplicationServiceValidator, IReleaseLocalDrivingLicenseApplicationValidator
{
    private const byte release = (byte)EnServicePurpose.Release;
    private readonly ICheckApplicationExistenceService _checkApplicationExistenceService;
    private readonly IPendingOrInProgressApplicationStatus _pendingOrInProgressApplicationStatus;
    private readonly IGetLocalDrivingLicenseByUserId _getLocalDrivingLicenseByUserId;
    private readonly IGetDetainLicense _getDetainLicense;
    private readonly ILogger<ReleaseLocalDrivingLicenseApplicationValidator> _logger;
    public ReleaseLocalDrivingLicenseApplicationValidator(
        ICheckApplicationExistenceService checkApplicationExistenceService,
        IPendingOrInProgressApplicationStatus pendingOrInProgressApplicationStatus,
        IGetLocalDrivingLicenseByUserId getLocalDrivingLicenseByUserId,
        IGetDetainLicense getDetainLicense,
        ILogger<ReleaseLocalDrivingLicenseApplicationValidator> logger) : base(logger)
    {
        _checkApplicationExistenceService = checkApplicationExistenceService;
        _pendingOrInProgressApplicationStatus = pendingOrInProgressApplicationStatus;
        _getLocalDrivingLicenseByUserId = getLocalDrivingLicenseByUserId;
        _getDetainLicense = getDetainLicense;
        _logger = logger;
    }

    public override async Task ValidateRequest(CreateApplicationRequest request)
    {

        _logger.LogInformation("---------------- Validation processes for creating release local driving license application is starting");

        try
        {

            await base.ValidateRequest(request);

            var application = await _checkApplicationExistenceService.CheckApplicationExistence(request);

            if (application != null)
            {
                _logger.LogWarning("!-!-!-! there is already an application");
                _pendingOrInProgressApplicationStatus.CheckApplicationStatus((EnApplicationStatus)application.ApplicationStatus);
            }


            CreateLocalDrivingLicenseApplicationRequest? localDrivingLicenseApplicationRequest = request as CreateLocalDrivingLicenseApplicationRequest;

            if (localDrivingLicenseApplicationRequest != null)
            {
                localDrivingLicenseApplicationRequest.ServicePurposeId = release;
            }


            var licenses = await _getLocalDrivingLicenseByUserId.Get(localDrivingLicenseApplicationRequest.UserId);

            var LDLicense = licenses.FirstOrDefault(l =>
             l.localDrivingLicense.LicenseClassId == localDrivingLicenseApplicationRequest.LicenseClassId);

            if (LDLicense == null)
            {
                _logger.LogError("!!! user does not have this license class");
                throw new DoesNotExistException("user does not have this license class");
            }


            var detainedLicense = await _getDetainLicense.GetByAsync(licenses => licenses.LicenseId == LDLicense.localDrivingLicense.LocalDrivingLicenseId);

            if (detainedLicense == null)
            {
                _logger.LogError("!!! license is not detained");
                throw new DetainedLicenseException("license is not detained");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "!!! Error in release local driving license application validation");
        }
    }
}
