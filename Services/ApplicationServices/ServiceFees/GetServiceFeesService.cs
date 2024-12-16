using AutoMapper;
using IRepository;
using IServices.IApplicationServices.Fees;
using Models.ApplicationModels;

using System.Linq.Expressions;

namespace Services.ApplicationServices.Fees;

public class GetServiceFeesService : IGetApplicationFees
{
    private readonly IGetRepository<ServiceFees> _getRepository;
    private readonly IMapper _mapper;

    public GetServiceFeesService(IGetRepository<ServiceFees> getRepository, IMapper mapper)
    {
        _getRepository = getRepository;
        _mapper = mapper;
    }

    public async Task<ServiceFees> GetByAsync(Expression<Func<ServiceFees, bool>> predicate)
    {
        var appFees = await _getRepository.GetAsync(predicate);
        return appFees;
    }
}
