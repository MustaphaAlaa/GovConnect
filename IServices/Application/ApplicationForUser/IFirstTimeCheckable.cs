using ModelDTO.ApplicationDTOs.User;

namespace IServices.IApplicationServices.User;

public interface IFirstTimeCheckable
{
    public Task<bool>  IsFirstTime(CreateApplicationRequest request);
}