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
using Microsoft.Extensions.Logging;

namespace Services.ApplicationServices.Services.UserAppServices;

public class CreateApplicationService : ICreateApplicationService
{
    private readonly ICreateApplicationEntity _createApplicationEntity;
    private readonly IGetRepository<ServiceFees> _getApplicationFeesRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateApplicationService> _logger;
    private readonly ICheckApplicationExistenceService _checkApplicationExistenceService;

    public CreateApplicationService(IGetRepository<ServiceFees> getFeesRepository,
        ICheckApplicationExistenceService checkApplicationExistenceService,
        ICreateApplicationEntity createApplicationEntity,
        ILogger<CreateApplicationService> logger,
        IMapper mapper)
    {
        _getApplicationFeesRepository = getFeesRepository;
        _checkApplicationExistenceService = checkApplicationExistenceService;
        _createApplicationEntity = createApplicationEntity;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ApplicationDTOForUser> CreateAsync(CreateApplicationRequest entity)
    {

        Expression<Func<ServiceFees, bool>> expression = appFees =>
            (appFees.ServicePurposeId == entity.ServicePurposeId
             && appFees.ServiceCategoryId == entity.ServiceCategoryId);

        try
        {
            var applicationFees = await _getApplicationFeesRepository.GetAsync(expression)
                                  ?? throw new DoesNotExistException("ServiceFees Doesn't Exist");


            await _checkApplicationExistenceService.CheckApplicationExistence(entity);

            var applicationIsCreated = (await _createApplicationEntity.CreateNewApplication(entity, applicationFees))
                ?? throw new FailedToCreateException();

            //var applicationDToForUser = _mapper.Map<ApplicationDTOForUser?>(applicationIsCreated);

            var applicationDToForUser = new ApplicationDTOForUser
            {
                ApplicationId = applicationIsCreated.ApplicationId,
                ApplicationDate = applicationIsCreated.ApplicationDate,
                ApplicationStatus = (byte)applicationIsCreated.ApplicationStatus,
                LastStatusDate = applicationIsCreated.LastStatusDate,
                PaidFees = applicationIsCreated.PaidFees,   /// Get it and add it from the service fees table
                ServiceCategoryId = entity.ServiceCategoryId,
                ServicePurposeId = entity.ServicePurposeId,
                UserId = applicationIsCreated.UserId
            };


            if (applicationDToForUser is null)
                throw new AutoMapperMappingException();

            return applicationDToForUser;
        }
        catch (DoesNotExistException ex)
        {
            throw;
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, "AutoMapper mapping failed.");
            throw;
        }
        catch (FailedToCreateException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred.");

            throw;
        }

    }
}