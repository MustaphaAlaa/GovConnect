using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.ApplicationModels;

namespace Models.ApplicationModels;

public class ServiceFees
{
    [ForeignKey("ServicePurpose")]
    [Required]
    public byte ApplicationTypeId { get; set; }

    [ForeignKey("ServiceCategory")]
    [Required]
    public short ServiceCategoryId { get; set; }

    [Required] public decimal Fees { get; set; }

    [Required] public DateTime LastUpdate { get; set; }

    public ServicePurpose ApplicationPurpose { get; set; }
    public ServiceCategory ServiceCategory { get; set; }
    public ICollection<Application> Applications { get; set; }
}