using System.ComponentModel.DataAnnotations;
using Models.Drivers;

namespace ModelDTO;

public class UserDTO
{
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] public string Username { get; set; }
    [Required] public enGender Gender { get; set; }

    public string ImagePath { get; set; }

    [Required] public string Email { get; set; }
    [Required] public string ConfirmEmail { get; set; }

    public string PhoneNumber { get; set; }

    [Required] public int CountryId { get; set; }
}