using IServices.IApplicationServices.User;
using Microsoft.Extensions.Logging;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Services.Exceptions;

namespace Services.ApplicationServices.Services.UserAppServices;

/// <summary>
/// shared validation that every application record regradless its category or puprpose will implement
/// </summary>
public abstract class CreateApplicationServiceValidator : ICreateApplicationServiceValidator
{
    ILogger<ICreateApplicationServiceValidator> _logger;


    protected CreateApplicationServiceValidator(ILogger<ICreateApplicationServiceValidator> logger)
    {
        _logger = logger;
    }


    /// <summary>
    /// Validate the request for creating an application record in database
    /// </summary>
    /// <param name="request">The request object containing the details for the new CreateApplicationRequest.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DoesNotExistException"></exception>
    public virtual Task ValidateRequest(CreateApplicationRequest request)
    {
        _logger.LogInformation("---------------- Validate processes for creating new application is starting ");
        try
        {
            if (request is null)
                throw new ArgumentNullException("Create Request is null.");

            if (request.UserId == Guid.Empty)
                throw new ArgumentException();

            if (!(Enum.IsDefined(typeof(EnServicePurpose), (int)request.ServicePurposeId)))
                throw new DoesNotExistException("ServicePurposeId must be contained in enum EnServicePurpose");

            if (!(Enum.IsDefined(typeof(EnServicePurpose), (int)request.ServiceCategoryId)))
                throw new DoesNotExistException("ServiceCategoryId must be contained in enum EnServicePurpose");
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "!!! Create Application Request Validate Faild");
            throw;
        }

        return Task.CompletedTask;
    }
}
