using IServices.IApplicationServices.IServiceCategoryApplications.ILocalDrivingLicenseApplication;
using IServices.IApplicationServices.User;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;

namespace Services.ApplicationServices.ServiceCategoryApplications;

public class CreateLocalDrivingLicenseApplicationService : ICreateLocalDrivingLicenseApplicationService
{
    public CreateLocalDrivingLicenseApplicationService(ICreateApplicationService createApplicationService,
        ICreateNewLocalDrivingLicenseApplication createLocalDrivingLicenseApplication, 
        ICreateApplicationEntity createApplicatioEntity)
    {
        _createApplicationService = createApplicationService;
        _createlocalDrivingLicenseApplication = createLocalDrivingLicenseApplication; 
    }

    private readonly ICreateApplicationService _createApplicationService;
    private readonly ICreateNewLocalDrivingLicenseApplication _createlocalDrivingLicenseApplication; 


    public async Task<LocalDrivingLicenseApplication> Create(CreateLocalDrivingLicenseApplicationRequest entity, ILocalDrivingLicenseApplicationServicePurposeValidator validator)
    {
         validator.ValidateRequest(entity);

        var application = await _createApplicationService.CreateAsync(entity);

        entity.ApplicationId = application.ApplicationId;


        var ldlApplication = await _createlocalDrivingLicenseApplication.CreateAsync(entity);


        return ldlApplication;
    } 
}
