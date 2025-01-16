using DataConfigurations.TVFs.ITVFs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Models.Users;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelDTO.TestsDTO;
using Microsoft.Data.SqlClient;
using Models.Tests;
using Microsoft.EntityFrameworkCore;

namespace DataConfigurations
{
    public partial class GovConnectDbContext : IdentityDbContext<User, UserRoles, Guid>, ITVF_GetLDLAppsAllowedToRetakATest
    {
        public async Task<LDLApplicationsAllowedToRetakeATestDTO?> GetLDLAppsAllowedToRetakATest(int LDLApplicationId, int TestTypeId)
        {
            SqlParameter[] parameters = new[] { new SqlParameter("@TestTypeId", TestTypeId),
                                                 new SqlParameter("@LDLAppId", LDLApplicationId) };

            var result = await Database.SqlQueryRaw<LDLApplicationsAllowedToRetakeATestDTO?>(@"SELECT * FROM GetLDLAppsAllowedToRetakATest(@TestTypeId,@LDLAppId)", parameters).FirstOrDefaultAsync();

            return result;
        }
    }
}
