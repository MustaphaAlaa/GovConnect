using System.ComponentModel.DataAnnotations;

namespace Models.ApplicationModels;

//Maybe i'll move it to be generic with other applications
public class ApplicationPurpose
{
    public byte ApplicationPurposeId { get; set; }
    [Required] public string Purpose { get; set; }
}
