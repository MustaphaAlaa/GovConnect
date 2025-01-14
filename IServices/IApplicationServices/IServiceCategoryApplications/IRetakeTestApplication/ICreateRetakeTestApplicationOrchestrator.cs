using IServices.IValidtors.ILocalDrivingLicenseApplications;
using Models.ApplicationModels;

namespace IServices.IApplicationServices.IServiceCategoryApplications.IRetakeTestApplication;




public interface ICreateRetakeTestApplicationOrchestrator
{
    Task<LocalDrivingLicenseApplication> Create(/*Dummy parameter*/ object request, ILocalDrivingLicenseApplicationServicePurposeValidator validator);
}
