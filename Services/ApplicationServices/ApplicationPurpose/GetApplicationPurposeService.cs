using System.Linq.Expressions;
using AutoMapper;
using IRepository;
using IServices.IApplicationServices.Purpose;
using Models.ApplicationModels;

namespace Services.ApplicationServices.Purpose;

public class GetApplicationPurposeService : IGetApplicationPurpose
{

    private readonly IGetRepository<ApplicationPurpose> _getApplicationType;
    private IMapper _mapper;

    public GetApplicationPurposeService(IGetRepository<ApplicationPurpose> getApplicationType, IMapper mapper)
    {
        _getApplicationType = getApplicationType;
        _mapper = mapper;
    }


    public async Task<ApplicationPurpose> GetByAsync(Expression<Func<ApplicationPurpose, bool>> predicate)
    {
        var type = await _getApplicationType.GetAsync(predicate);
        return type;
    }
}