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

public class GetLocalLicenseServices : IGetLocalLicenseService
{
    private readonly IGetAllRepository<Driver> _getAllDriversRepository;
    private readonly IGetRepository<LocalDrivingLicense> _getLocalLicenseRepository;
    private readonly IGetAllRepository<LocalDrivingLicense> _getAllLocalLicensesRepository;
    private readonly IMapper _mapper;

  public  async Task<LocalDrivingLicenseDTO> GetByAsync(Expression<Func<LocalDrivingLicense, bool>> predicate)
    {
      var localLicnese =  await  _getLocalLicenseRepository.GetAsync(predicate);

      return localLicnese is null ? null : _mapper.Map<LocalDrivingLicenseDTO>(localLicnese);
    }
  
  
    public async Task<bool> IsNewLicenseClassForTheUser(CreateApplicationRequest request)
    {
        bool queryMethod = (await _getAllDriversRepository.GetAllAsync())
            .Where(driver => driver.UserId == request.UserId)
            .Join((await _getAllLocalLicensesRepository.GetAllAsync()),
                driver => driver.DriverId,
                license => license.DriverId,
                (driver, license) => new { Id = driver.DriverId, license.LicenseClass }
            )
            .Any(result => result.LicenseClass.LicenseClassId == request.LicenseClassId);

        return !queryMethod;
    }

  
}
public class GetInternationalLicenseServices : IGetInternationalLicenseService
{
    private readonly IGetAllRepository<Driver> _getAllDriversRepository;
    private readonly IGetRepository<InternationalDrivingLicense> _getInternationalDrivingLicenseRepository;
    private readonly IGetAllRepository<InternationalDrivingLicense> _getAllInternationalDrivingLicensesRepository;
    private readonly IMapper _mapper;

  public  async Task<InternationalDrivingLicenseDTO> GetByAsync(Expression<Func<InternationalDrivingLicense, bool>> predicate)
    {
      var licnese =  await  _getInternationalDrivingLicenseRepository.GetAsync(predicate);

      return licnese is null ? null : _mapper.Map<InternationalDrivingLicenseDTO>(licnese);
    }
  
  
    public async Task<bool> IsNewInternationalDrivingLicenseForTheUser(CreateApplicationRequest request)
    {
       
        //if the applicant already has an international license
        throw new Exception();

    }

  
}

