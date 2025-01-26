
using System.ComponentModel;
using ModelDTO.LicenseDTOs;
using Models.LicenseModels;

namespace IServices.ILicenseServices;

public interface ILocalDrivingLicenseCreationService   : ICreateService<LocalDrivingLicense, LocalDrivingLicenseDTO>
{
    
}