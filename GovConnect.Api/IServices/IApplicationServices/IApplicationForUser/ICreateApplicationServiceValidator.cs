using ModelDTO.ApplicationDTOs.User;

namespace IServices.IApplicationServices.User;

public interface ICreateApplicationServiceValidator
{
    public   Task ValidateRequest(CreateApplicationRequest entity);
}


