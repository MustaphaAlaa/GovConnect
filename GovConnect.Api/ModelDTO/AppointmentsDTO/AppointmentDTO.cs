using Models.Tests;
using System.ComponentModel.DataAnnotations;

namespace ModelDTO.Appointments;

public class AppointmentDTO
{
    /// <summary>
    /// The unique identifier for appointment.
    /// </summary>
    [Key] public int AppointmentId { get; set; }

    /// <summary>
    /// The foreign key referencing the associated Test Type.
    /// The field is required.
    /// </summary>
    [Required] public int TestTypeId { get; set; }

    /// <summary>
    /// The foreign key referencing the associated Time Interval.
    /// The field is required.
    /// </summary> 
    [Required] public int TimeIntervalId { get; set; }

    /// <summary>
    /// Indiactes whether this apponitment is available for booking.
    /// true if available, false if passed or booked.
    /// </summary>
    public bool IsAvailable { get; set; }

    /// <summary>
    /// The specific day of the appointment.
    /// </summary>
    [RegularExpression(@"^\d{2}/\d{2}/\d{4}", ErrorMessage = "Invalid Date Fromat")]
    public DateOnly AppointmentDay { get; set; }

    public override bool Equals(object? obj)
    {
        AppointmentDTO appointmentDTO = obj as AppointmentDTO;

        if (appointmentDTO == null) return false;

        return appointmentDTO.TimeIntervalId == this.TimeIntervalId &&
               appointmentDTO.AppointmentId == this.AppointmentId &&
               appointmentDTO.IsAvailable == this.IsAvailable &&
               appointmentDTO.TestTypeId == this.TestTypeId;
    }

}