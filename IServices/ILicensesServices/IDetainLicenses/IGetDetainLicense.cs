using System.Collections.Generic;
using System.Threading.Tasks;
using IServices;
using ModelDTO.LicenseDTOs;
using Models.LicenseModels;

namespace GovConnect.IServices.ILicensesServices.IDetainLicenses;

public interface IGetDetainLicense : IGetWhenService<DetainedLicense, DetainedLicenseDTO>
{

} 