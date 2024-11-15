using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Models.Drivers;
using Models.Types;

namespace Models.Users;

public class User : IdentityUser<Guid>
{
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] public string NationalNo { get; set; }
    [Required] public enGender Gender { get; set; }
    public string Address { get; set; }
    public string ImagePath { get; set; }
    
    [Required]
    [ForeignKey("Country")]
    public int CountryId { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public Country Country { get; set; }
}
