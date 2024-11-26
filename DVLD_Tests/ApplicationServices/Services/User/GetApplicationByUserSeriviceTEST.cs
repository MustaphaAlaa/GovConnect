using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.User;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Tests.ApplicationServices.Services.User;

public class GetApplicationByUserSeriviceTEST
{

    private readonly Mock<IGetRepository<Application>> _getRepository;

    private readonly IGetApplicationByUser _getApplication;

    public GetApplicationByUserSeriviceTEST()
    {
        _getRepository = new Mock<IGetRepository<Application>>();
        _getApplication = new GetApplicationByUserService(_getRepository.Object);
    }

    [Fact]
    public async Task GetAsync_WhenApplicationDoesNotExist_RetunsNull()
    {
        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<Application, bool>>>()))
           .ReturnsAsync(null as Application);

        var result = await _getApplication.GetByAsync(It.IsAny<Expression<Func<Application, bool>>>());


        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAsync_WhenApplicationExists_RetunsApplicationObj()
    {

        Application application = new Application()
        {
            Id = 2,
            ApplicantUserId = Guid.NewGuid(),
            ApplicationDate = DateTime.Now,
            PaidFees = 500
        };
        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<Application, bool>>>()))
            .ReturnsAsync(application);

        var result = await _getApplication.GetByAsync(It.IsAny<Expression<Func<Application, bool>>>());


        result.Should().BeEquivalentTo(application);
    }


}
