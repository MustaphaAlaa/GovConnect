using AutoMapper;
using IRepository;
using IServices.Application.For;
using ModelDTO.Application.For;
using Models.Applications;
 
namespace Services.Application.For;

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

        ApplicationForDTO updatedDto = _mapper.Map<ApplicationForDTO>(updatedFor);
    
        return updatedDto;
    }
}