using AutoMapper;
using IRepository;
using IServices.IApplicationServices.Fees;

using ModelDTO.ApplicationDTOs.Fees;
using Models.ApplicationModels;

using System.Linq.Expressions;

namespace Services.ApplicationServices.Fees;

public class GetApplicationFeesService : IGetApplicationFees
{
    private readonly IGetRepository<ApplicationFees> _getRepository;
    private readonly IMapper _mapper;

    public GetApplicationFeesService(IGetRepository<ApplicationFees> getRepository, IMapper mapper)
    {
        _getRepository = getRepository;
        _mapper = mapper;
    }

    public async Task<ApplicationFees> GetByAsync(Expression<Func<ApplicationFees, bool>> predicate)
    {
        var appFees = await _getRepository.GetAsync(predicate);
        return appFees;
    }
}
