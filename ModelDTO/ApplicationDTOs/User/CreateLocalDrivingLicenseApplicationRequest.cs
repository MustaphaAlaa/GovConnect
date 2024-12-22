using System.ComponentModel.DataAnnotations;
using Models.ApplicationModels;

namespace ModelDTO.ApplicationDTOs.User;

public class CreateLocalDrivingLicenseApplicationRequest : CreateApplicationRequest
{
    private short _serviceCategoryId;
    [Required] public short LicenseClassId { get; set; }

    public int ApplicationId { get; set; }
    public override short ServiceCategoryId
    {
        get { return _serviceCategoryId; }
        set { _serviceCategoryId = (short)EnServiceCategory.Local_Driving_License; }

    }
}