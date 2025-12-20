using ModelDTO.LicenseDTOs;
using Models.LicenseModels;

namespace IServices.ILicenseServices;

public interface ILocalLicenseRetrieveService : IAsyncRetrieveService<LocalDrivingLicense,LocalDrivingLicenseDTO?>
{
    
}