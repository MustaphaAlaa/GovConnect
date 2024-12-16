    using System.ComponentModel.DataAnnotations;
    using Models.Users;

    namespace Models.Types;

public class Country
{   
    [Key]
    public int CountryId { get; set; }
    [Required] public string CountryName { get; set; }
    public List<User> Users { get; set; }
}