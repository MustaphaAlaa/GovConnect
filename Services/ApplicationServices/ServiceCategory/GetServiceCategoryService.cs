using System.Linq.Expressions;
using AutoMapper;
using IRepository.IGenericRepositories;
using IServices.IApplicationServices.Category;
using Models.ApplicationModels;



namespace Services.ApplicationServices.For;

public class GetServiceCategoryService : IGetServiceCategory
{
    private readonly IGetRepository<ServiceCategory> _getRepository;
    private IMapper _mapper;

    public GetServiceCategoryService(IGetRepository<ServiceCategory> getRepository, IMapper mapper)
    {
        _getRepository = getRepository;
        _mapper = mapper;
    }


    public async Task<ServiceCategory> GetByAsync(Expression<Func<ServiceCategory, bool>> predicate)
    {
        var appFor = await _getRepository.GetAsync(predicate);
        return appFor;

    }
}