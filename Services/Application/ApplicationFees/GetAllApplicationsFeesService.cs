using AutoMapper;
using IRepository;
using IServices.Application.Fees;
using ModelDTO.Application.Fees;
using Models.Applications;
using System.Linq.Expressions;


namespace Services.Application.Fees;

public class GetAllApplicationsFeesService : IGetAllApplicationFees
{

    private readonly IGetAllRepository<ApplicationFees> _getAllRepository;
    private readonly IMapper _mapper;

    public GetAllApplicationsFeesService(IGetAllRepository<ApplicationFees> getAllRepository, IMapper mapper)
    {
        _getAllRepository = getAllRepository;
        _mapper = mapper;
    }

    public async Task<List<ApplicationFeesDTO>> GetAllAsync()
    {
        List<ApplicationFees> applicationFeesList = await _getAllRepository.GetAllAsync();

        var DTOs = applicationFeesList
             .Select(app => _mapper.Map<ApplicationFeesDTO>(app))
             .ToList();
        return DTOs;
    }

    public async Task<IQueryable<ApplicationFeesDTO>> GetAllAsync(Expression<Func<ApplicationFees, bool>> predicate)
    {
        IQueryable<ApplicationFees> applicationsFees = await _getAllRepository.GetAllAsync(predicate);

        IQueryable<ApplicationFeesDTO> applicationFeesDTOs = applicationsFees
                                     .Select(app => _mapper.Map<ApplicationFeesDTO>(app))
                                     .AsQueryable();

        return applicationFeesDTOs;
    }
}
