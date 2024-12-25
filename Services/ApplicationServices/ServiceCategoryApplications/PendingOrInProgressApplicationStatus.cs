using IServices.IApplicationServices.User;
using Models.ApplicationModels;
using Services.Execptions;

namespace Services.ApplicationServices.ServiceCategoryApplications;

public class PendingOrInProgressApplicationStatus : IPendingOrInProgressApplicationStatus
{
    public void CheckApplicationStatus(EnApplicationStatus enApplicationStatus)
    {
        try
        {
            switch (enApplicationStatus)
            {
                case EnApplicationStatus.Pending:
                case EnApplicationStatus.InProgress:
                    throw new ApplicationStatusInProgressOrPendingException();

            }
        }
        catch (ApplicationStatusInProgressOrPendingException ex)
        {
            throw;
        }
    }
}