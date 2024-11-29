using System.Linq.Expressions;
using AutoMapper;
using IRepository;
using IServices.IApplicationServices.Type;
using Models.ApplicationModels;

namespace Services.ApplicationServices.Type;

public class GetApplicationTypeService : IGetApplicationType
{

    private readonly IGetRepository<ApplicationType> _getApplicationType;
    private IMapper _mapper;

    public GetApplicationTypeService(IGetRepository<ApplicationType> getApplicationType, IMapper mapper)
    {
        _getApplicationType = getApplicationType;
        _mapper = mapper;
    }


    public async Task<ApplicationType> GetByAsync(Expression<Func<ApplicationType, bool>> predicate)
    {
        var type = await _getApplicationType.GetAsync(predicate);
        return type;
    }
}