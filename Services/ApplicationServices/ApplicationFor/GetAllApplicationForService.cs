using System.Linq.Expressions;
using AutoMapper;
using IRepository;
using IServices.IApplicationServices.For;
using ModelDTO.ApplicationDTOs.For;
using Models.ApplicationModels;

namespace Services.ApplicationServices.For;

public class GetAllApplicationForService : IGetAllApplicationFor
{
    private readonly IGetAllRepository<ApplicationFor> _getAllRepository;
    private readonly IMapper _mapper;

    public GetAllApplicationForService(IGetAllRepository<ApplicationFor> getAllRepository,
        IMapper mapper)
    {
        _getAllRepository = getAllRepository;
        _mapper = mapper;
    }


    public async Task<List<ApplicationForDTO>> GetAllAsync()
    {
        var applicationsFor = await _getAllRepository.GetAllAsync();
        var typeList = applicationsFor.Select(appfor => _mapper.Map<ApplicationForDTO>(appfor))
            .ToList();

        return typeList;
    }

    public async Task<IQueryable<ApplicationForDTO>> GetAllAsync(Expression<Func<ApplicationFor, bool>> predicate)
    {
        var applicationTypes = await _getAllRepository.GetAllAsync(predicate);
        var typeList = applicationTypes.Select(type => _mapper.Map<ApplicationForDTO>(type));

        return typeList;
    }
}