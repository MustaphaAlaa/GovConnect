using System.Linq.Expressions;
using AutoMapper;
using IRepository;
using IServices.Application.Type;
using IServices.Country;
using ModelDTO.Application.Type;
using Models.Applications;

namespace Services.Application.Type;

public class GetAllApplicationTypesService : IGetAllApplicationTypes
{
    private readonly IGetAllRepository<ApplicationType> _getAllApplicationTyperepository;
    private readonly IMapper _mapper;

    public GetAllApplicationTypesService(IGetAllRepository<ApplicationType> getAllApplicationTyperepository,
        IMapper mapper)
    {
        _getAllApplicationTyperepository = getAllApplicationTyperepository;
        _mapper = mapper;
    }


    public async Task<List<ApplicationTypeDTO>> GetAllAsync()
    {
        var applicationTypes = await _getAllApplicationTyperepository.GetAllAsync();
        var typeList = applicationTypes.Select(type => _mapper.Map<ApplicationTypeDTO>(type))
            .ToList();

        return typeList;
    }

    public async Task<IQueryable<ApplicationTypeDTO>> GetAllAsync(Expression<Func<ApplicationType, bool>> predicate)
    {
        var applicationTypes = await _getAllApplicationTyperepository.GetAllAsync(predicate);
        var typeList = applicationTypes.Select(type => _mapper.Map<ApplicationTypeDTO>(type));

        return typeList;
    }
}