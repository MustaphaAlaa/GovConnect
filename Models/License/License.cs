using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using Models.Types;
using Models.Users;
using Models.ApplicationModels;

namespace Models.LicenseModels;

public class License
{
    [Key] public int Id { get; set; }

    [Required] [ForeignKey("Application")] public int ApplicationId { get; set; }

    [Required] [ForeignKey("Driver")] public Guid DriverId { get; set; }

    [Required]
    [ForeignKey("LicenseClass")]
    public int LicenseClassId { get; set; }

    public string Notes { get; set; }

    public bool IsActive { get; set; }

    public byte IssueReason { get; set; }

    public DateTime IssuingDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public DateTime DateOfBirth { get; set; }


    [Required] [ForeignKey("Employee")] public Guid CreatedByEmployee { get; set; }
    [Required] [ForeignKey("Country")] public string LocalCountryName { get; set; }
    public Country Country { get; set; }

    public Employee Employee { get; set; }
    public Driver Driver { get; set; }
    public Application application { get; set; }
    public LicenseClass LicenseClass { get; set; }
}