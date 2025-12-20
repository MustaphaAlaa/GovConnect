using AutoMapper;
using IRepository.IGenericRepositories;
using IServices.IApplicationServices.IPurpose;
using ModelDTO.ApplicationDTOs.Purpose;
using Models.ApplicationModels;


namespace Services.ApplicationServices.Purpose;

public class CreateServicePurposeService : ICreateServicePurpose
{
    private readonly ICreateRepository<ServicePurpose> _createApplicationPurposeRepository;
    private readonly IGetRepository<ServicePurpose> _getApplicationPurposeRepository;
    private readonly IMapper _mapper;

    public CreateServicePurposeService(ICreateRepository<ServicePurpose> createApplicationPurposeRepository,
        IGetRepository<ServicePurpose> getApplicationPurposeRepository, IMapper mapper)
    {
        _createApplicationPurposeRepository = createApplicationPurposeRepository;
        _getApplicationPurposeRepository = getApplicationPurposeRepository;
        _mapper = mapper;
    }


    public async Task<ServicePurposeDTO> CreateAsync(CreateServicePurposeRequest entity)
    {
        if (entity == null)
            throw new ArgumentNullException($" {typeof(CreateServicePurposeRequest)} is Null");

        if (string.IsNullOrEmpty(entity.Purpose))
            throw new ArgumentException($"DrivingLicenseApplication IPurpose cannot by null.");


        entity.Purpose = entity.Purpose?.Trim().ToLower();


        var type = await _getApplicationPurposeRepository.GetAsync(at => at.Purpose == entity.Purpose);

        if (type != null)
            throw new InvalidOperationException("This DrivingLicenseApplication type is already exist, cant duplicate types.");


        var reqDtoToModel = _mapper.Map<ServicePurpose>(entity);

        if (reqDtoToModel == null)
            throw new AutoMapperMappingException($"failure mapping from '{entity.GetType().Name}' To 'ServicePurpose'");


        var applicationType =
            await _createApplicationPurposeRepository.CreateAsync(reqDtoToModel);

        var dto = _mapper.Map<ServicePurposeDTO>(applicationType);

        if (dto == null)
            throw new AutoMapperMappingException($"failure mapping from '{reqDtoToModel.GetType().Name}' To 'ServicePurposeDTO'");

        return dto;

    }
}