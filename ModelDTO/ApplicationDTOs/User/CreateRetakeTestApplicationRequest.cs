using System.ComponentModel.DataAnnotations;
using Models.ApplicationModels;

namespace ModelDTO.ApplicationDTOs.User;

public class CreateRetakeTestApplicationRequest : CreateApplicationRequest
{
    private short _serviceCategoryId;
    private byte _servicePurposeId;

    [Required] public int TestTypeId { get; set; }

    public int ApplicationId { get; set; }
    public override short ServiceCategoryId
    {
        get { return _serviceCategoryId; }
        set { _serviceCategoryId = (short)EnServiceCategory.Local_Driving_License; }
    }
    public override byte ServicePurposeId
    {
        get { return _servicePurposeId; }
        set { _servicePurposeId = (byte)EnServicePurpose.Retake_Test; }
    }
}