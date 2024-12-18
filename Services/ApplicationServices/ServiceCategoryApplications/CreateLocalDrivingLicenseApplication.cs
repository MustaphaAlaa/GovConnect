using AutoMapper;
using IRepository;
using IServices.Application.IServiceCategoryApplications;
using IServices.IApplicationServices.User;
using IServices.ILicencesServices;
using ModelDTO.ApplicationDTOs.User;
using ModelDTO.LicenseDTOs;
using Models.ApplicationModels;
using Models.LicenseModels;
using Services.ApplicationServices.Services.UserAppServices;
using Services.Execptions;

namespace Services.ApplicationServices.ServiceCategoryApplications;

public class CreateLocalDrivingLicenseApplicationService : ICreateLocalDrivingLicenseApplicationService
{
    public CreateLocalDrivingLicenseApplicationService(ICreateApplicationService createApplicationService, ICreateRepository<LocalDrivingLicenseApplication> localDrivingLicenseApplicationRepository)
    {
        _createApplicationService = createApplicationService;
        _localDrivingLicenseApplicationRepository = localDrivingLicenseApplicationRepository;
        _validator = new CreateLocalDrivingLicenseApplicationValidator();
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


public class CreateLocalDrivingLicenseApplication : ICreateLocalDrivingLicenseApplication
{
    public CreateLocalDrivingLicenseApplication(ICreateApplicationService createApplicationService,
        ICreateRepository<LocalDrivingLicenseApplication> localDrivingLicenseApplicationRepository,
        IMapper mapper)
    {
        _createApplicationService = createApplicationService;
        _localDrivingLicenseApplicationRepository = localDrivingLicenseApplicationRepository;
        _validator = new CreateLocalDrivingLicenseApplicationValidator();
        mapper = _mapper;
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

public class CreateLocalDrivingLicenseApplicationValidator : CreateApplicationServiceValidator
{
    private readonly ICreateApplicationService _createApplicationService;
    private readonly ICheckApplicationExistenceService _checkApplicationExistenceService;
    public readonly IGetLocalLicenseService _getLocalLicenseService;
    public CreateLocalDrivingLicenseApplicationValidator()
    {

    }

    public override async void ValidateRequest(CreateApplicationRequest request)
    {
        base.ValidateRequest(request);

        await _checkApplicationExistenceService.CheckApplicationExistence(request);

        CreateLocalDrivingLicenseApplicationRequest localDrivingLicenseApplicationRequest = request as CreateLocalDrivingLicenseApplicationRequest
                                                                                            ?? throw new InvalidOperationException();


        if (!Enum.IsDefined(typeof(EnLicenseClasses), localDrivingLicenseApplicationRequest?.LicenseClassId))
            throw new DoesNotExistException("License class id does not exist");
        //check if the applicant has already license class

        var ldl = _getLocalLicenseService.GetByAsync(license => license.);
        if (request.IsFirstTimeOnly)
        {

        }

    }
}