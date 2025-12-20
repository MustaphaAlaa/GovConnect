using System.ComponentModel.DataAnnotations;
using Models.License;
using Models.LicenseModels;

namespace ModelDTO.LicenseDTOs;

public class LocalDrivingLicenseDTO
{
  [Key] public int LocalDrivingLicenseId { get; set; }

  public string Notes { get; set; }

  public EnLicenseStatus LicenseStatus { get; set; } // 1: Active, 2: Expired, 3: Suspended, 4: Revoked, 5: Cancelled, 6: Detained

  [Required] public EnIssueReason IssueReason { get; set; } // 1: New, 2: Renewal, 3: Lost, 4: Damaged

  public DateTime IssuingDate { get; set; }
  public DateTime ExpiryDate { get; set; }
  public DateTime DateOfBirth { get; set; }


  [Required] public Guid CreatedByEmployee { get; set; }
  [Required] public int CountryId { get; set; }

  [Required] public int LocalDrivingLicenseApplicationId { get; set; }
  [Required] public Guid DriverId { get; set; }
  [Required] public short LicenseClassId { get; set; }
}