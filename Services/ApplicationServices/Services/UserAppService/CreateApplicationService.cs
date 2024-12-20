using AutoMapper;
using IRepository;
using IServices.IApplicationServices.User;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Services.Execptions;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using IServices.IApplicationServices.Category;
using Models.LicenseModels;
using Models.Users;

namespace Services.ApplicationServices.Services.UserAppServices;

public class CreateApplicationService : ICreateApplicationService
{
    private readonly ICreateApplicationEntity _createApplicationEntity;
    private readonly IGetRepository<ServiceFees> _getApplicationFeesRepository;
    private readonly IMapper _mapper;

    private readonly ICheckApplicationExistenceService _checkApplicationExistenceService;

    public CreateApplicationService(IGetRepository<ServiceFees> getFeesRepository,
        ICheckApplicationExistenceService checkApplicationExistenceService,
        ICreateApplicationEntity createApplicationEntity,
        IMapper mapper)
    {
        _getApplicationFeesRepository = getFeesRepository;
        _checkApplicationExistenceService = checkApplicationExistenceService;
        _createApplicationEntity = createApplicationEntity;
        _mapper = mapper;
    }

    public async Task<ApplicationDTOForUser> CreateAsync(CreateApplicationRequest entity)
    {

        Expression<Func<ServiceFees, bool>> expression = appFees =>
            (appFees.ServicePurposeId == entity.ServicePurposeId
             && appFees.ServiceCategoryId == entity.ServiceCategoryId);

        var applicationFees = await _getApplicationFeesRepository.GetAsync(expression)
                              ?? throw new DoesNotExistException("ServiceFees Doesn't Exist");


        await _checkApplicationExistenceService.CheckApplicationExistence(entity);

        var applicationIsCreated = (await _createApplicationEntity.CreateNewApplication(entity, applicationFees))
            ?? throw new FailedToCreateException();

        var applicationDToForUser = _mapper.Map<ApplicationDTOForUser>(applicationIsCreated)
                                    ?? throw new AutoMapperMappingException();

        return applicationDToForUser;
    }
}