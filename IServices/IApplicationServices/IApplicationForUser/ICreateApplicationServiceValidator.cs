using ModelDTO.ApplicationDTOs.User;

namespace IServices.IApplicationServices.User;

public interface ICreateApplicationServiceValidator
{
    public void ValidateRequest(CreateApplicationRequest entity);
}


