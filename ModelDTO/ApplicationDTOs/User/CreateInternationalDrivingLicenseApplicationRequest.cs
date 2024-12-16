using System.ComponentModel.DataAnnotations;
using Models.ApplicationModels;

namespace ModelDTO.ApplicationDTOs.User;

public class CreateInternationalDrivingLicenseApplicationRequest : CreateApplicationRequest
{
    [Required] public Guid UserId { get; set; }
    [Required] public Guid DriverId { get; set; }

    private short _applicationForId;

    public override short ServiceCategoryId
    {
        get { return _applicationForId; }
        set { _applicationForId = (short)EnServiceCategory.InternationalLicense; }
    }

    //PassprotData....
    
    [Required] public short LicenseClassId { get; set; }
}