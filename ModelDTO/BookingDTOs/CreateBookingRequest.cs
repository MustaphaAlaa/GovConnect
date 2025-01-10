using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDTO.BookingDTOs
{
    public class CreateBookingRequest
    {
        /// <summary>
        /// The fees paid by the user for the booking.
        /// </summary>
        [Required]
        public decimal PaidFees { get; set; }

        /// <summary>
        /// The user who made the booking.
        /// It is a foreign key reference to the User table.
        /// The field is required.
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// The appointment that was booked, it is a foreign key reference to the Appointment table.
        /// The field is required.
        /// </summary>
        [Required]
        public int AppointmentId { get; set; }

        /// <summary>
        /// Foreign key reference to the local driving license application table that appointment is booked for.
        /// The field is required.
        /// </summary>
        [Required]
        public int LocalDrivingLicenseApplicationId { get; set; }

        /// <summary>
        /// Foreign key reference to the application table for retake test application.
        /// </summary>
        public int? RetakeTestApplicationId { get; set; }
    }
}
