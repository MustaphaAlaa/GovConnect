using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.ApplicationModels;

namespace Models.LicenseModels;

public class InternationalDrivingLicense
{
    [Key] public int InternationalDrivingLicenseId { get; set; }
 
    // The international driving license DrivingLicenseApplication contains the passport
    [Required]
    [ForeignKey("InternationalDrivingLicenseApplication")]
    public int InternationalDrivingLicenseApplicationID { get; set; }
 
    [Required]
    public int LicenseClassId
    {
        get => LocalDrivingLicense.LicenseClass.LicenseClassId; 
        private set => value = LocalDrivingLicense.LicenseClass.LicenseClassId;
    }

    [Required] [ForeignKey("LocalDrivingLicense")] public int LocalDrivingLicenseId { get; set; }

    public InternationalDrivingLicenseApplication InternationalDrivingLicenseApplication { get; set; }
    public LocalDrivingLicense LocalDrivingLicense { get; set; }
}