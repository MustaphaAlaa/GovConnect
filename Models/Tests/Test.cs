using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Users;

namespace Models.Tests;

/// <summary>
/// Represents the test's result and all information associted with the result   
/// </summary>
[Table("Tests")]
public class Test
{
    /// <summary>
    /// the unique identifier for the test
    /// </summary>
    [Key] public int TestId { get; set; }

    /// <summary>
    /// The result of the test, it's onlt can be true (for pass) or false (for failure)
    /// The field is required
    /// </summary>
    [Required] public bool TestResult { get; set; }

    /// <summary>
    /// The forign key for the booked appointment
    /// The field is required
    /// </summary>
    [ForeignKey("Booking")][Required] public int BookingId { get; set; }

    /// <summary>
    /// if there any notes about the result
    /// The field is optional
    /// </summary>
    public string Notes { get; set; }

    /// <summary>
    /// The Employee who create the result
    /// </summary>
    [Required][ForeignKey("Employee")] public Guid CreatedByEmployee { get; set; }

    /// <summary>
    /// Navigation property for CreatedByEmployee
    /// </summary>
    public Employee Employee { get; set; }

    ///<summary>
    /// Navigation Property for BookingId
    ///</summary>
    public Booking Booking { get; set; }
}