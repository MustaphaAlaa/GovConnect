using AutoMapper;
using IRepository;
using IServices.IApplicationServices.Category;
using IServices.IApplicationServices.User;
using ModelDTO.ApplicationDTOs.User;
using Models.LicenseModels;
using Models.Users;

namespace Services.ApplicationServices.Services.UserAppServices.IsFirstTime;


public class FirstTimeLocalDrivingLicense : IFirstTimeCheckable<CreateLocalDrivingLicenseApplicationRequest>
{

    public FirstTimeLocalDrivingLicense(IGetLocalDrivingLicneseByUserId getLocalDrivingLicneseByUserId)
    {
        _getLocalLicenseByUsrId = getLocalDrivingLicneseByUserId;
    }


    private readonly IGetLocalDrivingLicneseByUserId _getLocalLicenseByUsrId;


    public async Task<bool> IsFirstTime(CreateLocalDrivingLicenseApplicationRequest request)
    {
        var queryMethod = await _getLocalLicenseByUsrId.Get(request.UserId);
        var isExists = queryMethod.Any(result => result.localDrivingLicense.LicenseClassId == request.LicenseClassId);

        return !isExists;
    }
}