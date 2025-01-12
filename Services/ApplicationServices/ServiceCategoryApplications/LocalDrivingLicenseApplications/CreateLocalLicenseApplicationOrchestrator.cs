using IServices.IApplicationServices.IServiceCategoryApplications.ILocalDrivingLicenseApplication;
using IServices.IApplicationServices.User;
using IServices.IValidtors.ILocalDrivingLicenseApplications;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;

namespace Services.ApplicationServices.ServiceCategoryApplications;

/// <summary>
/// Orchestrates the creation of a local driving license application.
/// </summary>
public class CreateLocalLicenseApplicationOrchestrator : ICreateLocalDrivingLicenseApplicationOrchestrator
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateLocalLicenseApplicationOrchestrator"/> class.
    /// </summary>
    /// <param name="createApplicationService">Service for creating applications.</param>
    /// <param name="createLocalDrivingLicenseApplication">Service for creating a local driving license application.</param>
    /// <param name="createApplicatioEntity">Service for creating application entity.</param>
    public CreateLocalLicenseApplicationOrchestrator(ICreateApplicationService createApplicationService,
        INewLocalDrivingLicenseApplicationCreator createLocalDrivingLicenseApplication,
        ICreateApplicationEntity createApplicatioEntity)
    {
        _createApplicationService = createApplicationService;
        _createLocalDrivingLicenseApplication = createLocalDrivingLicenseApplication;
    }

    private readonly ICreateApplicationService _createApplicationService;
    private readonly INewLocalDrivingLicenseApplicationCreator _createLocalDrivingLicenseApplication;

    /// <summary>
    /// Creates a new local driving license application.
    /// </summary>
    /// <param name="entity">The request object containing the details for the new local driving license application.</param>
    /// <param name="validator">Validator for the local driving license application service purpose (purposes like, new, renew, ...etc).</param>
    /// <returns>The created <see cref="LocalDrivingLicenseApplication"/>.</returns>
    public async Task<LocalDrivingLicenseApplication> Create(CreateLocalDrivingLicenseApplicationRequest entity, ILocalDrivingLicenseApplicationServicePurposeValidator validator)
    {
        await validator.ValidateRequest(entity);

        var application = await _createApplicationService.CreateAsync(entity);

        entity.ApplicationId = application.ApplicationId;

        var ldlApplication = await _createLocalDrivingLicenseApplication.CreateAsync(entity);

        return ldlApplication;
    }
}
