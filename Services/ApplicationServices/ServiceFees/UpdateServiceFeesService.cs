using AutoMapper;
using IRepository;
using IServices.IApplicationServices.Fees;
using Microsoft.Extensions.Logging;
using ModelDTO.ApplicationDTOs.Fees;
using Models.ApplicationModels;

namespace Services.ApplicationServices.Fees
{
    public class UpdateServiceFeesService : IUpdateServiceFees
    {
        private readonly IMapper _mapper;
        private readonly IUpdateRepository<ServiceFees> _updateRepository;
        private readonly IGetRepository<ServiceFees> _getRepository;
        private readonly ILogger<UpdateServiceFeesService> _logger;

        public UpdateServiceFeesService(IMapper mapper,
                        IUpdateRepository<ServiceFees> updateRepository,
                        ILogger<UpdateServiceFeesService> logger,
                        IGetRepository<ServiceFees> getRepository)
        {
            _mapper = mapper;
            _updateRepository = updateRepository;
            _logger = logger;
            _getRepository = getRepository;
        }

        public async Task<ServiceFeesDTO> UpdateAsync(ServiceFeesDTO updateRequest)
        {
            try
            {
                if (updateRequest == null)
                {
                    _logger.LogError("Update request is null.");
                    throw new ArgumentNullException($"Cannot update null request.");
                }
                if (updateRequest.ServicePurposeId <= 0)
                {
                    _logger.LogError("Invalid ApplicationPurposeId: {ServicePurposeId}", updateRequest.ServicePurposeId);
                    throw new ArgumentOutOfRangeException("ApplicationPurposeId id must be greater than 0");
                }
                if (updateRequest.ServiceCategoryId <= 0)
                {
                    _logger.LogError("Invalid ServiceCategoryId: {ServiceCategoryId}", updateRequest.ServiceCategoryId);
                    throw new ArgumentOutOfRangeException("Category id must be greater than 0");
                }

                var applicationFees = await _getRepository.GetAsync(appFees =>
                      appFees.ServicePurposeId == updateRequest.ServicePurposeId
                      && appFees.ServiceCategoryId == updateRequest.ServicePurposeId);

                if (applicationFees == null)
                {
                    _logger.LogError("ServiceFees not found for ApplicationPurposeId: {ServicePurposeId} and ServiceCategoryId: {ServiceCategoryId}", updateRequest.ServicePurposeId, updateRequest.ServiceCategoryId);
                    throw new Exception("ServiceFees doesn't exist.");
                }
                if (updateRequest.LastUpdate < applicationFees.LastUpdate)
                {
                    _logger.LogError("Invalid LastUpdate: {LastUpdate}. It is earlier than the existing LastUpdate: {ExistingLastUpdate}", updateRequest.LastUpdate, applicationFees.LastUpdate);
                    throw new ArgumentOutOfRangeException("Invalid Date Range");
                }

                ServiceFees toUpdateObject = _mapper.Map<ServiceFees>(updateRequest)
                  ?? throw new AutoMapperMappingException();

                ServiceFees updatedObject = await _updateRepository.UpdateAsync(toUpdateObject);

                if (updatedObject is null)
                    throw new Exception("Does not updated");

                ServiceFeesDTO serviceFeesDto = _mapper.Map<ServiceFeesDTO>(updatedObject)
                        ?? throw new AutoMapperMappingException();

                _logger.LogInformation("ServiceFees updated successfully for ApplicationPurposeId: {ServicePurposeId} and ServiceCategoryId: {ServiceCategoryId}", updateRequest.ServicePurposeId, updateRequest.ServiceCategoryId);

                return serviceFeesDto;
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "ArgumentNullException occurred while updating ServiceFees.");
                throw;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _logger.LogError(ex, "ArgumentOutOfRangeException occurred while updating ServiceFees.");
                throw;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, "AutoMapperMappingException occurred while updating ServiceFees.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating ServiceFees.");
                throw;
            }
        }
    }
}
