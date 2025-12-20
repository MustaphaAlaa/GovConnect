using IServices.IValidtors.ILocalDrivingLicenseApplications;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;

namespace IServices.IApplicationServices.IServiceCategoryApplications.ILocalDrivingLicenseApplication;



 
public interface ICreateLocalDrivingLicenseApplicationOrchestrator
{
    
    Task<LocalDrivingLicenseApplication> Create(CreateLocalDrivingLicenseApplicationRequest request, ILocalDrivingLicenseApplicationServicePurposeValidator validator);
}

