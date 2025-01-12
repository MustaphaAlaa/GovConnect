using IRepository;
using IServices.IApplicationServices.IServiceCategoryApplications.ILocalDrivingLicenseApplication;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Services.Execptions;

namespace Services.ApplicationServices.ServiceCategoryApplications;


/// <summary>
/// The class is responsible for inserting a new record in the LocalDrivingApplication table.
/// </summary>
public class LocalDrivingLicenseApplicationCreator : INewLocalDrivingLicenseApplicationCreator
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LocalDrivingLicenseApplicationCreator"/> class.
    /// </summary>
    /// <param name="localDrivingLicenseApplicationRepository">The repository for creating LocalDrivingLicenseApplication records.</param>
    public LocalDrivingLicenseApplicationCreator(ICreateRepository<LocalDrivingLicenseApplication> localDrivingLicenseApplicationRepository)
    {
        _localDrivingLicenseApplicationRepository = localDrivingLicenseApplicationRepository;
    }

    private readonly ICreateRepository<LocalDrivingLicenseApplication> _localDrivingLicenseApplicationRepository;

    /// <summary>
    /// Converts a <see cref="CreateLocalDrivingLicenseApplicationRequest"/> to a <see cref="LocalDrivingLicenseApplication"/> and inserts it into the database.
    /// </summary>
    /// <param name="entity">The request object containing the details for the new LocalDrivingLicenseApplication.</param>
    /// <returns>The created <see cref="LocalDrivingLicenseApplication"/>.</returns>
    public async Task<LocalDrivingLicenseApplication> CreateAsync(CreateLocalDrivingLicenseApplicationRequest entity)
    {
        LocalDrivingLicenseApplication localDrivingLicenseApplication = new()
        {
            ApplicationId = entity.ApplicationId,
            LicenseClassId = entity.LicenseClassId,
            ReasonForTheApplication = ((EnServicePurpose)entity.ServicePurposeId).ToString().Replace("_", " "),
        };

        var ldlApplication = await _localDrivingLicenseApplicationRepository.CreateAsync(localDrivingLicenseApplication)
             ?? throw new FailedToCreateException();

        return ldlApplication;
    }
}
