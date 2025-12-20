using Models.Tests.Enums;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace IServices.IValidtors.IAppointments 
{
    /// <summary>
    /// This is the base class for validating the appointment.
    /// </summary> 
    public class ValidateAppointmentBase : IValidateAppointmentBase
    {
        /// <inheritdoc />
        public Task<DateTime> ValidateAppointment(int TypeId, string day)
        {
            if (!IsValidTestType(TypeId))
                throw new ArgumentException("Test Type does not exist.", nameof(TypeId));

            DateTime date;
            try
            {
                date = DateTime.ParseExact(day, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}", ex);
            }
            return Task.FromResult(date);
        }

        /// <summary>
        /// Validates the test type.
        /// </summary>
        /// <param name="TypeId">ID for Test Type.</param>
        /// <returns>Returns false if the test type ID is invalid.</returns>
        protected virtual bool IsValidTestType(int TypeId)
        {
            return Enum.IsDefined(typeof(EnTestTypes), TypeId);
        }
    }
}
