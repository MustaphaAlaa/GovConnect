using IServices.IApplicationServices.IServiceCategoryApplications.IRetakeTestApplication;
using ModelDTO.ApplicationDTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ApplicationServices.ServiceCategoryApplications.RetakeTestApplication;
public class RetakeTestApplicationCreateor : IRetakeTestApplicationCreation
{
    public Task<Models.Applications.RetakeTestApplication> CreateAsync(CreateRetakeTestApplicationRequest entity)
    {
        throw new NotImplementedException();
    }
}
