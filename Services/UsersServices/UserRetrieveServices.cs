using System.Linq.Expressions;
using AutoMapper;
using IRepository.IGenericRepositories;
using IServices.IUserServices;
using Microsoft.Extensions.Logging;
using ModelDTO.Users;
using Models.Users;

namespace Services.UsersServices;

public class UserRetrieveServices : IUserRetrieveService
{
    private readonly ILogger<UserRetrieveServices> _logger;
    private readonly IGetRepository<User> _getRepository;
    private readonly IMapper mapper;

    public UserRetrieveServices(ILogger<UserRetrieveServices> logger,
     IGetRepository<User> getRepository,
      IMapper mapper)
    {
        _logger = logger;
        _getRepository = getRepository;
        this.mapper = mapper;
    }

    public async Task<UserDTO> GetByAsync(Expression<Func<User, bool>> predicate)
    {
        var user = await _getRepository.GetAsync(predicate);
        var userDTO = mapper.Map<UserDTO>(user);
        return userDTO;
    }
}
