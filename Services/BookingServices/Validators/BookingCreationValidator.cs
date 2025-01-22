using IRepository.ITestRepos;
using IServices.IAppointments;
using IServices.IBookingServices;
using IServices.ILicencesServices;
using IServices.IValidators;
using IServices.IValidators.BookingValidators;
using Microsoft.Extensions.Logging;
using ModelDTO.BookingDTOs;
using Services.Exceptions;

namespace Services.BookingServices.Validators
{
    public class BookingCreationValidator : IBookingCreationValidators
    {
        private readonly ITestTypeOrder _testOrder;
        private readonly ITestTypePassedChecker _IsTestTypePassed;
        private readonly ILocalLicenseRetrieveService _lDLRetrievalService;
        private readonly IGetAppointmentService _getAppointmentService;
        private readonly ILogger<BookingCreationValidator> _logger;

        public BookingCreationValidator(ITestTypeOrder testOrder,
            ITestTypePassedChecker isTestTypePassed,
            ILocalLicenseRetrieveService getLocalLicense,
            IGetAppointmentService getAppointmentService,
            IFirstTimeBookingAnAppointmentValidation firstTimeBookingAnAppointment,
        IRetakeTestApplicationBookingValidator retakeTestApplicationValidator,
        ILogger<BookingCreationValidator> logger)
        {
            _testOrder = testOrder;
            _IsTestTypePassed = isTestTypePassed;
            _lDLRetrievalService = getLocalLicense;
            _getAppointmentService = getAppointmentService;
            _logger = logger;
        }

        public async Task IsValid(CreateBookingRequest request, IBookingCreationTypeValidation bookingTypeValidation)
        {
            try
            {
                var LDL = await _lDLRetrievalService.GetByAsync(ldl => ldl.LocalDrivingLicenseApplicationId == request.LocalDrivingLicenseApplicationId);

                if (LDL != null)
                {
                    _logger.LogError($"The local driving license is already exists. ");

                    throw new AlreadyExistException("The local driving license is already exists.");
                }

                var testpassed = await _IsTestTypePassed.IsTestTypePassed(request.LocalDrivingLicenseApplicationId, request.TestTypeId);

                if (testpassed)
                {
                    _logger.LogError($"This test type is already exist and passed.");
                    throw new AlreadyExistException("This test type is already exist and passed.");
                }

                await _testOrder.CheckTheOrder(request);

                var appointment = await _getAppointmentService.GetByAsync(appointment => appointment.AppointmentId == request.AppointmentId);

                if (appointment == null)
                {
                    _logger.LogError($"Appointment does not exists.");
                    throw new AlreadyExistException(" appointment does not exists");
                }

                if (!appointment.IsAvailable)
                {
                    _logger.LogError($"Appointment is not available.");
                    throw new InvalidRequestException("appointment is not available.");
                }

                if (appointment.AppointmentDay < DateOnly.FromDateTime(DateTime.Now))
                {
                    string logMsg = $"Appointment is Outdated.";
                    _logger.LogError(logMsg);
                    throw new ArgumentOutOfRangeException(logMsg);
                }

                await bookingTypeValidation.Validate(request);

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);
                throw;
            }
        }
    }


}

