using IServices.IApplicationServices.User;
using IServices.ILDLApplicationsAllowedToRetakeATestServices;
using IServices.IValidators;
using Microsoft.Extensions.Logging;
using ModelDTO.ApplicationDTOs.User;
using Services.ApplicationServices.Services.UserAppServices;
using Services.Exceptions;
namespace Services.ApplicationServices.ServiceCategoryApplications;


public class CreateRetakeTestApplicationValidator : CreateApplicationServiceValidator, ICreateRetakeTestApplicationValidation
{
    private readonly ILDLTestRetakeApplicationRetrieve _lDLTestRetakeApplicationRetrieve;
    private readonly ILogger<CreateRetakeTestApplicationValidator> _logger;

    public CreateRetakeTestApplicationValidator(ILDLTestRetakeApplicationRetrieve lDLTestRetakeApplicationRetrieve,
        ILogger<CreateRetakeTestApplicationValidator> logger,
        ILogger<ICreateApplicationServiceValidator> baseLogger) : base(baseLogger)
    {
        _lDLTestRetakeApplicationRetrieve = lDLTestRetakeApplicationRetrieve;
        _logger = logger;
    }

    public async Task Validate(CreateRetakeTestApplicationRequest request)
    {
        _logger.LogInformation($"{this.GetType().Name} -- Validate the request");

        await base.ValidateRequest(request);

        var lDLAllowed = await _lDLTestRetakeApplicationRetrieve.GetByAsync(ldl => ldl.LocalDrivingLicenseApplicationId == request.LocalDrivingLicenseApplicationId
                                                             && ldl.TestTypeId == request.TestTypeId);


        if (lDLAllowed == null || !lDLAllowed.IsAllowedToRetakeATest)
        {
            _logger.LogError($"the ldl application isn't allowed to retake this test");
            throw new ValidationException("Local Driving License Application is not allowed to retake a test");
        }

    }
}
