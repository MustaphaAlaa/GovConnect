using System.Linq.Expressions;
using AutoMapper;
using IRepository;
using IServices.IApplicationServices.Category;
using Models.ApplicationModels;



namespace Services.ApplicationServices.For;

public class IGetServiceCategoryService : IGetServiceCategory
{
    private readonly IGetRepository<ServiceCategory> _getRepository;
    private IMapper _mapper;

    public IGetServiceCategoryService(IGetRepository<ServiceCategory> getRepository, IMapper mapper)
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