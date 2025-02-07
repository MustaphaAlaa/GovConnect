using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelDTO.API;
using ModelDTO.Users;
using Models.Users;

namespace IServices.IUserServices;

/// <summary>
/// Interface for retrieve User Info from the database.
/// </summary> 
public interface IUserRetrieveService : IAsyncRetrieveService<User, UserDTO>
{
}


public interface IUserRegistrationService
{
    Task<bool> ValidateRegisterAsync(RegisterDTO register, ApiResponse response, ModelStateDictionary modelState);
}