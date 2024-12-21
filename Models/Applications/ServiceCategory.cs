using System.ComponentModel.DataAnnotations;

namespace Models.ApplicationModels;

public class ServiceCategory
{
    [Key] public short ServiceCategoryId { get; set; }
    [Required] public string Category { get; set; }
}