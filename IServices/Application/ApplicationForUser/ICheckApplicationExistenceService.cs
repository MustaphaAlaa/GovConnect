using ModelDTO.ApplicationDTOs.User;

namespace IServices.IApplicationServices.User;

public interface ICheckApplicationExistenceService
{
    public Task CheckApplicationExistence(CreateApplicationRequest entity);
}