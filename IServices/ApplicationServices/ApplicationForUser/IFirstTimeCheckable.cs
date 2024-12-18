using ModelDTO.ApplicationDTOs.User;

namespace IServices.IApplicationServices.User;

public interface IFirstTimeCheckable < T> where T :  CreateApplicationRequest
{
    public Task<bool>  IsFirstTime(T request);
}