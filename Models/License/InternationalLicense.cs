using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.ApplicationModels;

namespace Models.LicenseModels;

public class InternationalLicense
{
    [Key] public int Id { get; set; }
 
    // The international driving license LicenseApplication contains the passport
    [Required] [ForeignKey("LicenseApplication")] public int InternationalDrivingLicenseApplicationID { get; set; }


    [Required]
    public int LicenseClassId
    {
        get => LocalLicense.LicenseClass.LicenseClassId;

        private set => value = LocalLicense.LicenseClass.LicenseClassId;
    }

    [Required] [ForeignKey("LocalLicense")] public int LicenseId { get; set; }

    public InternationalDrivingLicenseApplication InternationalDrivingLicenseApplication { get; set; }
    public LocalLicense LocalLicense { get; set; }
}