using ModelDTO.LicenseDTOs;
using Models.LicenseModels;

namespace IServices.ILicencesServices;

public interface ILocalDrivingLicenseUpdateService   : IUpdateService <LocalDrivingLicense, LocalDrivingLicenseDTO>
{
    
}