using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Models.ApplicationModels;
using Models.Users;

namespace Models.LicenseModels;

public class DetainedLicense
{
    [Key] public int Id { get; set; }

    [Required][ForeignKey("License")] public int LicenseId { get; set; }

    [Required] public DateTime DetainDate { get; set; }

    public decimal FineFees { get; set; }

    [Required][ForeignKey("CreatedBy")] public Guid CreatedByEmployee { get; set; }

    public bool IsReleased { get; set; }

    public DateTime ReleasedDate { get; set; }


    [ForeignKey("ReleasedBy")] public Guid ReleasedByEmployee { get; set; }

    [ForeignKey("Application")] public int ReleaseApplicationId { get; set; }

    public License License { get; set; }
    public Application Application { get; set; }
    public Employee ReleasedBy { get; set; }
    public Employee CreatedBy { get; set; }
}