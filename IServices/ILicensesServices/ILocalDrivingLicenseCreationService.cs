
using System.ComponentModel;
using ModelDTO.LicenseDTOs;
using Models.LicenseModels;

namespace IServices.ILicencesServices;

public interface ILocalDrivingLicenseCreationService   : ICreateService<LocalDrivingLicense, LocalDrivingLicenseDTO>
{
    
}