using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.ApplicationModels;

/// <summary>
/// Represents the ServicesCategories table in the database.
/// </summary>
public class ServiceCategory
{
    /// <summary>
    /// The unique identifier for the table.
    /// </summary>
    [Key]
    public short ServiceCategoryId { get; set; }

    /// <summary>
    /// The title of the service category.
    /// </summary>
    /// <example>Local driving license</example>
    /// <example>International driving license</example>
    /// <remarks>All title list with ids in EnServiceCategory</remarks>
    [Required] public string Category { get; set; }

    /// <summary>
    /// Navigation property for applications.
    /// </summary>
    public IEnumerable<Application> Applications { get; set; }
}