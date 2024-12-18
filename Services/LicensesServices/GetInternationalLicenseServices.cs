using System.Linq.Expressions;
using AutoMapper;
using IRepository;
using IServices.ILicencesServices;
using ModelDTO.LicenseDTOs;
using Models.LicenseModels;
using Models.Users;

namespace Services.LicensesServices;

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