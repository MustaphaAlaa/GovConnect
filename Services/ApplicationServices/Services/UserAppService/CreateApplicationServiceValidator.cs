using IServices.IApplicationServices.User;
using Microsoft.Extensions.Logging;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Services.Execptions;

namespace Services.ApplicationServices.Services.UserAppServices;

public abstract class CreateApplicationServiceValidator : ICreateApplicationServiceValidator
{
    ILogger<ICreateApplicationServiceValidator> _logger;


    protected CreateApplicationServiceValidator(ILogger<ICreateApplicationServiceValidator> logger)
    {
        _logger = logger;
    }

    public virtual Task ValidateRequest(CreateApplicationRequest request)
    {
        _logger.LogInformation("---------------- Validation processes for creating new application is starting ");
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "!!! Create Application Request Validation Faild");
            throw;
        }

        return Task.CompletedTask;
    }
}
