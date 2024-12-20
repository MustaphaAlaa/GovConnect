using ModelDTO.LicenseDTOs;
using Models.LicenseModels;

namespace IServices.ILicencesServices;

public interface IGetInternationalLicenseService : IGetWhenService<InternationalDrivingLicense,InternationalDrivingLicenseDTO>
{
    
}