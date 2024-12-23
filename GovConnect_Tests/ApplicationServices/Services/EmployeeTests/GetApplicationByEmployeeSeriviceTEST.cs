using AutoMapper;
using FluentAssertions;
using IRepository;
using ModelDTO.ApplicationDTOs.Employee;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices.Services.EmployeeAppService;

using System.Linq.Expressions;


namespace GovConnect_Tests.ApplicationServices.Services.EmployeeTests;

public class GetApplicationByEmployeeSeriviceTEST
{

    private readonly Mock<IGetRepository<Application>> _getRepository;

    private readonly GetApplicationByEmployeeService _getApplication;

    private readonly Mock<IMapper> _mapper;

    public GetApplicationByEmployeeSeriviceTEST()
    {
        _mapper = new Mock<IMapper>();
        _getRepository = new Mock<IGetRepository<Application>>();
        _getApplication = new GetApplicationByEmployeeService(_getRepository.Object,
                                                             _mapper.Object);
    }



    [Fact]
    public async Task GetAsync_WhenApplicationDoesNotExist_RetunsNull()
    {
        //Arrange
        var expression = It.IsAny<Expression<Func<Application, bool>>>();

        _getRepository.Setup(temp => temp.GetAsync(expression))
           .ReturnsAsync(null as Application);

        //ACT
        var result = await _getApplication.GetByAsync(expression);

        //ASSERT
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAsync_WhenApplicationExists_RetunsApplicationObj()
    {

        Application application = new Application()
        {
            ApplicationId = 23,
            UserId = Guid.NewGuid(),
            PaidFees = 500,
            ServicePurposeId = 1,
            ServiceCategoryId = 2,
            ApplicationStatus = (byte)EnApplicationStatus.InProgress,
            ApplicationDate = new DateTime(2023, 1, 5),
            LastStatusDate = new DateTime(2023, 2, 5),
            UpdatedByEmployeeId = Guid.NewGuid()
        };

        var expression = It.IsAny<Expression<Func<Application, bool>>>();

        _getRepository.Setup(temp => temp.GetAsync(expression))
            .ReturnsAsync(application);

        _mapper.Setup(temp => temp.Map<ApplicationDTOForEmployee>(It.IsAny<Application>()))
            .Returns((Application source) => new ApplicationDTOForEmployee()
            {
                Id = source.ApplicationId,
                ApplicantUserId = source.UserId,
                ApplicationPurposeId = source.ServicePurposeId,
                ServiceCategoryId = source.ServiceCategoryId,
                PaidFees = source.PaidFees,
                ApplicationStatus = source.ApplicationStatus,
                ApplicationDate = source.ApplicationDate,
                LastStatusDate = source.LastStatusDate,
                UpdatedByEmployeeId = source.UpdatedByEmployeeId
            });

        var result = await _getApplication.GetByAsync(expression);

        result.Should().BeEquivalentTo(_mapper.Object.Map<ApplicationDTOForEmployee>(application));
    }


}
