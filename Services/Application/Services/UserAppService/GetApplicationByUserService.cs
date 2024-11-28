using AutoMapper;
using IRepository;
using IServices.IApplicationServices.User;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Models.Users;
using Services.Execptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.ApplicationServices.Services.UserAppServices
{
    public class GetApplicationByUserService : IGetApplicationByUser
    {

        private readonly IGetRepository<Application> _getRepository;
        private readonly IGetRepository<User> _getUserRepository;


        public GetApplicationByUserService(IGetRepository<Application> getRepository, IGetRepository<User> getUserRepository)
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
                                    app.Id == getApplicationByUser.ApplicationId
                                    && app.ApplicantUserId == getApplicationByUser.userId);
        }


    }
}
