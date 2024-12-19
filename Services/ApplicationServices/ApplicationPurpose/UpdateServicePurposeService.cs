using AutoMapper;
using IRepository;
using IServices.IApplicationServices.Purpose; 
using ModelDTO.ApplicationDTOs.Purpose;
using Models.ApplicationModels;

namespace Services.ApplicationServices.Purpose;

public class UpdateServicePurposeService : IUpdateServicePurpose
{
    private readonly IUpdateRepository<ServicePurpose> _updateRepository;
    private readonly IGetRepository<ServicePurpose> _getRepository;
    private readonly IMapper _mapper;

    public UpdateServicePurposeService(IUpdateRepository<ServicePurpose> updateRepository, IMapper mapper,
        IGetRepository<ServicePurpose> getRepository)
    {
        _updateRepository = updateRepository;
        _mapper = mapper;
        _getRepository = getRepository;
    }

    public async Task<ServicePurposeDTO> UpdateAsync(ServicePurposeDTO updateRequest)
    {
        if (updateRequest == null)
            throw new ArgumentNullException(nameof(updateRequest));

        if (String.IsNullOrWhiteSpace(updateRequest.Purpose))
            throw new ArgumentException($"Purpose cannot be null or empty", nameof(updateRequest.Purpose));

        if (updateRequest.ServicePurposeId <= 0)
            throw new ArgumentException($"InternationalDrivingLicenseId must be greater than 0");

        var type = await _getRepository.GetAsync(type => type.ServicePurposeId == updateRequest.ServicePurposeId);
        if (type == null)
            throw new Exception($"ServicePurpose With ID {updateRequest.ServicePurposeId} Doesn't exist");

        type.Purpose = updateRequest.Purpose;

        ServicePurpose updatedPurpose = await _updateRepository.UpdateAsync(type);

        if (updatedPurpose == null)
            throw new Exception($"Failed to update");

        var updatedDTO = _mapper.Map<ServicePurposeDTO>(updatedPurpose);

        if (updatedDTO == null)
            throw new AutoMapperMappingException($"Mapping from {nameof(ServicePurpose)} to {nameof(ServicePurposeDTO)} failed.");

        return updatedDTO;
    }
}