using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using Models.Drivers;
using Models.Types;

namespace ModelDTO;

public class RegisterDTO
{
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }

    [Required] public string NationalNo { get; set; }

    [Required] public enGender Gender { get; set; }

    public string Address { get; set; }

    public string ImagePath { get; set; }

    [Required] public string Email { get; set; }
    [Required] public string ConfirmEmail { get; set; }

    public string PhoneNumber { get; set; }

    [Required] public string Password { get; set; }
    [Required] public string ConfirmPassword { get; set; }

    [Required] public int CountryId { get; set; }
}


public class UpdateUserDTO
{
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
 
    
    public string Address { get; set; }

    public string ImagePath { get; set; }

    [Required] public string Email { get; set; }
    [Required] public string ConfirmEmail { get; set; }

    public string PhoneNumber { get; set; }

    [Required] public string Password { get; set; }
    [Required] public string ConfirmPassword { get; set; }

     
    
}