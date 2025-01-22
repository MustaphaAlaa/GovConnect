using IRepository.IGenericRepositories;
using IServices.IApplicationServices.User;
using ModelDTO.ApplicationDTOs.User;
using Models.LicenseModels;
using Models.Users;

namespace Services.ApplicationServices.Services.UserAppServices.IsFirstTime;

public class FirstTimeInternationalDrivingLicense : IFirstTimeApplicationCheckable<CreateInternationalDrivingLicenseApplicationRequest>
{
    public FirstTimeInternationalDrivingLicense(IGetAllRepository<Driver> getAllDriversRepository, IGetAllRepository<InternationalDrivingLicense> getAllInternationalLicensesRepository)
    {
        _getAllDriversRepository = getAllDriversRepository;
        _getAllInternationalLicensesRepository = getAllInternationalLicensesRepository;
    }


    private readonly IGetAllRepository<Driver> _getAllDriversRepository;
    private readonly IGetAllRepository<InternationalDrivingLicense> _getAllInternationalLicensesRepository;



    public async Task<bool> IsFirstTime(CreateInternationalDrivingLicenseApplicationRequest request)
    {
        //code...
        return false;
    }






}