using AutoMapper;
using IRepository.IGenericRepositories;
using IServices.IApplicationServices.Category;
using ModelDTO.ApplicationDTOs.Category;
using Models.ApplicationModels;


namespace Services.ApplicationServices.For;

public class CreateServiceCategoryService : ICreateServiceCategory
{
    private readonly ICreateRepository<ServiceCategory> _createRepository;
    private readonly IGetRepository<ServiceCategory> _getRepository;
    private readonly IMapper _mapper;

    public CreateServiceCategoryService(ICreateRepository<ServiceCategory> createRepository,
        IGetRepository<ServiceCategory> getRepository, IMapper mapper)
    {
        _createRepository = createRepository;
        _getRepository = getRepository;
        _mapper = mapper;
    }


    public async Task<ServiceCategoryDTO> CreateAsync(CreateServiceCategoryRequest entity)
    {
        if (entity == null)
            throw new ArgumentNullException($" {typeof(CreateServiceCategoryRequest).Name} is Null");

        if (string.IsNullOrEmpty(entity.Category))
            throw new ArgumentException($"DrivingLicenseApplication for cannot by null.");

        entity.Category = entity.Category?.Trim().ToLower();

        var appfor = await _getRepository.GetAsync(at => at.Category == entity.Category);

        if (appfor != null)
            throw new InvalidOperationException("This DrivingLicenseApplication for is already exist, cant duplicate types.");

        var reqDtoToModel = _mapper.Map<ServiceCategory>(entity);

        if (reqDtoToModel == null)
            throw new AutoMapperMappingException($"failure mapping from '{entity.GetType().Name}' To 'ServiceCategory'");

        var applicationType =
            await _createRepository.CreateAsync(reqDtoToModel);

        var dto = _mapper.Map<ServiceCategoryDTO>(applicationType);

        if (dto == null)
            throw new AutoMapperMappingException($"failure mapping from '{entity.GetType().Name}' To 'ServiceCategory'");


        return dto;
    }
}