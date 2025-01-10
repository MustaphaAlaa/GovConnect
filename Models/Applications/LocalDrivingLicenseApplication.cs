using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.LicenseModels;
using Models.Types;

namespace Models.ApplicationModels;

/// <summary>
/// Represents a local driving license application.
/// </summary>
public class LocalDrivingLicenseApplication
{
    /// <summary>
    /// The unique identifier for the local driving license application.
    /// </summary>
    [Key] public int Id { get; set; }

    /// <summary>
    /// The foreign key referencing the associated application.
    /// </summary>
    [Required]
    [ForeignKey("Application")]
    public int ApplicationId { get; set; }

    /// <summary>
    /// The foreign key referencing the associated license class.
    /// </summary>
    [Required]
    [ForeignKey("LicenseClass")]
    public short LicenseClassId { get; set; }

    /// <summary>
    /// The reason for the application.
    /// </summary>
    [Required]
    public string ReasonForTheApplication { get; set; }

    /// <summary>
    /// Navigation property for the associated application.
    /// </summary>
    public Application Application { get; set; }

    /// <summary>
    /// Navigation property for the associated license class.
    /// </summary>
    public LicenseClass LicenseClass { get; set; }
}
