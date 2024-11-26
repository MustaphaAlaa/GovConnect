using AutoMapper;
using IRepository;
using IServices.IApplicationServices.User;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.ApplicationServices.Services.User
{
    public class GetApplicationByUserService : IGetApplicationByUser
    {

        private readonly IGetRepository<Application> _getRepository;

        public GetApplicationByUserService(IGetRepository<Application> getRepository)
        {

            _getRepository = getRepository;
        }

        public async Task<Application> GetByAsync(Expression<Func<Application, bool>> predicate)
        {
            return await _getRepository.GetAsync(predicate);
        }
    }
}
