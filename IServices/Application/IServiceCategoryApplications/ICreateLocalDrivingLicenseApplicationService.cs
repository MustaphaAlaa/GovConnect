using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;

namespace IServices.Application.IServiceCategoryApplications;

public interface ICreateLocalDrivingLicenseApplicationService
{
    Task<LocalDrivingLicenseApplication> Create(CreateLocalDrivingLicenseApplicationRequest request);
}
