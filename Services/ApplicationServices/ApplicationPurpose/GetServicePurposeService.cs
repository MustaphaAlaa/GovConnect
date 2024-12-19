using System.Linq.Expressions;
using AutoMapper;
using IRepository;
using IServices.IApplicationServices.Purpose;
using Models.ApplicationModels;

namespace Services.ApplicationServices.Purpose;

public class GetServicePurposeService : IGetServicePurpose
{

    private readonly IGetRepository<ServicePurpose> _getApplicationType;
    private IMapper _mapper;

    public GetServicePurposeService(IGetRepository<ServicePurpose> getApplicationType, IMapper mapper)
    {
        _getApplicationType = getApplicationType;
        _mapper = mapper;
    }


    public async Task<ServicePurpose> GetByAsync(Expression<Func<ServicePurpose, bool>> predicate)
    {
        var type = await _getApplicationType.GetAsync(predicate);
        return type;
    }
}