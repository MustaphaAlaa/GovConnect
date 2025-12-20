using ModelDTO.ApplicationDTOs.User;
using ModelDTO.TestsDTO;
using Models.Applications;

namespace IServices.IApplicationServices.IServiceCategoryApplications.IRetakeTestApplication;


/// <summary>
/// Interface for service responsible of inserting records in retake test application.
/// </summary>
public interface IRetakeTestApplicationCreation : ICreateService<CreateRetakeTestApplicationRequest, RetakeTestApplication>
{
    event Func<object, TestDTO, Task> RetakeTestApplicationCreated;
}
