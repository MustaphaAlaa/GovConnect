using System.Linq.Expressions;
using AutoMapper;
using IRepository.IGenericRepositories;
using IServices.IApplicationServices.IPurpose;
using ModelDTO.ApplicationDTOs.Purpose;
using Models.ApplicationModels;

namespace Services.ApplicationServices.Purpose;

public class GetAllApplicationPurposesService : IGetAllServicePurpose
{
    private readonly IGetAllRepository<ServicePurpose> _getAllApplicationPurposesRepository;
    private readonly IMapper _mapper;

    public GetAllApplicationPurposesService(IGetAllRepository<ServicePurpose> getAllApplicationPurposesRepository,
        IMapper mapper)
    {
        _getAllApplicationPurposesRepository = getAllApplicationPurposesRepository;
        _mapper = mapper;
    }


    public async Task<List<ServicePurposeDTO>> GetAllAsync()
    {
        var applicationTypes = await _getAllApplicationPurposesRepository.GetAllAsync();
        var typeList = applicationTypes.Select(type => _mapper.Map<ServicePurposeDTO>(type))
            .ToList();

        return typeList;
    }

    public async Task<IQueryable<ServicePurposeDTO>> GetAllAsync(Expression<Func<ServicePurpose, bool>> predicate)
    {
        var applicationTypes = await _getAllApplicationPurposesRepository.GetAllAsync(predicate);
        var typeList = applicationTypes.Select(type => _mapper.Map<ServicePurposeDTO>(type));

        return typeList;
    }
}