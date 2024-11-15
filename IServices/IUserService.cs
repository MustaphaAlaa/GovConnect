using System.Linq.Expressions;
using ModelDTO;
using Models.Users;

namespace IServices;

public interface IUserService
{
    public Task<UserDTO> GetAsync(Guid id);
    public Task<IList<UserDTO>> GetAllAsync();

    public Task<CountryDTO> CreateAsync(RegisterDTO entity);
    public Task<UserDTO> UpdateAsync(UpdateUserDTO entity);
    
    public Task<bool> DeleteAsync(Guid id);
}