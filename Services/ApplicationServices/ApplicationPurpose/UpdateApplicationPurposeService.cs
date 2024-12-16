using AutoMapper;
using IRepository;
using IServices.IApplicationServices.Purpose; 
using ModelDTO.ApplicationDTOs.Purpose;
using Models.ApplicationModels;

namespace Services.ApplicationServices.Purpose;

public class UpdateApplicationPurposeService : IUpdateApplicationPurpose
{
    private readonly IUpdateRepository<ApplicationPurpose> _updateRepository;
    private readonly IGetRepository<ApplicationPurpose> _getRepository;
    private readonly IMapper _mapper;

    public UpdateApplicationPurposeService(IUpdateRepository<ApplicationPurpose> updateRepository, IMapper mapper,
        IGetRepository<ApplicationPurpose> getRepository)
    {
        _updateRepository = updateRepository;
        _mapper = mapper;
        _getRepository = getRepository;
    }

    public async Task<ApplicationPurposeDTO> UpdateAsync(ApplicationPurposeDTO updateRequest)
    {
        if (updateRequest == null)
            throw new ArgumentNullException(nameof(updateRequest));

        if (String.IsNullOrWhiteSpace(updateRequest.Purpose))
            throw new ArgumentException($"Purpose cannot be null or empty", nameof(updateRequest.Purpose));

        if (updateRequest.ApplicationPurposeId <= 0)
            throw new ArgumentException($"InternationalDrivingLicenseId must be greater than 0");

        var type = await _getRepository.GetAsync(type => type.ApplicationPurposeId == updateRequest.ApplicationPurposeId);
        if (type == null)
            throw new Exception($"ApplicationPurpose With ID {updateRequest.ApplicationPurposeId} Doesn't exist");

        type.Purpose = updateRequest.Purpose;

        ApplicationPurpose updatedPurpose = await _updateRepository.UpdateAsync(type);

        if (updatedPurpose == null)
            throw new Exception($"Failed to update");

        var updatedDTO = _mapper.Map<ApplicationPurposeDTO>(updatedPurpose);

        if (updatedDTO == null)
            throw new AutoMapperMappingException($"Mapping from {nameof(ApplicationPurpose)} to {nameof(ApplicationPurposeDTO)} failed.");

        return updatedDTO;
    }
}