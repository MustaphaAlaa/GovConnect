using DataConfigurations;
using IRepository.ISFs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorties.SFs
{
    public class CFIsTestTypePassed : ISF_IsTestTypePassed
    {

        private readonly GovConnectDbContext _dbContext;

        public CFIsTestTypePassed(GovConnectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsTestTypePassed(int LDLApplicationId, int TestTypeId)
        {
            return await _dbContext.IsTestTypePassed(LDLApplicationId, TestTypeId);
        }
    }
}
