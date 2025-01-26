using ModelDTO.LicenseDTOs;
using Models.LicenseModels;

namespace IServices.ILicenseServices;

public interface ILocalDrivingLicenseUpdateService   : IUpdateService <LocalDrivingLicense, LocalDrivingLicenseDTO>
{
    
}