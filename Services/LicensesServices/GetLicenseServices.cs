using System.ComponentModel;
using System.Linq.Expressions;
using AutoMapper;
using IRepository;
using IServices.ILicencesServices;
using ModelDTO.ApplicationDTOs.User;
using ModelDTO.LicenseDTOs;
using Models.LicenseModels;
using Models.Users;

namespace Services.LicensesServices;

public class GetLicenseServices : IGetLocalLicenseService
{
    private readonly IGetAllRepository<Driver> _getAllDriversRepository;
    private readonly IGetRepository<LocalLicense> _getLocalLicenseRepository;
    private readonly IGetAllRepository<LocalLicense> _getAllLocalLicensesRepository;
    private readonly IMapper _mapper;

  public  async Task<LocalLicenseDTO> GetByAsync(Expression<Func<LocalLicense, bool>> predicate)
    {
      var localLicnese =  await  _getLocalLicenseRepository.GetAsync(predicate);

      return localLicnese is null ? null : _mapper.Map<LocalLicenseDTO>(localLicnese);
    }
  
  
    public async Task<bool> IsNewLicenseClassForTheUser(CreateApplicationRequest request)
    {
        bool queryMethod = (await _getAllDriversRepository.GetAllAsync())
            .Where(driver => driver.UserId == request.UserId)
            .Join((await _getAllLocalLicensesRepository.GetAllAsync()),
                driver => driver.Id,
                license => license.DriverId,
                (driver, license) => new { driver.Id, license.LicenseClass }
            )
            .Any(result => result.LicenseClass.LicenseClassId == request.LicenseClassId);

        return !queryMethod;
    }

  
}