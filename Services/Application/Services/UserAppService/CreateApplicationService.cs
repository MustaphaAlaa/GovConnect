using AutoMapper;
using IRepository;
using IServices.IApplicationServices.User;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Services.Execptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.ApplicationServices.Services.UserAppServices
{
    public class CreateApplicationService : ICreateApplication
    {

        private readonly ICreateRepository<Application> _createRepository;
        private readonly IGetRepository<Application> _getRepository;
        private readonly IGetRepository<ApplicationFees> _getApplicationFeesRepository;
        private readonly IMapper _mapper;

        public CreateApplicationService(ICreateRepository<Application> createRepository,
               IGetRepository<Application> getRepository,
               IGetRepository<Models.ApplicationModels.ApplicationFees> getFeesRepository,
               IMapper mapper)
        {
            _getApplicationFeesRepository = getFeesRepository;
            _createRepository = createRepository;
            _getRepository = getRepository;
            _mapper = mapper;
        }

        public async Task<ApplicationDTOForUser> CreateAsync(CreateApplicationRequest entity)
        {
            if (entity is null)
                throw new ArgumentNullException("Create Request is null.");

            if (entity.ApplicantUserId == Guid.Empty)
                throw new ArgumentException();

            if (entity.ApplicationTypeId <= 0)
                throw new ArgumentOutOfRangeException("ApplicationTypeId nust be greater than 0");

            if (entity.ApplicationForId <= 0)
                throw new ArgumentOutOfRangeException("ApplicationForId nust be greater than 0");

            Expression<Func<ApplicationFees, bool>> expression = appFees =>
                                    (appFees.ApplicationTypeId == entity.ApplicationTypeId
                                    && appFees.ApplicationForId == entity.ApplicationForId);

            var applicationFees = await _getApplicationFeesRepository.GetAsync(expression)
                ?? throw new DoesNotExistException("ApplicationFees Doesn't Exist");

            var existenceApplication = await _getRepository.GetAsync(app => app.ApplicantUserId == entity.ApplicantUserId
                                                                        && app.ApplicationTypeId == entity.ApplicationTypeId
                                                                        && app.ApplicationForId == entity.ApplicationForId);

            switch (existenceApplication?.ApplicationStatus)
            {
                case (byte)ApplicationStatus.Finalized:
                case (byte)ApplicationStatus.InProgress:
                case (byte)ApplicationStatus.Pending:
                    throw new InvalidOperationException();
            }

            var newApplication = _mapper.Map<Application>(entity)
                                    ?? throw new AutoMapperMappingException();

            newApplication.ApplicationDate = DateTime.Now;
            newApplication.LastStatusDate = DateTime.Now;
            newApplication.PaidFees = applicationFees.Fees;
            newApplication.ApplicationStatus = ((byte)ApplicationStatus.InProgress);
            newApplication.UpdatedByEmployeeId = null;

            var applicationisCreated = await _createRepository.CreateAsync(newApplication)
                                           ?? throw new FailedToCreateException();

            var applicationDToForUser = _mapper.Map<ApplicationDTOForUser>(applicationisCreated)
                                        ?? throw new AutoMapperMappingException();

            return applicationDToForUser;

        }
    }
}
