using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;

namespace IServices.Application.IServiceCategoryApplications;

public interface ICreateLocalDrivingLicenseApplication : ICreateService< CreateLocalDrivingLicenseApplicationRequest,LocalDrivingLicenseApplication>
{
    
}