
using AutoMapper;
using IRepository;
using IServices.IApplicationServices.User;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Services.Execptions;


namespace Services.ApplicationServices.Services.UserAppServices;

public class UpdateApplcationByUserService : IUpdateApplicationByUser
{
    private readonly IUpdateRepository<LicenseApplication> _updateRepository;
    private readonly IGetRepository<LicenseApplication> _getRepository;
    private readonly IMapper _mapper;

    public UpdateApplcationByUserService(IUpdateRepository<LicenseApplication> updateRepository,
                                            IGetRepository<LicenseApplication> getRepository,
                                            IMapper mapper)
    {
        _updateRepository = updateRepository;
        _getRepository = getRepository;
        _mapper = mapper;
    }

    public async Task<ApplicationDTOForUser> UpdateAsync(UpdateApplicationByUser updateRequest)
    {
        if (updateRequest is null)
            throw new ArgumentNullException();

        if (updateRequest.Id <= 0)
            throw new ArgumentOutOfRangeException();

        if (updateRequest.ApplicantUserId == Guid.Empty)
            throw new ArgumentOutOfRangeException();

        if (updateRequest.ApplicationTypeId <= 0)
            throw new ArgumentOutOfRangeException();

        if (updateRequest.ApplicationForId <= 0)
            throw new ArgumentOutOfRangeException();

        var existsApplication = await _getRepository.GetAsync(app => app.Id == updateRequest.Id)
            ?? throw new DoesNotExistException();

        existsApplication.ApplicationForId = updateRequest.ApplicationForId;
        existsApplication.ApplicationTypeId = updateRequest.ApplicationTypeId;
        existsApplication.ApplicationDate = DateTime.Now;

        var updatedApplication = await _updateRepository.UpdateAsync(existsApplication)
               ?? throw new FailedToUpdateException();

        var updatedApplicationForUser = _mapper.Map<ApplicationDTOForUser>(existsApplication)
               ?? throw new AutoMapperMappingException();

        return updatedApplicationForUser;
    }
}
