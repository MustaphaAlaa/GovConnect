using AutoMapper;
using IRepository.IGenericRepositories;
using IServices.IDriverServices;
using Microsoft.Extensions.Logging;
using ModelDTO.Users;
using Models.Users;

namespace Services.DriverServices;

public class DriverCreatorService : IDriverCreatorService
{
    private readonly ICreateRepository<Driver> _createRepository;
    private readonly ILogger<DriverCreatorService> _logger;
    private readonly IMapper _mapper;


    public DriverCreatorService(ICreateRepository<Driver> createRepository, ILogger<DriverCreatorService> logger,
        IMapper mapper)
    {
        _createRepository = createRepository;
        _logger = logger;
        _mapper = mapper;
    }


    public async Task<DriverDTO> CreateAsync(Driver entity)
    {
        _logger.LogInformation($"{this.GetType().Name} -- CreateAsync Method -- Creating driver");
        try
        {
            if (entity == null)
            {
                _logger.LogError($"{this.GetType().Name} -- CreateAsync Entity -- null");
                throw new ArgumentNullException(nameof(entity));
            }
            entity.CreatedDate = DateTime.Now; 
            var driver = await _createRepository.CreateAsync(entity);
            var driverDTO = _mapper.Map<DriverDTO>(driver);
            return driverDTO;
        }
        catch (Exception e)
        {
            _logger.LogError($"{this.GetType().Name} -- CreateAsync Exception -- Exception: {e}");
            throw;
        }
    }
}