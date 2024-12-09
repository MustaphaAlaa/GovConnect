namespace Models.ApplicationModels;

public enum ApplicationStatus
{
    Finalized = 1,  //  The LicenseApplication has been completed (could be used if necessary)
    InProgress,    //   The LicenseApplication is being processed
    Pending,      //     The LicenseApplication is waiting for further action or approval
    Rejected,    // The LicenseApplication has been declined
    Approved    // The LicenseApplication has been accepted
}