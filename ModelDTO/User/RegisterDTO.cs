using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using Models.Drivers;
using Models.Types;

namespace ModelDTO.User;

public class RegisterDTO
{
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] public string Username { get; set; }

    [Required] public string NationalNo { get; set; }

    [Required] public enGender Gender { get; set; }

    public string Address { get; set; } = string.Empty;

    public string ImagePath { get; set; } = string.Empty;

    [Required] public string Email { get; set; }
    [Required][Compare("Email")] public string ConfirmEmail { get; set; }

    public string PhoneNumber { get; set; }

    [Required] public DateTime BirthDate { get; set; }

    [Required] public string Password { get; set; }
    [Required][Compare("Password")] public string ConfirmPassword { get; set; }

    [Required] public int CountryId { get; set; }
}


public class UpdateUserDTO
{
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }

    public string Username { get; set; }

    public string Address { get; set; }

    public string ImagePath { get; set; }

    [Required] public string Email { get; set; }
    public string ConfirmEmail { get; set; }

    public string PhoneNumber { get; set; }

    [Required] public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}