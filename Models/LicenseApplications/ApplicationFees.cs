using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.ApplicationModels;

namespace Models.ApplicationModels;

public class ApplicationFees
{
    [ForeignKey("ApplicationType")]
    [Required]
    public byte ApplicationTypeId { get; set; }

    [ForeignKey("ApplicationFor")]
    [Required]
    public short ApplicationForId { get; set; }

    [Required] public decimal Fees { get; set; }

    [Required] public DateTime LastUpdate { get; set; }

    public ApplicationType ApplicationType { get; set; }
    public ApplicationFor ApplicationFor { get; set; }
    public ICollection<LicenseApplication> Applications { get; set; }
}