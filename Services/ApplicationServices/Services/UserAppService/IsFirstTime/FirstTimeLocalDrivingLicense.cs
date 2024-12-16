using AutoMapper;
using IRepository;
using IServices.IApplicationServices.User;
using ModelDTO.ApplicationDTOs.User;
using Models.LicenseModels;
using Models.Users;

namespace Services.ApplicationServices.Services.UserAppServices.IsFirstTime;

public class FirstTimeLocalDrivingLicense: IFirstTimeCheckable
{
    
    public FirstTimeLocalDrivingLicense(IGetAllRepository<Driver> getAllDriversRepository,
        IGetRepository<LocalDrivingLicense> getLocalLicenseRepository,
        IGetAllRepository<LocalDrivingLicense> getAllLocalLicensesRepository )
    {
        _getAllDriversRepository = getAllDriversRepository;
        _getAllLocalLicensesRepository = getAllLocalLicensesRepository;
         
    }

    private readonly IGetAllRepository<Driver> _getAllDriversRepository;
    private readonly IGetAllRepository<LocalDrivingLicense> _getAllLocalLicensesRepository;
 

    
    public async Task<bool> IsFirstTime(CreateApplicationRequest request)
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