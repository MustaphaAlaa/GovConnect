using System.ComponentModel.DataAnnotations;

namespace Models.Types;

public class LicenseClass
{
    [Key]
    public int LicenseClassId { get; set; }
    [Required] public string ClassName      { get; set; }
    [Required] public string ClassDescription      { get; set; }
    [Range(0, 21)] [Required] public byte MinimumAllowedAge { get; set; } = 18;
     public ushort DefaultValidityLengthInMonths { get; set; } = 12;
    [Required]   public decimal LicenseClassFees { get; set; }

    
}