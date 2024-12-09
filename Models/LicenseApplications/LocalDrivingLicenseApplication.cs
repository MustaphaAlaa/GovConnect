using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Types;

namespace Models.ApplicationModels;

public class LocalDrivingLicenseApplication
{
    [Key] public int Id { get; set; }

    [Required]
    [ForeignKey("LicenseApplication")]
    public int ApplicationId { get; set; }

    [Required]
    [ForeignKey("LicenseClass")]
    public int LicenseClassId { get; set; }

    public LicenseApplication  LicenseApplication  { get; set; }
    public LicenseClass LicenseClass { get; set; }
}