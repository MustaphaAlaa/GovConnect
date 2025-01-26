using AutoMapper;
using IRepository.IGenericRepositories;
using IServices.ILDLApplicationsAllowedToRetakeATestServices;
using Microsoft.Extensions.Logging;
using ModelDTO.TestsDTO;
using Models.Tests;
using System.Linq.Expressions;

namespace Services.LDLApplicationsAllowedToRetakeATestServices;

public class LDLTestRetakeApplicationRetrieval : ILDLTestRetakeApplicationRetrieve
{
    private readonly IGetRepository<LDLApplicationsAllowedToRetakeATest> _getRepository;
    private readonly ILogger<LDLTestRetakeApplicationRetrieval> _logger;
    private readonly IMapper _mapper;

    public LDLTestRetakeApplicationRetrieval(IGetRepository<LDLApplicationsAllowedToRetakeATest> getRepository,
        ILogger<LDLTestRetakeApplicationRetrieval> logger,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<LDLApplicationsAllowedToRetakeATestDTO> GetByAsync(Expression<Func<LDLApplicationsAllowedToRetakeATest, bool>> predicate)
    {
        var ldlApp = await _getRepository.GetAsync(predicate);
        var ldlAppDTO = _mapper.Map<LDLApplicationsAllowedToRetakeATestDTO>(ldlApp);
        return ldlAppDTO;
    }
}
