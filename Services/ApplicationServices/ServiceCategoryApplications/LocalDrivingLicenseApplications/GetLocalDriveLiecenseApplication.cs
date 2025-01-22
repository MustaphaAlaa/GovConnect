using IRepository.IGenericRepositories;
using IServices.IApplicationServices.IServiceCategoryApplications.ILocalDrivingLicenseApplication;
using Microsoft.Extensions.Logging;
using Models.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.ApplicationServices.ServiceCategoryApplications.LocalDrivingLicenseApplications
{
    public class GetLocalDriveLiecenseApplication : IGetLocalDrivingLicenseApplication
    {
        private readonly IGetRepository<LocalDrivingLicenseApplication> _getRepository;
        private readonly ILogger<GetLocalDriveLiecenseApplication> _logger;

        public GetLocalDriveLiecenseApplication(IGetRepository<LocalDrivingLicenseApplication> getRepository,
            ILogger<GetLocalDriveLiecenseApplication> logger)
        {
            _getRepository = getRepository;
            _logger = logger;
        }

        public async Task<LocalDrivingLicenseApplication?> GetByAsync(Expression<Func<LocalDrivingLicenseApplication, bool>> predicate)
        {
            var res = await _getRepository.GetAsync(predicate);
            return res;
        }
    }
}
