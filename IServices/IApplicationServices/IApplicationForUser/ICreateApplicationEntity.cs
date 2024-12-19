using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;

public interface ICreateApplicationEntity
{
    public Task<Application> CreateNewApplication(CreateApplicationRequest request, ServiceFees serviceFees);
}