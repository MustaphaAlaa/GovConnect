using AutoMapper;
using IRepository.IGenericRepositories;
using IServices.ILicenseServices;
using Microsoft.Extensions.Logging;
using ModelDTO.LicenseDTOs;
using Models.LicenseModels;

namespace Services.LicensesServices;

public class LocalDrivingLicenseCreatorService : ILocalDrivingLicenseCreationService
{
    private readonly ICreateRepository<LocalDrivingLicense> _createRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<LocalDrivingLicenseCreatorService> _logger;

    public LocalDrivingLicenseCreatorService(ICreateRepository<LocalDrivingLicense> createRepository, IMapper mapper, ILogger<LocalDrivingLicenseCreatorService> logger)
    {
        _createRepository = createRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<LocalDrivingLicenseDTO> CreateAsync(LocalDrivingLicense entity)
    {
        _logger.LogInformation($"{this.GetType().Name} -- CreateAsync method called");
        try
        {
          if (entity == null)
            {
                _logger.LogError("Entity is null");
                throw new ArgumentNullException(nameof(entity));
            }

            var result = await _createRepository.CreateAsync(entity); 
            var localDrivingLicenseDTO = _mapper.Map<LocalDrivingLicenseDTO>(result);
            return localDrivingLicenseDTO;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}