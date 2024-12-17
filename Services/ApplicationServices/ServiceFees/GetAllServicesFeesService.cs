using AutoMapper;
using IRepository;
using IServices.IApplicationServices.Fees;
using ModelDTO.ApplicationDTOs.Fees;
using Models.ApplicationModels;
using System.Linq.Expressions;


namespace Services.ApplicationServices.Fees;

public class GetAllServicesFeesService : IGetAllServiceFees
{

    private readonly IGetAllRepository<ServiceFees> _getAllRepository;
    private readonly IMapper _mapper;

    public GetAllServicesFeesService(IGetAllRepository<ServiceFees> getAllRepository, IMapper mapper)
    {
        _getAllRepository = getAllRepository;
        _mapper = mapper;
    }

    public async Task<List<ServiceFeesDTO>> GetAllAsync()
    {
        List<ServiceFees> applicationFeesList = await _getAllRepository.GetAllAsync();

        var DTOs = applicationFeesList
             .Select(app => _mapper.Map<ServiceFeesDTO>(app))
             .ToList();
        return DTOs;
    }

    public async Task<IQueryable<ServiceFeesDTO>> GetAllAsync(Expression<Func<ServiceFees, bool>> predicate)
    {
        IQueryable<ServiceFees> applicationsFees = await _getAllRepository.GetAllAsync(predicate);

        IQueryable<ServiceFeesDTO> applicationFeesDTOs = applicationsFees
                                     .Select(app => _mapper.Map<ServiceFeesDTO>(app))
                                     .AsQueryable();

        return applicationFeesDTOs;
    }
}
