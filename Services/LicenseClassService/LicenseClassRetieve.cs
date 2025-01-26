

using System.Linq.Expressions;
using AutoMapper;
using IRepository.IGenericRepositories;
using Microsoft.Extensions.Logging;
using ModelDTO.LicenseDTOs;
using Models.LicenseModels;

namespace IServices.ILicenseClassServices
{
    public class LicenseClassRetrieve : ILicenseClassRetrieve
    {

        private readonly IGetRepository<LicenseClass> _licenseClassRepository;
        private readonly ILogger<LicenseClassRetrieve> _logger;
        private readonly IMapper _mapper;

        public LicenseClassRetrieve(IGetRepository<LicenseClass> licenseClassRepository,
         ILogger<LicenseClassRetrieve> logger,
          IMapper mapper)
        {
            _licenseClassRepository = licenseClassRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<LicenseClassDTO> GetByAsync(Expression<Func<LicenseClass, bool>> predicate)
        {
            _logger.LogInformation($"{nameof(GetByAsync)} method in {this.GetType().Name} called.");
            var licenseClass = await _licenseClassRepository.GetAsync(predicate);
            var licenseClassDTO = _mapper.Map<LicenseClassDTO>(licenseClass);
            return licenseClassDTO;
        } 
    }
}