using AutoMapper;
using IRepository;
using IServices.IApplicationServices.User;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Services.Execptions;
using System.Linq.Expressions;
using Models.LicenseModels;

namespace Services.ApplicationServices.Services.UserAppServices
{
    public class CreateApplicationService : ICreateApplication
    {
        private readonly ICreateRepository<Application> _createRepository;
        private readonly IGetRepository<Application> _getRepository;
        private readonly IGetRepository<ServiceFees> _getApplicationFeesRepository;
        private readonly IMapper _mapper;

        public CreateApplicationService(ICreateRepository<Application> createRepository,
            IGetRepository<Application> getRepository,
            IGetRepository<ServiceFees> getFeesRepository,
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

            if (entity.UserId == Guid.Empty)
                throw new ArgumentException();

            if (entity.ApplicationPurposeId <= 0)
                throw new ArgumentOutOfRangeException("ApplicationPurposeId nust be greater than 0");

            if (entity.ServiceCategoryId <= 0)
                throw new ArgumentOutOfRangeException("ServiceCategoryId nust be greater than 0");

            Expression<Func<ServiceFees, bool>> expression = appFees =>
                (appFees.ApplicationTypeId == entity.ApplicationPurposeId
                 && appFees.ServiceCategoryId == entity.ServiceCategoryId);

            var applicationFees = await _getApplicationFeesRepository.GetAsync(expression)
                                  ?? throw new DoesNotExistException("ServiceFees Doesn't Exist");

            
            var existenceApplication = await _getRepository.GetAsync(app =>
                app.UserId == entity.UserId
                && app.ApplicationPurposeId == entity.ApplicationPurposeId
                && app.ServiceCategoryId == entity.ServiceCategoryId);

            switch (existenceApplication?.ApplicationStatus)
            {
                case (byte)ApplicationStatus.Finalized:
                case (byte)ApplicationStatus.InProgress:
                case (byte)ApplicationStatus.Pending:
                    throw new InvalidOperationException();
            }


            if (entity.IsFirstTimeOnly)
            {
                /*Check if the applicant has already a license in the license class*/
                //if license class && userID Exist in license tables 
                throw new AlreadyExistException();
            }


            /*
             * 
             * IApplicationFor.Validate()
            */
            var newApplication = _mapper.Map<Application>(entity)
                                 ?? throw new AutoMapperMappingException();

            newApplication.ApplicationDate = DateTime.Now;
            newApplication.LastStatusDate = DateTime.Now;
            newApplication.PaidFees = applicationFees.Fees;
            newApplication.ApplicationStatus = ((byte)ApplicationStatus.InProgress);
            newApplication.UpdatedByEmployeeId = null;

            /*i'll make class/method to create it and all application needed */
            var applicationisCreated = await _createRepository.CreateAsync(newApplication)
                                       ?? throw new FailedToCreateException();

            var applicationDToForUser = _mapper.Map<ApplicationDTOForUser>(applicationisCreated)
                                        ?? throw new AutoMapperMappingException();

            /*IApplicationFor.Create()*/
            
            return applicationDToForUser;
        }
    }
}