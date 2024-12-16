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
    public GetLocalLicenseServices(IGetAllRepository<Driver> getAllDriversRepository,
        IGetRepository<LocalDrivingLicense> getLocalLicenseRepository,
        IGetAllRepository<LocalDrivingLicense> getAllLocalLicensesRepository, IMapper mapper)
    {
        _getAllDriversRepository = getAllDriversRepository;
        _getLocalLicenseRepository = getLocalLicenseRepository;
        _getAllLocalLicensesRepository = getAllLocalLicensesRepository;
        _mapper = mapper;
    }

    private readonly IGetAllRepository<Driver> _getAllDriversRepository;
    private readonly IGetRepository<LocalDrivingLicense> _getLocalLicenseRepository;
    private readonly IGetAllRepository<LocalDrivingLicense> _getAllLocalLicensesRepository;
    private readonly IMapper _mapper;

    public async Task<LocalDrivingLicenseDTO> GetByAsync(Expression<Func<LocalDrivingLicense, bool>> predicate)
    {
        var localLicnese = await _getLocalLicenseRepository.GetAsync(predicate);

        return localLicnese is null ? null : _mapper.Map<LocalDrivingLicenseDTO>(localLicnese);
    }
 
}

public class GetInternationalLicenseServices : IGetInternationalLicenseService
{
    private readonly IGetAllRepository<Driver> _getAllDriversRepository;
    private readonly IGetRepository<InternationalDrivingLicense> _getInternationalDrivingLicenseRepository;
    private readonly IGetAllRepository<InternationalDrivingLicense> _getAllInternationalDrivingLicensesRepository;
    private readonly IMapper _mapper;

    public async Task<InternationalDrivingLicenseDTO> GetByAsync(
        Expression<Func<InternationalDrivingLicense, bool>> predicate)
    {
        var licnese = await _getInternationalDrivingLicenseRepository.GetAsync(predicate);

        return licnese is null ? null : _mapper.Map<InternationalDrivingLicenseDTO>(licnese);
    }

}