using AutoMapper;
using IRepository;
using IServices.Application.Type;
using ModelDTO.Application.Type;
using Models.Applications;
using Models.Users;

namespace Services.Application.Type;

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

        if (String.IsNullOrEmpty(updateRequest.Type))
            throw new ArgumentException($"{nameof(updateRequest.Type)} cannot be null or empty");

        if (updateRequest.Id <= 0)
            throw new ArgumentException($"{nameof(updateRequest)}.{nameof(updateRequest.Type)} must be greater than 0");

        var type = await _getRepository.GetAsync(type => type.Id == updateRequest.Id);

        if (type == null)
            throw new ArgumentException($"{nameof(updateRequest.Type)} Doesn't exist");

        type.Type = updateRequest.Type;

        ApplicationType updatedType = await _updateRepository.UpdateAsync(type);

        ApplicationTypeDTO updatedDto = _mapper.Map<ApplicationTypeDTO>(updatedType);
    
        return updatedDto;
    }
}