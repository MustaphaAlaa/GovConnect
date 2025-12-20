using ModelDTO.Appointments;
using Models;

namespace IServices.ITimeIntervalService;

public interface IGetTimeIntervalService : IAsyncRetrieveService<TimeInterval,TimeIntervalDTO>
{
    
}