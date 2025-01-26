using AutoMapper;
using IRepository.IGenericRepositories;
using IServices.ILicenseServices;
using Microsoft.Extensions.Logging;
using ModelDTO.LicenseDTOs;
using Models.LicenseModels;

namespace Services.LicensesServices;

/// <summary>
///  Service responsible for Creating Local Driving License. 
/// </summary> 
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

    /// <summary>
    /// Create Local Driving License Method
    /// </summary>
    /// <param name="entity">The object of <see cref="LocalDrivingLicense"/> that contains the data to be created.</param>
    /// <returns>A <see cref="LocalDrivingLicenseDTO"/> object representing the created local driving license.</returns>
    public async Task<LocalDrivingLicenseDTO> CreateAsync(LocalDrivingLicense entity)
    {

        _logger.LogInformation($"{nameof(CreateAsync)} method in {this.GetType().Name} called.");
        try
        {
            if (entity == null)
            {
                _logger.LogError("Entity cannot be null. A valid LocalDrivingLicense object is required.");
                throw new ArgumentNullException(nameof(entity));
            }

            var result = await _createRepository.CreateAsync(entity);
            var localDrivingLicenseDTO = _mapper.Map<LocalDrivingLicenseDTO>(result);
            return localDrivingLicenseDTO;
        }
        catch (Exception ex)
        {
           _logger.LogError(ex, $"An error occurred while creating the local driving license: {ex.Message}");
            throw;
        }
    }
}