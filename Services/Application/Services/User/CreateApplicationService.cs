using AutoMapper;
using IRepository;
using IServices.Application.User;
using ModelDTO.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Application.Services.User
{
    public class CreateApplicationService : ICreateApplication
    {


        private readonly ICreateRepository<Models.ApplicationModels.Application> _createRepository;
        private readonly IGetRepository<Models.ApplicationModels.Application> _getRepository;
        private readonly IMapper _mapper;

        public CreateApplicationService(ICreateRepository<Models.ApplicationModels.Application> createRepository,
               IGetRepository<Models.ApplicationModels.Application> getRepository,
               IMapper mapper)
        {
            _createRepository = createRepository;
            _getRepository = getRepository;
            _mapper = mapper;
        }

        public Task<ApplicationDTOForUser> CreateAsync(CreateApplicationRequest entity)
        {
            throw new NotImplementedException();
        }
    }
}
