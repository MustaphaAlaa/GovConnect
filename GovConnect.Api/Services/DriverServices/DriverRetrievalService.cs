using System.Linq.Expressions;
using AutoMapper;
using IRepository.IGenericRepositories;
using IServices.IDriverServices;
using Microsoft.Extensions.Logging;
using ModelDTO.Users;
using Models.Users;

namespace Services.DriverServices;

public class DriverRetrievalService(
    IGetRepository<Driver> getDriverRepository,
    ILogger<DriverRetrievalService> logger,
    IMapper mapper)
    : IDriverRetrieveService
{
    public async Task<DriverDTO> GetByAsync(Expression<Func<Driver, bool>> predicate)
    {
        logger.LogDebug("GetByAsync");
        var drive = await getDriverRepository.GetAsync(predicate);
        var driverDTO = mapper.Map<DriverDTO>(drive);
        return driverDTO;
    }
}