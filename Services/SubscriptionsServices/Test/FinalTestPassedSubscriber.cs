using AutoMapper;
using IRepository.ITestRepos;
using IServices.IApplicationServices.IServiceCategoryApplications.ILocalDrivingLicenseApplication;
using IServices.IDriverServices;
using IServices.ILicenseClassServices;
using IServices.ILicenseServices;
using IServices.ITests.ITest;
using IServices.IUserServices;
using IServices.IValidtors.ILocalDrivingLicenseApplications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelDTO.TestsDTO;
using ModelDTO.Users;
using Models.Countries;
using Models.License;
using Models.LicenseModels;
using Models.Users;
using Services.Exceptions;

namespace Services.SubscriptionsServices.Tests
{
    public class FinalTestPassedSubscriber : IFinalTestSubscriber
    {
        private readonly ITestCreationService _testCreationService;
        private readonly ILogger<FinalTestPassedSubscriber> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public FinalTestPassedSubscriber(ITestCreationService testCreationService,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<FinalTestPassedSubscriber> logger)
        {
            _testCreationService = testCreationService;
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _testCreationService.OnFinalTestPassed += OnFinalTestPassed;
        }

        // public void Subscribe()
        // {
        //     _testCreationService.OnFinalTestPassed += OnFinalTestPassed;
        // }

        public async Task OnFinalTestPassed(object? sender, TestDTO testDTO)
        {
            _logger.LogInformation($"{nameof(OnFinalTestPassed)} method in {this.GetType().Name} called.");

            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var driverRetriever = scope.ServiceProvider.GetRequiredService<IDriverRetrieveService>();
                    var driverCreator = scope.ServiceProvider.GetRequiredService<IDriverCreatorService>();
                    var LDLCreation = scope.ServiceProvider.GetRequiredService<ILocalDrivingLicenseCreationService>();
                    var testResultInfoRetrieve = scope.ServiceProvider.GetRequiredService<ITestResultInfoRetrieve>();
                    var localDrivingLicenseApplicationRetrieve = scope.ServiceProvider.GetRequiredService<ILocalDrivingLicenseApplicationRetrieve>();
                    var userRetrieverInfo = scope.ServiceProvider.GetRequiredService<IUserRetrieveService>();
                    var LicenseClassRetrieve = scope.ServiceProvider.GetRequiredService<ILicenseClassRetrieve>();


                    var testResultInfo = testResultInfoRetrieve.GetTestResultInfo(testDTO.LocalDrivingLicenseApplicationId, testDTO.TestTypeId)
                    .FirstOrDefault(tr => tr.Test.TestResult);

                    if (testResultInfo is null)
                    {
                        _logger.LogError($"{nameof(OnFinalTestPassed)} method in {this.GetType().Name} -- test result info not found.");
                        throw new DoesNotExistException();
                    }

                    DriverDTO driver = await driverRetriever.GetByAsync(d => d.UserId == testResultInfo.Booking.UserId);

                    if (driver == null)
                    {
                        _logger.LogInformation($"{nameof(OnFinalTestPassed)} method in {this.GetType().Name} -- Driver is not found new one will created.");
                        Driver newDriver = new Driver
                        {
                            UserId = testResultInfo.Booking.UserId,
                            CreatedByEmployee = testResultInfo.Test.CreatedByEmployeeId
                        };

                        driver = await driverCreator.CreateAsync(newDriver);
                    }


                    var LDLApplication = await localDrivingLicenseApplicationRetrieve
                                                .GetByAsync(ldl => ldl.Id == testResultInfo.Booking.LocalDrivingLicenseApplicationId);

                    if (LDLApplication == null)
                    {
                        _logger.LogError($"{this.GetType().Name} -- OnFinalTestPassed -- local driving license application not found.");
                        return;
                        throw new DoesNotExistException();
                    }
                    var user = await userRetrieverInfo.GetByAsync(usr => usr.Id == testResultInfo.Booking.UserId);

                    var licenseClass = await LicenseClassRetrieve.GetByAsync(lc => lc.LicenseClassId == LDLApplication.LicenseClassId);

                    if (licenseClass is null)
                    {
                        _logger.LogError($"{this.GetType().Name} -- OnFinalTestPassed -- license class not found.");
                        throw new DoesNotExistException();
                    }

                    LocalDrivingLicense newLocalDrivingLicense = new LocalDrivingLicense
                    {
                        DriverId = driver.DriverId,
                        LocalDrivingLicenseApplicationId = testDTO.LocalDrivingLicenseApplicationId,
                        CreatedByEmployee = testResultInfo.Test.CreatedByEmployeeId,
                        CountryId = user.CountryId,
                        LicenseClassId = LDLApplication.LicenseClassId,
                        DateOfBirth = user.BirthDate,
                        IssuingDate = DateTime.Now,
                        ExpiryDate = DateTime.Now.AddMonths(licenseClass.DefaultValidityLengthInMonths),
                        Notes = testDTO.Notes,
                        IssueReason = EnIssueReason.New,
                        LicenseStatus = EnLicenseStatus.Active
                    };

                    var LDL = await LDLCreation.CreateAsync(newLocalDrivingLicense);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name} -- OnFinalTestPassed -- {ex.Message}");
                throw;
            }
        }
    }
}