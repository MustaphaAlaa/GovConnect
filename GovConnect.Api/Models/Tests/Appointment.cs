using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Tests;

namespace Models.Tests;


/// <summary>
/// Tracks all potential appointments (time slots) for each test type.
/// This class tracks availability and associated scheduling information.
/// </summary>
[Table("Appointments")]
public class Appointment
{
    /// <summary>
    /// The unique identifier for appointment.
    /// </summary>
    [Key] public int AppointmentId { get; set; }

    /// <summary>
    /// The foreign key referencing the associated Test Type.
    /// The field is required.
    /// </summary>
    [ForeignKey("TestType")][Required] public int TestTypeId { get; set; }

    /// <summary>
    /// The foreign key referencing the associated Time Interval.
    /// The field is required.
    /// </summary> 
    [ForeignKey("TimeInterval")][Required] public int TimeIntervalId { get; set; }

    /// <summary>
    /// Indiactes whether this apponitment is available for booking.
    /// true if available, false if passed or booked.
    /// </summary>
    public bool IsAvailable { get; set; }


    /// <summary>
    /// The specific day of the appointment.
    /// </summary>
    [Required]
    [RegularExpression(@"^\d{2}/\d{2}/\d{4}", ErrorMessage = "Invalid Date Fromat")]
    public DateOnly AppointmentDay { get; set; }

    /// <summary>
    /// The Navigation Property representing the time interval for this appointment.
    /// </summary>
    public TimeInterval TimeInterval { get; set; }


    /// <summary>
    /// The Navigation Property represnting the Test Type for this appointment.
    /// </summary>
    public TestType TestType { get; set; }
}