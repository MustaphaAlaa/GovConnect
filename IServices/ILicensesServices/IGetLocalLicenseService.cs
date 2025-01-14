using ModelDTO.LicenseDTOs;
using Models.LicenseModels;

namespace IServices.ILicencesServices;

public interface IGetLocalLicenseService : IAsyncRetrieveService<LocalDrivingLicense,LocalDrivingLicenseDTO>
{
    
}