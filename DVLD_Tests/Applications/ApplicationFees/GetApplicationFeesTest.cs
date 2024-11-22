
using AutoMapper;
using IRepository;
using IServices.Application.Fees;
using Models.Applications;
using Moq;
using Services.Application;
using System.Linq.Expressions;

namespace DVLD_Tests.Applications;

public class GetApplicationFeesTest
{
    private readonly Mock<IMapper> _mapper;

    private readonly Mock<IGetRepository<ApplicationFees>> _getRepository;
    private readonly IGetApplicationFees _getApplicationFees;
    public GetApplicationFeesTest()
    {
        _mapper = new Mock<IMapper>();
        _getRepository = new Mock<IGetRepository<ApplicationFees>>();

        _getApplicationFees = new GetApplicationFeesService(_getRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetApplicationFees_FalseCondation_returnfalse()
    {


    }
}
