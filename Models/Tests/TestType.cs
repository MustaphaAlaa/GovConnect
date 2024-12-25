using System.ComponentModel.DataAnnotations;

namespace Models.Tests;

public class TestType
{
    [Key]
    public int TestTypeId { get; set; }
    [Required] public string TestTypeTitle { get; set; }
    public string TestTypeDescription { get; set; }
    [Required] public decimal TestTypeFees { get; set; }
    
    public IList<Test> Tests { get; set; }
}