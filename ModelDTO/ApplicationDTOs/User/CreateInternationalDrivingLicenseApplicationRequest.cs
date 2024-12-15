using System.ComponentModel.DataAnnotations;
using Models.ApplicationModels;

namespace ModelDTO.ApplicationDTOs.User;

public class CreateInternationalDrivingLicenseApplicationRequest
{
    [Required] public Guid UserId { get; set; }
    [Required] public Guid DriverId { get; set; }
    [Required] public byte ApplicationTypeId { get; set; }
    public short ApplicationForId { get; private set; } = (short)EnApplicationFor.InternationalLicense;
    [Required] public short LicenseClassId  { get; set; }

}