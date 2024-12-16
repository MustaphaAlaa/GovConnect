using AutoMapper;
using IRepository;
using IServices.IApplicationServices.Purpose;
using ModelDTO.ApplicationDTOs.Purpose;
using Models.ApplicationModels;


namespace Services.ApplicationServices.Purpose;

public class CreateApplicationPurposeService : ICreateApplicationPurpose
{
    private readonly ICreateRepository<ApplicationPurpose> _createApplicationPurposeRepository;
    private readonly IGetRepository<ApplicationPurpose> _getApplicationPurposeRepository;
    private readonly IMapper _mapper;

    public CreateApplicationPurposeService(ICreateRepository<ApplicationPurpose> createApplicationPurposeRepository,
        IGetRepository<ApplicationPurpose> getApplicationPurposeRepository, IMapper mapper)
    {
        _createApplicationPurposeRepository = createApplicationPurposeRepository;
        _getApplicationPurposeRepository = getApplicationPurposeRepository;
        _mapper = mapper;
    }


    public async Task<ApplicationPurposeDTO> CreateAsync(CreateApplicationPurposeRequest entity)
    {
        if (entity == null)
            throw new ArgumentNullException($" {typeof(CreateApplicationPurposeRequest)} is Null");

        if (string.IsNullOrEmpty(entity.Purpose))
            throw new ArgumentException($"DrivingLicenseApplication Purpose cannot by null.");


        entity.Purpose = entity.Purpose?.Trim().ToLower();


        var type = await _getApplicationPurposeRepository.GetAsync(at => at.Purpose == entity.Purpose);

        if (type != null)
            throw new InvalidOperationException("This DrivingLicenseApplication type is already exist, cant duplicate types.");


        var reqDtoToModel = _mapper.Map<ApplicationPurpose>(entity);

        if (reqDtoToModel == null)
            throw new AutoMapperMappingException($"failure mapping from '{entity.GetType().Name}' To 'ApplicationPurpose'");


        var applicationType =
            await _createApplicationPurposeRepository.CreateAsync(reqDtoToModel);

        var dto = _mapper.Map<ApplicationPurposeDTO>(applicationType);

        if (dto == null)
            throw new AutoMapperMappingException($"failure mapping from '{reqDtoToModel.GetType().Name}' To 'ApplicationPurposeDTO'");

        return dto;

    }
}