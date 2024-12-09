using System.ComponentModel.DataAnnotations;

namespace Models.ApplicationModels;

public class ApplicationType
{
    public byte Id { get; set; }
    [Required] public string Type { get; set; }
}
