using AutoMapper;
using IRepository;
using IServices.IApplicationServices.Employee;
using ModelDTO.ApplicationDTOs.Employee;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Services.Execptions;

namespace Services.ApplicationServices.Services.EmployeeAppService
{
    public class UpdateApplicationByEmployeeService : IUpdateApplicationByEmployee
    {
        private readonly IGetRepository<Application> _getRepository;
        private readonly IUpdateRepository<Application> _updateRepository;
        private IMapper _mapper;

        public UpdateApplicationByEmployeeService(IGetRepository<Application> getRepository,
            IUpdateRepository<Application> updateRepository,
            IMapper mapper)
        {
            _getRepository = getRepository;
            _updateRepository = updateRepository;
            _mapper = mapper;
        }

        public async Task<ApplicationDTOForEmployee> UpdateAsync(UpdateApplicationByEmployee updateRequest)
        {

            if (updateRequest is null)
                throw new ArgumentNullException();

            if (updateRequest.Id <= 0)
                throw new ArgumentOutOfRangeException();

            if (updateRequest.ApplicantUserId == Guid.Empty)
                throw new ArgumentException();

            if (!Enum.IsDefined(typeof(ApplicationStatus), (ApplicationStatus)updateRequest.ApplicationStatus))
                throw new DoesNotExistException();

            var existsApplication = await _getRepository.GetAsync(app => app.Id == updateRequest.Id)
                ?? throw new DoesNotExistException();

            if (existsApplication.ApplicantUserId != updateRequest.ApplicantUserId)
                throw new InvalidOperationException();

            existsApplication.ApplicationStatus = updateRequest.ApplicationStatus;
            existsApplication.UpdatedByEmployeeId = updateRequest.UpdatedByEmployeeId;
            existsApplication.LastStatusDate = DateTime.Now;

            var updatedApplication = await _updateRepository.UpdateAsync(existsApplication)
                   ?? throw new FailedToUpdateException();

            var updatedApplicationForUser = _mapper.Map<ApplicationDTOForEmployee>(existsApplication)
                   ?? throw new AutoMapperMappingException();

            return updatedApplicationForUser;
        }
    }
}
