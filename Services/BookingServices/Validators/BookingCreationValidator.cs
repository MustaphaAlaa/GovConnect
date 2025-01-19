using AutoMapper;
using IRepository.ISFs;
using IServices.ILicencesServices;
using IServices.IValidators;
using IServices.IValidators.BookingValidators;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Logging;
using ModelDTO.BookingDTOs;
using Services.Execptions;

namespace Services.BookingServices.Validators
{
    public class BookingCreationValidator : IBookingCreationValidators
    {
        private readonly ITestTypeOrder _testOrder;
        private readonly ISF_IsTestTypePassed _IsTestTypePassed;
        private readonly ILocalLicenseRetrieveService _lDLRetrievalService;
        private readonly ILogger<BookingCreationValidator> _logger;

        public BookingCreationValidator(ITestTypeOrder testOrder,
            ISF_IsTestTypePassed isTestTypePassed,
            ILocalLicenseRetrieveService getLocalLicense,
            ILogger<BookingCreationValidator> logger)
        {
            _testOrder = testOrder;
            _IsTestTypePassed = isTestTypePassed;
            _lDLRetrievalService = getLocalLicense;
            _logger = logger;
        }

        public async Task IsValid(CreateBookingRequest request)
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
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);
                throw;
            }
        }
    }


}


