using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.ApplicationModels;

/// <summary>
/// Represents the ServicesPurposes table in the database.
/// </summary>
public class ServicePurpose
{
    /// <summary>
    /// The unique identifier for the table.
    /// </summary>
    [Key]
    public byte ServicePurposeId { get; set; }

    /// <summary>
    /// The title of the service purpose.
    /// </summary>
    /// <example>New</example>
    /// <example>Renew</example>
    /// <remarks>All title list with ids in EnServicePurpose</remarks>
    [Required] public string Purpose { get; set; }

    /// <summary>
    /// Navigation property for applications.
    /// </summary>
    public IEnumerable<Application> Applications { get; set; }
}
