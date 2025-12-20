using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.ApplicationModels;
using Models.Users;

namespace Models.Tests;

/// <summary>
/// Tracks actual booked Appointments made by users
/// </summary> 

[Table("Bookings")]
public class Booking
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookingId { get; set; }

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
    /// The user who made the booking.
    /// it is a foreign key reference ot the User table.
    /// The field is required.
    /// </summary>
    [ForeignKey("User")]
    [Required]
    public Guid UserId { get; set; }

    /// <summary>
    /// The appointment that was booked, it is a foreign key reference to the Appointment table.
    /// The field is required.
    /// </summary>
    [ForeignKey("Appointment")][Required] public int AppointmentId { get; set; }
    [ForeignKey("TestType")][Required] public int TestTypeId { get; set; }

    /// <summary>
    /// Foreign key reference to the local driving license application table that appointment is booked for.
    /// the field is required.
    /// </summary>
    [ForeignKey("LocalDrivingLicenseApplication")][Required] public int LocalDrivingLicenseApplicationId { get; set; }

    /// <summary>
    /// 
    /// </summary>

    [ForeignKey("Application")] public int? RetakeTestApplicationId { get; set; }

    public User User { get; set; }
    public LocalDrivingLicenseApplication LocalDrivingLicenseApplication { get; set; }
    public TestType TestType { get; set; }
    public Appointment Appointment { get; set; }
    public Application Application { get; set; }

}
