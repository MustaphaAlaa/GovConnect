using IServices.IDriverServices;
using IServices.IValidators.DriverValidators;
using Microsoft.Extensions.Logging;

namespace Services.DriverServices.Validators;

public class DriverCreatorValidator : IDriverCreationValidator
{
    private readonly IDriverRetrieveService _driverRetriever;
    private readonly ILogger<DriverCreatorValidator> _logger;

    public DriverCreatorValidator(IDriverRetrieveService  driverRetriever,
        ILogger<DriverCreatorValidator>  logger)
    {
        _driverRetriever = driverRetriever;
        _logger = logger;
    }
    
    public async Task<bool> IsValid(Guid UserId)
    {
        _logger.LogInformation($"{this.GetType().Name} --- IsValid() Method --- Validating {nameof(UserId)}.");
        var driver = await _driverRetriever.GetByAsync(driver => driver.UserId == UserId);
        
        return  driver != null; 
    }
}