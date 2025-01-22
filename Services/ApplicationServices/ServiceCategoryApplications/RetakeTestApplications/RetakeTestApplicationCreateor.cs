using AutoMapper;
using Azure.Core;
using IRepository.IGenericRepositories;
using IServices.IApplicationServices.Fees;
using IServices.IApplicationServices.IServiceCategoryApplications.ILocalDrivingLicenseApplication;
using IServices.IApplicationServices.IServiceCategoryApplications.IRetakeTestApplication;
using IServices.IApplicationServices.User;
using IServices.ITests.ITestTypes;
using IServices.IValidators;
using Microsoft.Extensions.Logging;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Models.Applications;
using Services.Exceptions;

namespace Services.ApplicationServices.ServiceCategoryApplications;


public class RetakeTestApplicationCreateor : IRetakeTestApplicationCreation
{
    private readonly ICreateRetakeTestApplicationValidation _retakeTestValiadtion;
    private readonly ITestTypeRetrievalService _testTypeRetrievalService;
    private readonly ICreateRepository<RetakeTestApplication> _createRepository;
    private readonly IGetLocalDrivingLicenseApplication _localDrivingLicenseApplication;
    private readonly ICreateApplicationEntity _createApplicationEntity;
    private readonly IServiceFeeRetrieverService _serviceFeeRetrieverService;
    private readonly ILogger<RetakeTestApplicationCreateor> _logger;
    private readonly IMapper _mapper;

    public async Task<RetakeTestApplication> CreateAsync(CreateRetakeTestApplicationRequest entity)
    {
        try
        {
            await _retakeTestValiadtion.Validate(entity);

            //var testType = await _testTypeRetrievalService.GetByAsync(tt => tt.TestTypeId == entity.TestTypeId);

            var ldlApp = await _localDrivingLicenseApplication.GetByAsync(ldl => ldl.Id == entity.LocalDrivingLicenseApplicationId);

            var serviceFee = await _serviceFeeRetrieverService.GetByAsync(sf => sf.ServicePurposeId == entity.ServicePurposeId
                                                                            && sf.ServiceCategoryId == entity.ServiceCategoryId);

            if (serviceFee is null)
            {
                _logger.LogError("Service Fee Does not exist");

                throw new DoesNotExistException();
            }

            if (ldlApp == null)
            {
                _logger.LogError("Local Driving Application Does not exist");
                throw new DoesNotExistException();
            }


            Application application = await _createApplicationEntity.CreateNewApplication(entity, serviceFee);

            RetakeTestApplication retakeTestApplicationReq = new RetakeTestApplication()
            {
                ApplicationId = application.ApplicationId,
                LocalDrivingLicenseApplicationId = entity.LocalDrivingLicenseApplicationId,
                TestTypeId = entity.TestTypeId
            };


            var retakeTestApplication = await _createRepository.CreateAsync(retakeTestApplicationReq);

            return retakeTestApplication;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw new Exception();
        }

    }
}
