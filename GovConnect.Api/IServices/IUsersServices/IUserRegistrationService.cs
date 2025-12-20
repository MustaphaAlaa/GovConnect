using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelDTO.API;
using ModelDTO.Users;

namespace IServices.IUserServices;


public interface IUserRegistrationService
{
    Task<bool> ValidateRegisterAsync(RegisterDTO register, ApiResponse response, ModelStateDictionary modelState);
}