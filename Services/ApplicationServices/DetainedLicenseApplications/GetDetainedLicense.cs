using System.Linq.Expressions;
using AutoMapper;
using GovConnect.IServices.ILicensesServices.IDetainLicenses;
using IRepository.IGenericRepositories;
using ModelDTO.LicenseDTOs;
using Models.LicenseModels;

public class GetDetainedLicense : IGetDetainLicense
{
    private readonly IGetRepository<DetainedLicense> _getRepository;
    private readonly IMapper _mapper;
    public GetDetainedLicense(IGetRepository<DetainedLicense> getRepository, IMapper mapper)
    {
        _getRepository = getRepository;
        _mapper = mapper;
    }
    public async Task<DetainedLicenseDTO> GetByAsync(Expression<Func<DetainedLicense, bool>> predicate)
    {
        var detainedLicense = await _getRepository.GetAsync(predicate);
        var detainedLicenseDTO = _mapper.Map<DetainedLicenseDTO>(detainedLicense);

        return detainedLicenseDTO;
    }
}