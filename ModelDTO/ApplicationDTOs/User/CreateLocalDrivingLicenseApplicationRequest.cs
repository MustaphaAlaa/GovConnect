using Models.ApplicationModels;

namespace ModelDTO.ApplicationDTOs.User;

public class CreateLocalDrivingLicenseApplicationRequest : CreateApplicationRequest
{
    private short _serviceCategoryId;

    public override short ServiceCategoryId
    {
        get { return _serviceCategoryId; }
        set { _serviceCategoryId = (short)EnServiceCategory.LocalLicense; }

    }
}