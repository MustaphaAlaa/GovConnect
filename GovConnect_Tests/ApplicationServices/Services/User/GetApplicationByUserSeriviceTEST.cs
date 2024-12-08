using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.User;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Models.Users;
using Moq;
using Services.ApplicationServices.Services.UserAppServices;
using Services.Execptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GovConnect_Tests.ApplicationServices.Services.UserTests;

public class GetApplicationByUserSeriviceTEST
{

    private readonly Mock<IGetRepository<LicenseApplication>> _getRepository;
    private readonly Mock<IGetRepository<User>> _getUserRepository;

    private readonly IGetApplicationByUser _getApplication;

    public GetApplicationByUserSeriviceTEST()
    {
        _getRepository = new Mock<IGetRepository<LicenseApplication>>();
        _getUserRepository = new Mock<IGetRepository<Models.Users.User>>();

        _getApplication = new GetApplicationByUserService(_getRepository.Object,
                        _getUserRepository.Object);
    }

    [Fact]
    public async Task GetAsync_WhenApplicationIdIsInvalid_RetunsArgumentOutOfRangeException()
    {

        Func<Task> action = async () => await _getApplication.GetByAsync(new GetApplicationByUser(0, Guid.NewGuid()));


        await action.Should().ThrowAsync<ArgumentOutOfRangeException>();
    }

    [Fact]
    public async Task GetAsync_WhenApplicantUserIdIsInvalid_RetunsInvalidOperationException()
    {
        Func<Task> action = async () => await _getApplication.GetByAsync(new GetApplicationByUser(1, Guid.Empty));

        await action.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task GetAsync_WhenApplicantUserDoesNotExist_ThrowsDoesNotExistException()
    {

        _getUserRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(null as User);

        Func<Task> action = async () => await _getApplication.GetByAsync(new GetApplicationByUser(1, Guid.NewGuid()));



        await action.Should().ThrowAsync<DoesNotExistException>();
    }
    [Fact]
    public async Task GetAsync_WhenApplicationDoesNotExist_RetunsNull()
    {
        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<LicenseApplication, bool>>>()))
           .ReturnsAsync(null as LicenseApplication);

        _getUserRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(new User());

        var result = await _getApplication.GetByAsync(new GetApplicationByUser(1, Guid.NewGuid()));



        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAsync_WhenApplicationExists_RetunsApplicationObj()
    {

        LicenseApplication licenseApplication = new LicenseApplication()
        {
            Id = 2,
            ApplicantUserId = Guid.NewGuid(),
            ApplicationDate = DateTime.Now,
            PaidFees = 500
        };
        _getRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<LicenseApplication, bool>>>()))
            .ReturnsAsync(licenseApplication);

        _getUserRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
           .ReturnsAsync(new User());


        var result = await _getApplication.GetByAsync(new GetApplicationByUser(55, Guid.NewGuid()));


        result.Should().BeEquivalentTo(licenseApplication);
    }


}
