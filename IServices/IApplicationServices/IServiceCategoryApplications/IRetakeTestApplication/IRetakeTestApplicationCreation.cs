using ModelDTO.ApplicationDTOs.User;
using Models.Applications;

namespace IServices.IApplicationServices.IServiceCategoryApplications.IRetakeTestApplication;


/// <summary>
/// Interface for service responsible of inserting records in retake test application.
/// </summary>
public interface IRetakeTestApplicationCreation : ICreateService<CreateRetakeTestApplicationRequest, RetakeTestApplication>
{

}


public interface IRetakeTestApplicationRetriever : IAsyncRetrieveService<RetakeTestApplication, RetakeTestApplication>
{

}