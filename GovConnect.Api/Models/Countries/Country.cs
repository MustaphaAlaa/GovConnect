using System.ComponentModel.DataAnnotations;
using Models.LicenseModels;
using Models.Users;

namespace Models.Countries;

public class Country
{
    [Key]
    public int CountryId { get; set; }
    [Required] public string CountryName { get; set; }
    [Required] public string CountryCode { get; set; }
    public IEnumerable<User> Users { get; set; }
    public IEnumerable<LocalDrivingLicense> localDrivingLicenses { get; set; }
}