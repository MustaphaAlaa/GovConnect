
using System.ComponentModel;
using ModelDTO.LicenseDTOs;
using Models.LicenseModels;

namespace IServices.ILicencesServices;

public interface ICreateLocalLicenseService  //: ICreateService<CreateLocalLicenseRequest, License>
{
    
}

public interface IGetLocalLicenseService : IGetWhenService<LocalLicense,LocalLicenseDTO>
{
    
}