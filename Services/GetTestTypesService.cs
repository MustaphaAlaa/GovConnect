using AutoMapper;
using IRepository;
using IServices.ITests;
using Microsoft.Extensions.Logging;
using ModelDTO.TestsDTOs;
using Models.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services;
public class GetTestTypesService : IGetTestTypeService
{
    private readonly IGetRepository<TestType> _getTestTypeRepository;
    private ILogger<TestType> _logger;
    private IMapper _mapper;
    public GetTestTypesService(IGetRepository<TestType> testTypeRepository, ILogger<TestType> logger, IMapper mapper)
    {
        _getTestTypeRepository = testTypeRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<TestTypeDTO> GetByAsync(Expression<Func<TestType, bool>> predicate)
    {
        _logger.LogInformation("Get TestType by Expression");

        var testType = await _getTestTypeRepository.GetAsync(predicate);

        if (testType == null)
        {
            _logger.LogInformation("TestType not found");
            return null;
        }

        _logger.LogInformation("TestType found");
        var testTypeDTO = _mapper.Map<TestTypeDTO>(testType);
        return testTypeDTO;

    }
}
