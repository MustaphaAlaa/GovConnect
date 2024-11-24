using AutoMapper;
using IRepository;
using IServices.Application.Type;
using ModelDTO.Application.Type;
using Models.ApplicationModels;


namespace Services.Application.Type;

public class CreateApplicationTypeService : ICreateApplicationType
{
    private readonly ICreateRepository<ApplicationType> _createApplicationTypeRepository;
    private readonly IGetRepository<ApplicationType> _getApplicationTypeRepository;
    private readonly IMapper _mapper;

    public CreateApplicationTypeService(ICreateRepository<ApplicationType> createApplicationTypeRepository,
        IGetRepository<ApplicationType> getApplicationTypeRepository, IMapper mapper)
    {
        _createApplicationTypeRepository = createApplicationTypeRepository;
        _getApplicationTypeRepository = getApplicationTypeRepository;
        _mapper = mapper;
    }


    public async Task<ApplicationTypeDTO> CreateAsync(CreateApplicationTypeRequest entity)
    {
        if (entity == null)
            throw new ArgumentNullException($" {typeof(CreateApplicationTypeRequest)} is Null");

        if (string.IsNullOrEmpty(entity.Type))
            throw new ArgumentException($"Application Type cannot by null.");


        entity.Type = entity.Type?.Trim().ToLower();


        var type = await _getApplicationTypeRepository.GetAsync(at => at.Type == entity.Type);

        if (type != null)
            throw new InvalidOperationException("This application type is already exist, cant duplicate types.");


        var reqDtoToModel = _mapper.Map<ApplicationType>(entity);

        if (reqDtoToModel == null)
            throw new AutoMapperMappingException($"failure mapping from '{entity.GetType().Name}' To 'ApplicationType'");


        var applicationType =
            await _createApplicationTypeRepository.CreateAsync(reqDtoToModel);

        var dto = _mapper.Map<ApplicationTypeDTO>(applicationType);

        if (dto == null)
            throw new AutoMapperMappingException($"failure mapping from '{reqDtoToModel.GetType().Name}' To 'ApplicationTypeDTO'");

        return dto;

    }
}