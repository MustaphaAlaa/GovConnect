using System.ComponentModel.DataAnnotations;

namespace Models.LicenseModels;

public class LicenseType
{
    [Key] public byte LicenseTypeId { get; set; }
    [Required] public string Title { get; set; } // Matches the enum names for consistency
    [Required] public decimal Fees { get; set; }
}
