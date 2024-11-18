using AutoMapper;
using IRepository;
using IServices.Application.For;
using ModelDTO.Application.For; 
using Models.Applications;


namespace Services.Application.For;

public class CreateApplicationForService : ICreateApplicationFor
{
    private readonly ICreateRepository<ApplicationFor> _createRepository;
    private readonly IGetRepository<ApplicationFor> _getRepository;
    private readonly IMapper _mapper;

    public CreateApplicationForService(ICreateRepository<ApplicationFor> createRepository,
        IGetRepository<ApplicationFor> getRepository, IMapper mapper)
    {
        _createRepository = createRepository;
        _getRepository = getRepository;
        _mapper = mapper;
    }


    public async Task<ApplicationForDTO> CreateAsync(CreateApplicationForRequest entity)
    {
        if (entity == null)
            throw new ArgumentNullException($" {typeof(CreateApplicationForRequest).Name} is Null");

        if (string.IsNullOrEmpty(entity.For))
            throw new ArgumentException($"Application for cannot by null.");
 
        entity.For = entity.For?.Trim().ToLower();
 
        var type = await _getRepository.GetAsync(at => at.For == entity.For);

        if (type != null)
            throw new InvalidOperationException("This application for is already exist, cant duplicate types.");

        var reqDtoToModel = _mapper.Map<ApplicationFor>(entity);

        var applicationType =
            await _createRepository.CreateAsync(reqDtoToModel);

        var dto = _mapper.Map<ApplicationForDTO>(applicationType);

        return dto;
    }
}