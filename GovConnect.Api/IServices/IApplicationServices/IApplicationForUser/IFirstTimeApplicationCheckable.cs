using ModelDTO.ApplicationDTOs.User;

namespace IServices.IApplicationServices.User;

public interface IFirstTimeApplicationCheckable < T> where T :  CreateApplicationRequest
{
    public Task<bool>  IsFirstTime(T request);
}