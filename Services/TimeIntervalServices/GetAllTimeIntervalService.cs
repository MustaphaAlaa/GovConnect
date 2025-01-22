using System.Linq.Expressions;
using AutoMapper;
using IRepository.IGenericRepositories;
using IServices.ITimeIntervalService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelDTO.Appointments;
using Models;

namespace Services.TimeIntervalServices;

public class GetAllTimeIntervalService : IGetAllTimeIntervalService
{

    private readonly IGetAllRepository<TimeInterval> _getAllTimeIntervalService;
    private readonly ILogger<TimeInterval> _logger;
    private readonly IMapper _mapper;

    public GetAllTimeIntervalService(IGetAllRepository<TimeInterval> getAllTimeIntervalService,
        ILogger<TimeInterval> logger,
        IMapper mapper)
    {
        _getAllTimeIntervalService = getAllTimeIntervalService;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<List<TimeIntervalDTO>> GetAllAsync()
    {
        _logger.LogInformation($"{this.GetType().Name} ---- GetAllAsync");
        var timeIntervals = await _getAllTimeIntervalService.GetAllAsync();
        var timeIntervalDTOs = timeIntervals != null || timeIntervals.Count == 0 ? timeIntervals.Select(ti => _mapper.Map<TimeIntervalDTO>(ti)).ToList() : null;
        return timeIntervalDTOs;
    }

    public async Task<IQueryable<TimeIntervalDTO>> GetAllAsync(Expression<Func<TimeInterval, bool>> predicate)
    {
        _logger.LogInformation($"{this.GetType().Name} ---- GetAllAsync By Expression");

        var timeIntervals = await _getAllTimeIntervalService.GetAllAsync(predicate);
        var timeIntervalDTOs = timeIntervals != null ? timeIntervals.Select(ti => _mapper.Map<TimeIntervalDTO>(ti)).ToList() : null;
        return timeIntervalDTOs.AsQueryable();

    }

    public async Task<Dictionary<int, List<TimeIntervalDTO>>> GetTimeIntervalsDictionaryAsync(Expression<Func<TimeInterval, bool>> predict)
    {
        _logger.LogInformation($"{this.GetType().Name} ---- GetTimeIntervalsDictionaryAsync");

        IQueryable<TimeInterval> timeIntervals = await _getAllTimeIntervalService.GetAllAsync(predict);


        var timeIntervalsDictionary = timeIntervals.GroupBy(ti => ti.Hour).ToDictionary(ti => (int)ti.Key, ti => ti.Select(t => _mapper.Map<TimeIntervalDTO>(t)).ToList());

        return timeIntervalsDictionary;
    }
}