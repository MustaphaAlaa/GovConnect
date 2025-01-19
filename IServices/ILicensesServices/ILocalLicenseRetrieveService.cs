using ModelDTO.LicenseDTOs;
using Models.LicenseModels;

namespace IServices.ILicencesServices;

public interface ILocalLicenseRetrieveService : IAsyncRetrieveService<LocalDrivingLicense,LocalDrivingLicenseDTO>
{
    
}