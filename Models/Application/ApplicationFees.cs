using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Applications;

namespace Models.Applications;

public class ApplicationFees
{
    [ForeignKey("ApplicationType")]
    [Required]
    public int ApplicationTypeId { get; set; }

    [ForeignKey("ApplicationFor")]
    [Required]
    public int ApplicationForId { get; set; }

    [Required] public decimal Fees { get; set; }

    public ApplicationType ApplicationType { get; set; }
    public ApplicationFor ApplicationFor { get; set; }
    public ICollection<Application> Applications { get; set; }
}