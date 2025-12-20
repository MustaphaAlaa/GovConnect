using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.ApplicationModels;
using Models.LicenseModels;
using Models.Users;

namespace ModelDTO.LicenseDTOs;
public class DetainedLicenseDTO
{
    public int DetainedLicenseId { get; set; }

    [Required][ForeignKey("Local_Driving_License")] public int LicenseId { get; set; }

    [Required] public DateTime DetainDate { get; set; }

    public decimal FineFees { get; set; }
    public bool IsReleased { get; set; }
    public DateTime ReleasedDate { get; set; }

    [ForeignKey("Application")] public int ReleaseApplicationId { get; set; }

    public LocalDrivingLicense LocalDrivingLicense { get; set; }
    public Application Application { get; set; }

}