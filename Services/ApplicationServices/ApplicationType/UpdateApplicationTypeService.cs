using AutoMapper;
using IRepository;
using IServices.IApplicationServices.Type;
using ModelDTO.ApplicationDTOs.Type;
using Models.ApplicationModels;

namespace Services.ApplicationServices.Type;

public class UpdateApplicationTypeService : IUpdateApplicationType
{
    private readonly IUpdateRepository<ApplicationType> _updateRepository;
    private readonly IGetRepository<ApplicationType> _getRepository;
    private readonly IMapper _mapper;

    public UpdateApplicationTypeService(IUpdateRepository<ApplicationType> updateRepository, IMapper mapper,
        IGetRepository<ApplicationType> getRepository)
    {
        _updateRepository = updateRepository;
        _mapper = mapper;
        _getRepository = getRepository;
    }

    public async Task<ApplicationTypeDTO> UpdateAsync(ApplicationTypeDTO updateRequest)
    {
        if (updateRequest == null)
            throw new ArgumentNullException(nameof(updateRequest));

        if (String.IsNullOrWhiteSpace(updateRequest.Type))
            throw new ArgumentException($"Type cannot be null or empty", nameof(updateRequest.Type));

        if (updateRequest.Id <= 0)
            throw new ArgumentException($"Id must be greater than 0");

        var type = await _getRepository.GetAsync(type => type.Id == updateRequest.Id);
        if (type == null)
            throw new Exception($"ApplicationType With ID {updateRequest.Id} Doesn't exist");

        type.Type = updateRequest.Type;

        ApplicationType updatedType = await _updateRepository.UpdateAsync(type);

        if (updatedType == null)
            throw new Exception($"Failed to update");

        var updatedDTO = _mapper.Map<ApplicationTypeDTO>(updatedType);

        if (updatedDTO == null)
            throw new AutoMapperMappingException($"Mapping from {nameof(ApplicationType)} to {nameof(ApplicationTypeDTO)} failed.");

        return updatedDTO;
    }
}