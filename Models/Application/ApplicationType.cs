using System.ComponentModel.DataAnnotations;

namespace Models.Applications;

public class ApplicationType
{
    public int Id { get; set; }
    [Required] public string Type { get; set; }
}