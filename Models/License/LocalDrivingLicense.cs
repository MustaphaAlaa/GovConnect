using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Countries;
using Models.Users;
using Models.License;

namespace Models.LicenseModels;

public class LocalDrivingLicense
{
    [Key] public int LocalDrivingLicenseId { get; set; }

    public string Notes { get; set; }

    public EnLicenseStatus LicenseStatus { get; set; } // 1: Active, 2: Expired, 3: Suspended, 4: Revoked, 5: Cancelled, 6: Detained

    public byte IssueReason { get; set; } // 1: New, 2: Renewal, 3: Lost, 4: Damaged

    public DateTime IssuingDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public DateTime DateOfBirth { get; set; }


    [Required][ForeignKey("Employee")] public Guid CreatedByEmployee { get; set; }
    [Required][ForeignKey("Country")] public int CountryId { get; set; }
    [Required]
    [ForeignKey("DrivingLicenseApplication")]
    public int ApplicationId { get; set; }
    [Required][ForeignKey("Driver")] public Guid DriverId { get; set; }
    [Required]
    [ForeignKey("LicenseClass")]
    public short LicenseClassId { get; set; }

    public Country Country { get; set; }
    public Employee Employee { get; set; }
    public Driver Driver { get; set; }
    public LocalDrivingLicense DrivingLicenseApplication { get; set; }
    public LicenseClass LicenseClass { get; set; }
}