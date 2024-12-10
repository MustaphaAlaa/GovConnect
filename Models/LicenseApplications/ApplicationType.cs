using System.ComponentModel.DataAnnotations;

namespace Models.ApplicationModels;

//Maybe i'll move it to be generic with other applications
public class ApplicationType
{
    public byte Id { get; set; }
    [Required] public string Type { get; set; }
}
