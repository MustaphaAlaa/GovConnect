using System.ComponentModel.DataAnnotations;

namespace Models.Applications;

public class ApplicationFor
{
    public int Id { get; set; }
    [Required] public string For { get; set; }
}