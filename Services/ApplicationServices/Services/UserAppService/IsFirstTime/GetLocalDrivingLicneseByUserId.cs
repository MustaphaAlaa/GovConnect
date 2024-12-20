using IRepository;
using IServices.IApplicationServices.Category;
using ModelDTO.ApplicationDTOs.Category;
using Models.LicenseModels;
using Models.Users;

namespace Services.ApplicationServices.Services.UserAppServices.IsFirstTime;

public class GetLocalDrivingLicneseByUserId : IGetLocalDrivingLicenseByUserId
{
    public GetLocalDrivingLicneseByUserId(IGetAllRepository<Driver> getAllDriversRepository,
       IGetRepository<LocalDrivingLicense> getLocalLicenseRepository,
       IGetAllRepository<LocalDrivingLicense> getAllLocalLicensesRepository)
    {
        _getAllDriversRepository = getAllDriversRepository;
        _getAllLocalLicensesRepository = getAllLocalLicensesRepository;

    }

    private readonly IGetAllRepository<Driver> _getAllDriversRepository;
    private readonly IGetAllRepository<LocalDrivingLicense> _getAllLocalLicensesRepository;


    public async Task<IEnumerable<DriverIdLicenseClassId>> Get(Guid usrId)
    {
        var queryMethod = (await _getAllDriversRepository.GetAllAsync())
           .Where(driver => driver.UserId == usrId)
           .Join((await _getAllLocalLicensesRepository.GetAllAsync()),
               driver => driver.DriverId,
               license => license.DriverId,
               (driver, license) => new DriverIdLicenseClassId(driver.DriverId, license)
           );

        return queryMethod;
    }
}
