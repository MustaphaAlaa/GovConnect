using ModelDTO.Appointments;
using Models;
using System.Linq.Expressions;

namespace IServices.ITimeIntervalService;

public interface IGetAllTimeIntervalService : IGetAllService<TimeInterval, TimeIntervalDTO>
{
    public Task<Dictionary<int, List<TimeIntervalDTO>>> GetTimeIntervalsDictionaryAsync(Expression<Func<TimeInterval, bool>> predict);
}