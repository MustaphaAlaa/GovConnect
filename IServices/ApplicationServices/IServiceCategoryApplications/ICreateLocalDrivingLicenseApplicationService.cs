using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;

namespace IServices.IApplicationServices.IServiceCategoryApplications;

public interface ICreateLocalDrivingLicenseApplicationService
{
    Task<LocalDrivingLicenseApplication> Create(CreateLocalDrivingLicenseApplicationRequest request);
}
