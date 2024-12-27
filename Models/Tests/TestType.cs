using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Tests;

/// <summary>
/// Represent the types of tests in the system.
/// </summary>
[Table("TestTypes")]
public class TestType
{
    /// <summary>
    /// The unique identifier for test type.
    /// </summary>
    [Key] public int TestTypeId { get; set; }

    /// <summary>
    /// The name of the test type.
    /// The field is required.
    /// </summary>
    [Required] public string TestTypeTitle { get; set; }

    /// <summary>
    /// For description or additional information about the test type.
    /// </summary>
    public string? TestTypeDescription { get; set; }

    /// <summary>
    /// The fees for this test's type
    /// </summary>
    [Required] public decimal TestTypeFees { get; set; }

    /// <summary>
    /// The Collection of appointments for test's type, this is navigation property.
    /// </summary> 
    public IEnumerable<Appointment> Appointments { get; set; }
}