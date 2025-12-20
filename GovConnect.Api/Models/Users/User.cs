using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Models.ApplicationModels;
using Models.Drivers;
using Models.Countries;
using Models.Tests;

namespace Models.Users;


/// <summary>
/// User Information
/// </summary>
public class User : IdentityUser<Guid>
{
    [Required] public string FirstName { get; set; }
    [Required] public string SecondName { get; set; }
    [Required] public string ThirdName { get; set; }
    [Required] public string FourthName { get; set; }

    [Required] public string NationalNo { get; set; }
    [Required] public enGender Gender { get; set; }
    public string Address { get; set; }
    public string ImagePath { get; set; }

    [Required]
    [ForeignKey("CountryDTOs")]
    public int CountryId { get; set; }

    public DateTime BirthDate { get; set; }

    public Country Country { get; set; }
    public IEnumerable<Application> Applications { get; set; }
    public IEnumerable<Booking> Bookings { get; set; }
}
