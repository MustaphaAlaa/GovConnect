using AutoMapper;
using IRepository;
using IServices;
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

namespace Services.TestServices
{
    public class GetAllTestTypesService : IGetAllTestTypesService
    {

        private readonly IGetAllRepository<TestType> _getTestTypeRepository;
        private ILogger<TestType> _logger;
        private IMapper _mapper;

        public GetAllTestTypesService(IGetAllRepository<TestType> getTestTypeRepository, ILogger<TestType> logger, IMapper mapper)
        {
            _getTestTypeRepository = getTestTypeRepository;
            _logger = logger;
            _mapper = mapper;
        }



        public async Task<IQueryable<TestTypeDTO>> GetAllAsync(Expression<Func<TestType, bool>> predicate)
        {
            var lst = await _getTestTypeRepository.GetAllAsync(predicate);

            return lst.Select(tt => _mapper.Map<TestTypeDTO>(tt));
        }

        public async Task<List<TestTypeDTO>> GetAllAsync()
        {
            var lst = await _getTestTypeRepository.GetAllAsync();

            var lstMap = lst.Select(tt => _mapper.Map<TestTypeDTO>(tt));
            return lstMap.ToList();
        }
    }
}
