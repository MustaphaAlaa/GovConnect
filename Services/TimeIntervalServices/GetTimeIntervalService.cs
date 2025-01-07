using System.Linq.Expressions;
using AutoMapper;
using IRepository;
using IServices.ITimeIntervalService;
using Microsoft.Extensions.Logging;
using ModelDTO.Appointments;
using Models;

namespace Services.TimeIntervalServices;

public class GetTimeIntervalService : IGetTimeIntervalService
{
    private readonly IGetRepository<TimeInterval> _getRepository;
    private readonly ILogger<TimeInterval> _logger;
    private readonly IMapper _mapper;

    public GetTimeIntervalService(IGetRepository<TimeInterval> getRepository,
        ILogger<TimeInterval> logger,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<TimeIntervalDTO> GetByAsync(Expression<Func<TimeInterval, bool>> predicate)
    {
        _logger.LogInformation($"{this.GetType().Name} ---- GetAsync");

       var timeInterval =  await _getRepository.GetAsync(predicate);
       
       var timeIntervalDTO = timeInterval != null ? _mapper.Map<TimeIntervalDTO>(timeInterval) : null;
      
       return timeIntervalDTO; 
    }
}