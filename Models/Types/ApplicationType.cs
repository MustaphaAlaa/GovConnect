using System.ComponentModel.DataAnnotations;

namespace Models;

public class ApplicationType
{
    public int ApplicationTypeId { get; set; }
  [Required]  public string ApplicationTitle { get; set; }
  [Required] public decimal ApplicationFees { get; set; }
}