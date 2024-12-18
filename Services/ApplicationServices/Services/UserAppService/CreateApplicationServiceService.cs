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

public class CreateApplicationServiceService : ICreateApplicationService
{
    private readonly ICreateApplicationEntity _createApplicationEntity;
    private readonly IGetRepository<ServiceFees> _getApplicationFeesRepository;
    private readonly IServiceCategoryService _serviceCategoryService;
     private readonly IMapper _mapper;

    private readonly ICreateApplicationServiceValidator _createApplicationServiceValidator;
    private readonly ICheckApplicationExistenceService _checkApplicationExistenceService;

    public CreateApplicationServiceService(  IGetRepository<ServiceFees> getFeesRepository,
        IServiceCategoryService serviceCategoryService, 
        ICreateApplicationServiceValidator createApplicationServiceValidator,
        ICheckApplicationExistenceService checkApplicationExistenceService,
        ICreateApplicationEntity createApplicationEntity,
        IMapper mapper)
    {
        _getApplicationFeesRepository = getFeesRepository;
        _serviceCategoryService = serviceCategoryService; 
        _createApplicationServiceValidator = createApplicationServiceValidator;
        _checkApplicationExistenceService = checkApplicationExistenceService;
        _createApplicationEntity = createApplicationEntity;
        _mapper = mapper;
    }

    public async Task<ApplicationDTOForUser> CreateAsync(CreateApplicationRequest entity)
    {
        _createApplicationServiceValidator.ValidateRequest(entity);

        Expression<Func<ServiceFees, bool>> expression = appFees =>
            (appFees.ApplicationTypeId == entity.ApplicationPurposeId
             && appFees.ServiceCategoryId == entity.ServiceCategoryId);

        var applicationFees = await _getApplicationFeesRepository.GetAsync(expression)
                              ?? throw new DoesNotExistException("ServiceFees Doesn't Exist");


        await _checkApplicationExistenceService.CheckApplicationExistence(entity);


        bool isValidServiceCategory = await _serviceCategoryService.IsValidServiceCategory();

        if (!isValidServiceCategory)
            throw new Exception();


        var applicationIsCreated = await _createApplicationEntity.CreateNewApplication(entity, applicationFees);

        var applicationDToForUser = _mapper.Map<ApplicationDTOForUser>(applicationIsCreated)
                                    ?? throw new AutoMapperMappingException();

        return applicationDToForUser;
    }
}