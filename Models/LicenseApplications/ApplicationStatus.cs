namespace Models.ApplicationModels;

public enum ApplicationStatus
{
    Finalized = 1,  //  The DrivingLicenseApplication has been completed (could be used if necessary)
    InProgress,    //   The DrivingLicenseApplication is being processed
    Pending,      //     The DrivingLicenseApplication is waiting for further action or approval
    Rejected,    // The DrivingLicenseApplication has been declined
    Approved    // The DrivingLicenseApplication has been accepted
}