using System.ComponentModel.DataAnnotations;

namespace Models.ApplicationModels;

public class ApplicationFor
{
    [Key] public short Id { get; set; }
    [Required] public string For { get; set; }
}