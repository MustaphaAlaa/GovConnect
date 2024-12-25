using System.Runtime.CompilerServices;
using GovConnect.IServices.ILicensesServices.IDetainLicenses;
using IRepository;
using IServices.IApplicationServices.Category;
using IServices.IApplicationServices.IServiceCategoryApplications.ILocalDrivingLicenseApplication;
using IServices.IApplicationServices.User;
using Microsoft.Extensions.Logging;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Models.LicenseModels;
using Services.ApplicationServices.Services.UserAppServices;
using Services.Execptions;

namespace Services.ApplicationServices.ServiceCategoryApplications;

public class RenewLocalDrivingLicenseApplicationValidator : CreateApplicationServiceValidator, IRenewLocalDrivingLicenseApplicationValidator
{
    private const byte renew = (byte)EnServicePurpose.Renew;
    private readonly ICheckApplicationExistenceService _checkApplicationExistenceService;
    private readonly IPendingOrInProgressApplicationStatus _pendingOrInProgressApplicationStatus;
    private readonly IGetLocalDrivingLicenseByUserId _getLocalDrivingLicenseByUserId;
    private readonly IGetDetainLicense _getDetainLicense;
    private readonly ILogger<IRenewLocalDrivingLicenseApplicationValidator> _logger;

    public RenewLocalDrivingLicenseApplicationValidator(
        ICheckApplicationExistenceService checkApplicationExistenceService,
        IPendingOrInProgressApplicationStatus pendingOrInProgressApplicationStatus,
        IGetLocalDrivingLicenseByUserId getLocalDrivingLicenseByUserId,
        IGetDetainLicense getDetainLicense,
        ILogger<IRenewLocalDrivingLicenseApplicationValidator> logger) : base(logger)
    {
        _checkApplicationExistenceService = checkApplicationExistenceService;
        _pendingOrInProgressApplicationStatus = pendingOrInProgressApplicationStatus;
        _getLocalDrivingLicenseByUserId = getLocalDrivingLicenseByUserId;
        _getDetainLicense = getDetainLicense;
        _logger = logger;
    }

    public override async Task ValidateRequest(CreateApplicationRequest request)
    {
        _logger.LogInformation("----------------Vaidation porcesses for creating renew local drving license applicaiton is starting ");

        try
        {

            await base.ValidateRequest(request);

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

            var detainedLicense = await _getDetainLicense.GetByAsync(licenses => licenses.LicenseId == LDLicense.localDrivingLicense.LocalDrivingLicenseId);

            if (detainedLicense != null)
            {
                throw new DetainedLicenseException("license is detained");

            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "!!! Request Validation Faild");
            throw new InvalidRequestException();
        }

    }
}
