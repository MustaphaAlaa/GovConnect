using AutoMapper;
using IRepository;
using IServices.IApplicationServices.Fees;
using ModelDTO.ApplicationDTOs.Fees;
using Models.ApplicationModels;


namespace Services.ApplicationServices.Fees;

public class CreateServiceFeesService : ICreateServiceFees
{
    private readonly IMapper _mapper; //
    private readonly IGetRepository<ServiceFees> _getRepository;
    private readonly ICreateRepository<ServiceFees> _createRepository;

    public CreateServiceFeesService(IMapper mapper,
        IGetRepository<ServiceFees> getRepository,
        ICreateRepository<ServiceFees> createRepository)
    {
        _mapper = mapper;
        _getRepository = getRepository;
        _createRepository = createRepository;
    }

    public async Task<ServiceFeesDTO> CreateAsync(ServiceFeesDTO entity)
    {
        if (entity == null)
            throw new ArgumentNullException($"ServiceFeesDTo is null");

        if (entity.ApplicationPuropseId <= 0)
            throw new ArgumentOutOfRangeException("Purpose id must be greater than 0");

        if (entity.ServiceCategoryId <= 0)
            throw new ArgumentOutOfRangeException("Category id must be greater than 0");

        if (entity.Fees < 0)
            throw new ArgumentOutOfRangeException("Category id must be greater than 0");


        var applicationFees = await _getRepository.GetAsync(appFees =>
              appFees.ApplicationTypeId == entity.ApplicationPuropseId && appFees.ServiceCategoryId == entity.ApplicationPuropseId);

        if (applicationFees != null)
            throw new Exception("ServiceFees is already exist you cannot recreate it only update it");

        ServiceFees createReq = _mapper.Map<ServiceFees>(entity)
             ?? throw new AutoMapperMappingException();



        var createdAppFees = await _createRepository.CreateAsync(createReq);

        if (createdAppFees is null)
            throw new Exception();

        var AppFeesDTO = _mapper.Map<ServiceFeesDTO>(createdAppFees);

        if (AppFeesDTO is null)
            throw new AutoMapperMappingException();

        return AppFeesDTO;
    }
}
