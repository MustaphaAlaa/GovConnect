using ModelDTO.BookingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices.IValidators;

/// <summary>
/// Interface for validating existing RetakeTestApplication.
/// </summary>
public interface IRetakeTestApplicationValidator
{
    /// <summary>
    /// Validates the specified retake test application.
    /// </summary>
    /// <param name="retakeTestApplication">The retake test application to validate.</param>
    /// <returns>A task that represents the asynchronous validation operation.</returns>
    Task Validate(CreateBookingRequest retakeTestApplication);
}
