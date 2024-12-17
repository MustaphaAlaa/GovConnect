using IServices.IApplicationServices.User;
using ModelDTO.ApplicationDTOs.User;

namespace Services.ApplicationServices.Services.UserAppServices;

public class CreateApplicationServiceValidator : ICreateApplicationServiceValidator
{
    public void ValidateRequest(CreateApplicationRequest request)
    {
        if (request is null)
            throw new ArgumentNullException("Create Request is null.");

        if (request.UserId == Guid.Empty)
            throw new ArgumentException();

        if (request.ApplicationPurposeId <= 0)
            throw new ArgumentOutOfRangeException("ApplicationPurposeId nust be greater than 0");

        if (request.ServiceCategoryId <= 0)
            throw new ArgumentOutOfRangeException("ServiceCategoryId nust be greater than 0");
    }
}