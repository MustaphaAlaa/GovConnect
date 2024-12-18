using AutoMapper;
using IRepository;
using IServices.IApplicationServices.IServiceCategoryApplications;
using IServices.IApplicationServices.User;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Models.LicenseModels;
using Models.Users;
using Services.ApplicationServices.Services.UserAppServices;
using Services.ApplicationServices.Services.UserAppServices.IsFirstTime;
using Services.Execptions;

namespace Services.ApplicationServices.ServiceCategoryApplications;

public class CreateLocalDrivingLicenseApplicationService : ICreateLocalDrivingLicenseApplicationService
{
    public CreateLocalDrivingLicenseApplicationService(ICreateApplicationService createApplicationService, ICreateRepository<LocalDrivingLicenseApplication> localDrivingLicenseApplicationRepository)
    {
        _createApplicationService = createApplicationService;
        _localDrivingLicenseApplicationRepository = localDrivingLicenseApplicationRepository;
        //_validator = new NewLocalDrivingLicenseApplicationValidator();
    }

    private readonly ICreateApplicationService _createApplicationService;
    private readonly ICreateRepository<LocalDrivingLicenseApplication> _localDrivingLicenseApplicationRepository;
    private readonly ICreateApplicationServiceValidator _validator;


    public Task<LocalDrivingLicenseApplication> Create(CreateLocalDrivingLicenseApplicationRequest entity)
    {
        _validator.ValidateRequest(entity);  //validate => validate for request => validate for localdrivinglicing

        if (entity.ApplicationId == 0)
            return null;
        throw new NotImplementedException();
    }


}


public class NewLocalDrivingLicenseApplication : INewLocalDrivingLicenseApplication
{
    public NewLocalDrivingLicenseApplication(ICreateApplicationService createApplicationService,
        ICreateRepository<LocalDrivingLicenseApplication> localDrivingLicenseApplicationRepository,
        IMapper mapper)
    {
        _createApplicationService = createApplicationService;
        _localDrivingLicenseApplicationRepository = localDrivingLicenseApplicationRepository;
        //_validator = new NewLocalDrivingLicenseApplicationValidator();
        _mapper = mapper;
    }

    private readonly ICreateApplicationService _createApplicationService;
    private readonly ICreateRepository<LocalDrivingLicenseApplication> _localDrivingLicenseApplicationRepository;
    private readonly ICreateApplicationServiceValidator _validator;
    private readonly IMapper _mapper;


    public Task<LocalDrivingLicenseApplication> CreateAsync(CreateLocalDrivingLicenseApplicationRequest entity)
    {

        LocalDrivingLicenseApplication localDrivingLicense = _mapper.Map<LocalDrivingLicenseApplication>(entity);
        localDrivingLicense.LicenseClassId = entity.LicenseClassId;
        throw new NotImplementedException();
    }


}

public class NewLocalDrivingLicenseApplicationValidator : CreateApplicationServiceValidator
{
    private readonly ICheckApplicationExistenceService _checkApplicationExistenceService;
    private readonly IFirstTimeCheckable<CreateLocalDrivingLicenseApplicationRequest> _firstTimeChecker;
    private readonly IPendingOrInProgressApplicationStatus _pendingOrInProgressApplicationStatus;

    public NewLocalDrivingLicenseApplicationValidator(
        IGetRepository<Application> getApplicationRepository,
        IFirstTimeCheckable<CreateLocalDrivingLicenseApplicationRequest> firstTimeChecker,
        ICheckApplicationExistenceService checkApplicationExistenceService,
        IPendingOrInProgressApplicationStatus pendingOrInProgressApplicationStatus)
    {

        _firstTimeChecker = firstTimeChecker;
        _checkApplicationExistenceService = checkApplicationExistenceService;
        _pendingOrInProgressApplicationStatus = pendingOrInProgressApplicationStatus;

    }

    public override async void ValidateRequest(CreateApplicationRequest request)
    {
        base.ValidateRequest(request);

        var application = await _checkApplicationExistenceService.CheckApplicationExistence(request);

        if (application != null)
        {
            _pendingOrInProgressApplicationStatus.CheckApplicationStatus((EnApplicationStatus)application.ApplicationStatus);
        }


        CreateLocalDrivingLicenseApplicationRequest localDrivingLicenseApplicationRequest = request as CreateLocalDrivingLicenseApplicationRequest


        if (!Enum.IsDefined(typeof(EnLicenseClasses), localDrivingLicenseApplicationRequest?.LicenseClassId))
            throw new DoesNotExistException("License class id does not exist");


        var firstLocalDrivingLicense = await _firstTimeChecker.IsFirstTime(localDrivingLicenseApplicationRequest);

        if (firstLocalDrivingLicense == false)
            throw new AlreadyExistException("The applicant is Already has the license class ");

    }
}
