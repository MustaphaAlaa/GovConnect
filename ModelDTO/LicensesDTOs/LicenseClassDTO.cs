using System.ComponentModel.DataAnnotations;

namespace ModelDTO.LicenseDTOs;

public class LicenseClassDTO
{
    [Key]
    public short LicenseClassId { get; set; }
    [Required] public string ClassName { get; set; }
    [Required] public string ClassDescription { get; set; }
    [Range(18, 21)][Required] public byte MinimumAllowedAge { get; set; } = 18;
    public ushort DefaultValidityLengthInMonths { get; set; } = 12;
    [Required] public decimal LicenseClassFees { get; set; }

 
}