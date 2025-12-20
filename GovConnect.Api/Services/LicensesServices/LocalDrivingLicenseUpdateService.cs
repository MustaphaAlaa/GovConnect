using AutoMapper;
using IRepository.IGenericRepositories;
using IServices.IApplicationServices.IServiceCategoryApplications.ILocalDrivingLicenseApplication;
using IServices.ILicenseServices;
using Microsoft.Extensions.Logging;
using ModelDTO.LicenseDTOs;
using Models.LicenseModels;
using Services.Exceptions;

namespace Services.LicensesServices;

public class LocalDrivingLicenseUpdateService : ILocalDrivingLicenseUpdateService
{
    private readonly ILocalLicenseRetrieveService _localLicenseRetrieveService;
    private readonly IUpdateRepository<LocalDrivingLicense> _updateRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<LocalDrivingLicenseUpdateService> _logger;

    public LocalDrivingLicenseUpdateService(ILocalLicenseRetrieveService localLicenseRetrieveService,
     IUpdateRepository<LocalDrivingLicense> updateRepository,
      IMapper mapper,
       ILogger<LocalDrivingLicenseUpdateService> logger)
    {
        _localLicenseRetrieveService = localLicenseRetrieveService;
        _updateRepository = updateRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<LocalDrivingLicenseDTO> UpdateAsync(LocalDrivingLicense updateRequest)
    {
        _logger.LogInformation($"{this.GetType().Name} UpdateAsync");
        try
        {
            var localDrivingLicense = await _localLicenseRetrieveService.GetByAsync(ldl => ldl.LocalDrivingLicenseId == updateRequest.LocalDrivingLicenseId);
            if (localDrivingLicense == null)
            {
                _logger.LogError($"{this.GetType().Name} UpdateAsync - Not Found");
                throw new DoesNotExistException("Not Found");
            }

            var updatedLocalDrivingLicense = _mapper.Map<LocalDrivingLicense>(updateRequest);

            var ldlUpdated = await _updateRepository.UpdateAsync(updatedLocalDrivingLicense);

            var ldlDTO = _mapper.Map<LocalDrivingLicenseDTO>(ldlUpdated);

            return ldlDTO;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{this.GetType().Name} UpdateAsync - Error: {ex.Message}");
            throw;
        }
    }
}