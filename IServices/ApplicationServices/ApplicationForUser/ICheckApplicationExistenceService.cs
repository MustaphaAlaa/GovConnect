using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;

namespace IServices.IApplicationServices.User;

public interface ICheckApplicationExistenceService
{
    public Task<Application?> CheckApplicationExistence(CreateApplicationRequest entity);
}
