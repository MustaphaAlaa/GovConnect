namespace IServices.IValidtors.IAppointments 
{
    /// <summary>
    /// This is the base class for validating the appointment.
    /// </summary> 
    public interface IValidateAppointmentBase
    {
        /// <summary>
        /// This method validates the appointment by checking the test type and date.
        /// </summary>
        /// <param name="TypeId">ID for Test Type.</param>
        /// <param name="day">The day of the appointment.</param>
        /// <returns>Returns the date as a DateTime object.</returns>
        /// <exception cref="ArgumentException">Thrown when the test type ID is invalid.</exception>
        /// <exception cref="FormatException">Thrown when the date format is invalid.</exception>
        public Task<DateTime> ValidateAppointment(int TypeId, string day);

    }
}
