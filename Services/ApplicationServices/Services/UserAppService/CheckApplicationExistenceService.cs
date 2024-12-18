using IRepository;
using IServices.IApplicationServices.User;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Services.Execptions;

namespace Services.ApplicationServices.Services.UserAppServices;

public class CheckApplicationExistenceService : ICheckApplicationExistenceService
{
    private readonly IGetRepository<Application> _getRepository;

    public CheckApplicationExistenceService(IGetRepository<Application> getRepository)
    {
        _getRepository = getRepository;
    }

    public async Task<Application?> CheckApplicationExistence(CreateApplicationRequest entity)
    {
        var existenceApplication = await _getRepository.GetAsync(app =>
            app.UserId == entity.UserId
            && app.ApplicationPurposeId == entity.ApplicationPurposeId
            && app.ServiceCategoryId == entity.ServiceCategoryId);

        return existenceApplication;
    }
}