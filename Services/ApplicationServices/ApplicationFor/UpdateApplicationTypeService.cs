using AutoMapper;
using IRepository;
using IServices.IApplicationServices.For;
using ModelDTO.ApplicationDTOs.For;
using Models.ApplicationModels;

namespace Services.ApplicationServices.For;

public class UpdateApplicationForService : IUpdateApplicationFor
{
    private readonly IUpdateRepository<ApplicationFor> _updateRepository;
    private readonly IGetRepository<ApplicationFor> _getRepository;
    private readonly IMapper _mapper;

    public UpdateApplicationForService(IUpdateRepository<ApplicationFor> updateRepository, IMapper mapper,
        IGetRepository<ApplicationFor> getRepository)
    {
        _updateRepository = updateRepository;
        _mapper = mapper;
        _getRepository = getRepository;
    }

    public async Task<ApplicationForDTO> UpdateAsync(ApplicationForDTO updateRequest)
    {
        if (updateRequest == null)
            throw new ArgumentNullException(nameof(updateRequest));

        if (String.IsNullOrEmpty(updateRequest.For))
            throw new ArgumentException($"{nameof(updateRequest.For)} cannot be null or empty");

        if (updateRequest.Id <= 0)
            throw new ArgumentException($"{nameof(updateRequest)}.{nameof(updateRequest.For)} must be greater than 0");

        var applicationFor = await _getRepository.GetAsync(appfor => appfor.Id == updateRequest.Id);

        if (applicationFor == null)
            throw new ArgumentException($"{nameof(updateRequest.For)} Doesn't exist");

        applicationFor.For = updateRequest.For;

        ApplicationFor updatedFor = await _updateRepository.UpdateAsync(applicationFor);

        if (updatedFor == null)
            throw new Exception($"Failed to update");

        ApplicationForDTO updatedDto = _mapper.Map<ApplicationForDTO>(updatedFor);

        if (updatedDto == null)
            throw new AutoMapperMappingException($"Mapping from {nameof(ApplicationFor)} to {nameof(ApplicationForDTO)} failed.");

        return updatedDto;
    }
}