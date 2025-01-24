using AutoMapper;
using IRepository.IGenericRepositories;
using IServices.IApplicationServices.IServiceCategoryApplications.IRetakeTestApplication;
using Microsoft.Extensions.Logging;
using Models.Applications;
using System.Linq.Expressions;

namespace Services.ApplicationServices.ServiceCategoryApplications;

/// <summary>
/// Responsible for retrieving data from RetakeTestApplication table.
/// </summary>
public class RetakeTestApplicationRetriever : IRetakeTestApplicationRetriever
{
    private readonly IGetRepository<RetakeTestApplication> _getRepository;
    private readonly ILogger<RetakeTestApplicationRetriever> _logger;
    private readonly IMapper _mapper;

    public RetakeTestApplicationRetriever(IGetRepository<RetakeTestApplication> getRepository, ILogger<RetakeTestApplicationRetriever> logger, IMapper mapper)
    {
        _getRepository = getRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<RetakeTestApplication> GetByAsync(Expression<Func<RetakeTestApplication, bool>> predicate)
    {
        _logger.LogInformation($"{this.GetType().Name} -- GetByAsync -- RetakeTestApplication");
        var retakeTestApplication = await _getRepository.GetAsync(predicate);
        return retakeTestApplication;
    }
}