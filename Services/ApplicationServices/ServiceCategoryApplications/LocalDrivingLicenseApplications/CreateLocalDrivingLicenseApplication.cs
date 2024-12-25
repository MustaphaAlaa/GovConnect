using IRepository;
using IServices.IApplicationServices.IServiceCategoryApplications.ILocalDrivingLicenseApplication;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Services.Execptions;

namespace Services.ApplicationServices.ServiceCategoryApplications;


public class CreateLocalDrivingLicenseApplication : ICreateNewLocalDrivingLicenseApplication
{
    public CreateLocalDrivingLicenseApplication(ICreateRepository<LocalDrivingLicenseApplication> localDrivingLicenseApplicationRepository)
    {
        _localDrivingLicenseApplicationRepository = localDrivingLicenseApplicationRepository;
    }

    private readonly ICreateRepository<LocalDrivingLicenseApplication> _localDrivingLicenseApplicationRepository;



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
