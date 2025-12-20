using ModelDTO.LicenseDTOs;
using Models.LicenseModels;

namespace IServices.ILicenseServices;

public interface IGetInternationalLicenseService : IAsyncRetrieveService<InternationalDrivingLicense,InternationalDrivingLicenseDTO>
{
    
}