using AutoMapper;
using IRepository;
using IServices.IApplicationServices.User;
using ModelDTO.ApplicationDTOs.User;
using Models.LicenseModels;
using Models.Users;

namespace Services.ApplicationServices.Services.UserAppServices.IsFirstTime;

public record DriverIdLicenseClassId(Guid driverId, LocalDrivingLicense localDrivingLicense);

public interface IGetLocalDrivingLicneseByUserId
{
    Task<IEnumerable<DriverIdLicenseClassId>> Get(Guid usrId);
}

public class LocalDrivingLicneseByUserIdAndLicenseClassId : IGetLocalDrivingLicneseByUserId
{
    public LocalDrivingLicneseByUserIdAndLicenseClassId(IGetAllRepository<Driver> getAllDriversRepository,
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


public class FirstTimeLocalDrivingLicense : IFirstTimeCheckable<CreateLocalDrivingLicenseApplicationRequest>
{

    public FirstTimeLocalDrivingLicense(IGetAllRepository<Driver> getAllDriversRepository,
        IGetRepository<LocalDrivingLicense> getLocalLicenseRepository,
        IGetAllRepository<LocalDrivingLicense> getAllLocalLicensesRepository,
        IGetLocalDrivingLicneseByUserId getLocalDrivingLicneseByUserId)
    {
        _getAllDriversRepository = getAllDriversRepository;
        _getAllLocalLicensesRepository = getAllLocalLicensesRepository;
        _getLocalLicenseByUsrId = getLocalDrivingLicneseByUserId;

    }

    private readonly IGetAllRepository<Driver> _getAllDriversRepository;
    private readonly IGetAllRepository<LocalDrivingLicense> _getAllLocalLicensesRepository;
    private readonly IGetLocalDrivingLicneseByUserId _getLocalLicenseByUsrId;


    public async Task<bool> IsFirstTime(CreateLocalDrivingLicenseApplicationRequest request)
    {
        var queryMethod = await _getLocalLicenseByUsrId.Get(request.UserId);
        var isExists = queryMethod.Any(result => result.localDrivingLicense.LicenseClassId == request.LicenseClassId);

        return !isExists;
    }
}