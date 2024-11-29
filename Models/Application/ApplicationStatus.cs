namespace Models.ApplicationModels;

public enum ApplicationStatus
{
    Finalized = 1,  //  The application has been completed (could be used if necessary)
    InProgress,    //   The application is being processed
    Pending,      //     The application is waiting for further action or approval
    Rejected,    // The application has been declined
    Approved    // The application has been accepted
}