using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.ApplicationModels;

namespace Models.LicenseModels;

public class InternationalLicense
{
    [Key] public int Id { get; set; }
 
    // The international driving license application contains the passport
    [Required] [ForeignKey("application")] public int InternationalDrivingLicenseApplication { get; set; }


    [Required]
    public int LicenseClassId
    {
        get => License.LicenseClass.LicenseClassId;

        private set => value = License.LicenseClass.LicenseClassId;
    }

    [Required] [ForeignKey("License")] public int LicenseId { get; set; }

    public InternationalDrivingLicenseApplication application { get; set; }
    public License License { get; set; }
}