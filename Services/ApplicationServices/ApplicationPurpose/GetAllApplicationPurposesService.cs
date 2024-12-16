using System.Linq.Expressions;
using AutoMapper;
using IRepository;
using IServices.IApplicationServices.Purpose;
using ModelDTO.ApplicationDTOs.Purpose;
using Models.ApplicationModels;

namespace Services.ApplicationServices.Purpose;

public class GetAllApplicationPurposesService : IGetAllApplicationPurpose
{
    private readonly IGetAllRepository<ApplicationPurpose> _getAllApplicationPurposesRepository;
    private readonly IMapper _mapper;

    public GetAllApplicationPurposesService(IGetAllRepository<ApplicationPurpose> getAllApplicationPurposesRepository,
        IMapper mapper)
    {
        _getAllApplicationPurposesRepository = getAllApplicationPurposesRepository;
        _mapper = mapper;
    }


    public async Task<List<ApplicationPurposeDTO>> GetAllAsync()
    {
        var applicationTypes = await _getAllApplicationPurposesRepository.GetAllAsync();
        var typeList = applicationTypes.Select(type => _mapper.Map<ApplicationPurposeDTO>(type))
            .ToList();

        return typeList;
    }

    public async Task<IQueryable<ApplicationPurposeDTO>> GetAllAsync(Expression<Func<ApplicationPurpose, bool>> predicate)
    {
        var applicationTypes = await _getAllApplicationPurposesRepository.GetAllAsync(predicate);
        var typeList = applicationTypes.Select(type => _mapper.Map<ApplicationPurposeDTO>(type));

        return typeList;
    }
}