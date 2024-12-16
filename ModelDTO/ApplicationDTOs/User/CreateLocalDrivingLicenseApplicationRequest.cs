using Models.ApplicationModels;

namespace ModelDTO.ApplicationDTOs.User;

public class CreateLocalDrivingLicenseApplicationRequest : CreateApplicationRequest
{
    private short _applicationForId;

    public override short ApplicationForId
    {
        get { return _applicationForId; }
        set { _applicationForId = (short)EnApplicationFor.LocalLicense; }

    }
}