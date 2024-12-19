using System.ComponentModel.DataAnnotations;

namespace Models.ApplicationModels;

//Maybe i'll move it to be generic with other applications
public class ServicePurpose
{
    public byte ServicePurposeId { get; set; }
    [Required] public string Purpose { get; set; }
}
