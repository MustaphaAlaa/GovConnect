using Models.ApplicationModels;
using Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDTO.BookingDTOs;
public class BookingDTO
{

    [Required]
    public int BookinId { get; set; }

    /// <summary>
    /// The date when this field is recorded, in timestamp.
    /// </summary>
    [Required] public DateTime BookingDate { get; set; }

    /// <summary>
    /// The fees paid by the user for the booking.
    /// </summary>
    [Required] public decimal PaidFees { get; set; }

    /// <summary>
    /// The status of the booking.
    /// Pending, Completed, Canceled
    /// </summary>
    [Required] public string BookingStatus { get; set; } // its value must be in EnBookingStatus. 


    /// <summary>
    /// The appointment that was booked, it is a foreign key reference to the Appointment table.
    /// The field is required.
    /// </summary>
    [Required] public int AppointmentId { get; set; }

    /// <summary>
    /// Foreign key reference to the local driving license application table that appointment is booked for.
    /// the field is required.
    /// </summary>
    [Required] public int LocalDrivingLicenseApplicationId { get; set; }

    /// <summary>
    /// Foreign key reference to the test type table that appointment is booked for.
    /// the field is required.
    /// </summary>
    [Required] public int TestTypeId { get; set; }

    /// <summary>
    /// 
    /// </summary>

    public int? RetakeTestApplicationId { get; set; }



}
