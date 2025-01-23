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
using System.Linq.Expressions;

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

    public RetakeTestApplicationCreateor(ICreateRetakeTestApplicationValidation retakeTestValiadtion,
        ITestTypeRetrievalService testTypeRetrievalService,
        ICreateRepository<RetakeTestApplication> createRepository,
        IGetLocalDrivingLicenseApplication localDrivingLicenseApplication,
        ICreateApplicationEntity createApplicationEntity,
        IServiceFeeRetrieverService serviceFeeRetrieverService,
        ILogger<RetakeTestApplicationCreateor> logger,
        IMapper mapper)
    {
        _retakeTestValiadtion = retakeTestValiadtion;
        _testTypeRetrievalService = testTypeRetrievalService;
        _createRepository = createRepository;
        _localDrivingLicenseApplication = localDrivingLicenseApplication;
        _createApplicationEntity = createApplicationEntity;
        _serviceFeeRetrieverService = serviceFeeRetrieverService;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<RetakeTestApplication> CreateAsync(CreateRetakeTestApplicationRequest entity)
    {
        try
        {
            //await _retakeTestValiadtion.Validate(entity);

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
            throw;
        }

    }
}




/// <summary>
/// Responsible for retrieving data from RetakeTestApplication table.
/// </summary>
public class RetakeTestApplicationRetriever : IRetakeTestApplicationRetriever
{
    private readonly IGetRepository<RetakeTestApplication> _getRepository;
    private readonly ILogger<RetakeTestApplicationRetriever> _logger;
    private readonly IMapper _mapper;

    public RetakeTestApplicationRetriever(IGetRepository<RetakeTestApplication> getRepository, ILogger<RetakeTestApplicationRetriever> logger, IMapper mapper)
    {
        _getRepository = getRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<RetakeTestApplication> GetByAsync(Expression<Func<RetakeTestApplication, bool>> predicate)
    {
        _logger.LogInformation($"{this.GetType().Name} -- GetByAsync -- RetakeTestApplication");
        var retakeTestApplication = await _getRepository.GetAsync(predicate);
        return retakeTestApplication;
    }
}