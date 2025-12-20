using Models.ApplicationModels;

namespace IServices.IApplicationServices.User;

public interface IValidateApplicationStatus
{
    void CheckApplicationStatus(EnApplicationStatus enApplicationStatus);
}
