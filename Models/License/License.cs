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

    [Required][ForeignKey("Application")] public int ApplicationId { get; set; }

    [Required][ForeignKey("Driver")] public Guid DriverId { get; set; }

    [Required]
    [ForeignKey("LicenseClass")]
    public int LicenseClassId { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public string Notes { get; set; }

    public decimal PaidFees { get; set; }

    public bool IsActive { get; set; }

    public byte IssueReason { get; set; }

    [Required]
    [ForeignKey("Employee")]
    public Guid CreatedByEmployee { get; set; }

    public Employee Employee { get; set; }
    public Driver Driver { get; set; }
    public Application application { get; set; }
    public LicenseClass LicenseClass { get; set; }
}
