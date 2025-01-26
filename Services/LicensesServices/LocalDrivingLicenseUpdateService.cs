using IServices.ILicencesServices;
using ModelDTO.LicenseDTOs;
using Models.LicenseModels;

namespace Services.LicensesServices;

public class LocalDrivingLicenseUpdateService :  ILocalDrivingLicenseUpdateService
{
     
    public Task<LocalDrivingLicenseDTO> UpdateAsync(LocalDrivingLicense updateRequest)
    {
        throw new NotImplementedException();
    }
}