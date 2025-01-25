using IServices.IValidators.BookingValidators;
using ModelDTO.BookingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices.IValidators;

/// <summary>
/// Validates a specified retake test application before it used to book an appointment.
/// </summary>
public interface IRetakeTestApplicationBookingValidator : IBookingCreationTypeValidation
{

}
