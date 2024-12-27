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
    public int BookinId { get; set; }

    [Required] public DateTime BookingDate { get; set; }

    [Required] public decimal PaidFees { get; set; }

    [Required] public string BookingStatus { get; set; } // its value must be in EnBookingStatus

    [ForeignKey("User")]
    [Required]
    public Guid UserId { get; set; }

    [ForeignKey("Appointment")][Required] public int AppointmentId { get; set; }

    [ForeignKey("LocalDrivingLicenseApplication")][Required] public int LocalDrivingLicenseApplicationId { get; set; }


    [ForeignKey("Application")] public int RetakeTestApplicationId { get; set; }

    public User User { get; set; }
    public LocalDrivingLicenseApplication LocalDrivingLicenseApplication { get; set; }
    public Appointment Appointment { get; set; }
    public Application Application { get; set; }

}
