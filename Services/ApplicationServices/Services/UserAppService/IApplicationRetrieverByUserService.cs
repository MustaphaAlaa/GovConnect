using IRepository;
using IServices.IApplicationServices.User;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Models.Users;
using Services.Exceptions;

namespace Services.ApplicationServices.Services.UserAppServices
{
    public class IApplicationRetrieverByUserService : IApplicationRetrieverByUser
    {

        private readonly IGetRepository<Application> _getRepository;
        private readonly IGetRepository<User> _getUserRepository;


        public IApplicationRetrieverByUserService(IGetRepository<Application> getRepository, IGetRepository<User> getUserRepository)
        {
            _getRepository = getRepository;
            _getUserRepository = getUserRepository;
        }

        public async Task<Application?> GetByAsync(GetApplicationByUser getApplicationByUser)
        {
            if (getApplicationByUser.ApplicationId <= 0)
                throw new ArgumentOutOfRangeException();

            if (getApplicationByUser.userId == Guid.Empty)
                throw new InvalidOperationException("");

            var user = await _getUserRepository.GetAsync(user => user.Id == getApplicationByUser.userId)
                   ?? throw new DoesNotExistException("The user does not exists.");

            return await _getRepository.GetAsync(app =>
                                    app.ApplicationId == getApplicationByUser.ApplicationId
                                    && app.UserId == getApplicationByUser.userId);
        }


    }
}
