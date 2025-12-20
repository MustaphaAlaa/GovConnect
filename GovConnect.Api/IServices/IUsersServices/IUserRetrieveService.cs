using ModelDTO.Users;
using Models.Users;

namespace IServices.IUserServices;

/// <summary>
/// Interface for retrieve User Info from the database.
/// </summary> 
public interface IUserRetrieveService : IAsyncRetrieveService<User, UserDTO>
{
}
