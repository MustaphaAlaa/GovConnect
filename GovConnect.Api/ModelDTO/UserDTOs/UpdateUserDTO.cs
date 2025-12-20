using System.ComponentModel.DataAnnotations;

namespace ModelDTO.Users;

public class UpdateUserDTO
{


    public Guid Id { get; set; }
    [Required] public string FirstName { get; set; }
    [Required] public string SecondName { get; set; }
    public string ThirdName { get; set; }
    public string FourthName { get; set; }

    public string Username { get; set; }

    public string Address { get; set; }

    public string ImagePath { get; set; }

    [Required] public string Email { get; set; }
    public string ConfirmEmail { get; set; }

    public string PhoneNumber { get; set; }

    [Required] public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}