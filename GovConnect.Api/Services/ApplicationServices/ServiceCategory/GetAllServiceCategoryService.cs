using System.Linq.Expressions;
using AutoMapper;
using IRepository.IGenericRepositories;
using IServices.IApplicationServices.Category;
using ModelDTO.ApplicationDTOs.Category;
using Models.ApplicationModels;

namespace Services.ApplicationServices.For;

public class GetAllServiceCategoryService : IGetAllServiceCategory
{
    private readonly IGetAllRepository<ServiceCategory> _getAllRepository;
    private readonly IMapper _mapper;

    public GetAllServiceCategoryService(IGetAllRepository<ServiceCategory> getAllRepository,
        IMapper mapper)
    {
        _getAllRepository = getAllRepository;
        _mapper = mapper;
    }


    public async Task<List<ServiceCategoryDTO>> GetAllAsync()
    {
        var applicationsFor = await _getAllRepository.GetAllAsync();
        var typeList = applicationsFor.Select(appfor => _mapper.Map<ServiceCategoryDTO>(appfor))
            .ToList();

        return typeList;
    }

    public async Task<IQueryable<ServiceCategoryDTO>> GetAllAsync(Expression<Func<ServiceCategory, bool>> predicate)
    {
        var applicationTypes = await _getAllRepository.GetAllAsync(predicate);
        var typeList = applicationTypes.Select(type => _mapper.Map<ServiceCategoryDTO>(type));

        return typeList;
    }
}