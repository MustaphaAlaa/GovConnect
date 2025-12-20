using AutoMapper;
using IRepository;
using IServices.IApplicationServices.Category;
using IServices.IApplicationServices.User;
using ModelDTO.ApplicationDTOs.User;
using Models.LicenseModels;
using Models.Users;

namespace Services.ApplicationServices.Services.UserAppServices.IsFirstTime;


public class FirstTimeLocalDrivingLicense : IFirstTimeApplicationCheckable<CreateLocalDrivingLicenseApplicationRequest>
{

    public FirstTimeLocalDrivingLicense(IGetLocalDrivingLicenseByUserId getLocalDrivingLicneseByUserId)
    {
        _getLocalLicenseByUsrId = getLocalDrivingLicneseByUserId;
    }


    private readonly IGetLocalDrivingLicenseByUserId _getLocalLicenseByUsrId;


    public async Task<bool> IsFirstTime(CreateLocalDrivingLicenseApplicationRequest request)
    {
        var driverIdLicenses = await _getLocalLicenseByUsrId.Get(request.UserId);

        var isExists = driverIdLicenses is null ? false :
                        driverIdLicenses.Any(result => result.localDrivingLicense.LicenseClassId == request.LicenseClassId);

        return !isExists;
    }
}