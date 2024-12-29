using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Tests;

namespace Models;

/// <summary>
/// Represents the Time Intervals
/// </summary>
[Table("TimeIntervals")]
public class TimeInterval
{
    /// <summary>
    /// The Unique identitfier for TimeInterval
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TimeIntervalId { get; set; }

    public EnHour Hour { get; set; }
    public EnHourStage Minute { get; set; }

    /// <summary>
    /// The Collection of appointments associated to the time interval, this is a Navigation property
    /// </summary>
    public IEnumerable<Appointment> Appointments { get; set; }

}