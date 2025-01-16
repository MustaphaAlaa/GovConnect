using DataConfigurations.TVFs.ITVFs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using ModelDTO.TestsDTO;
using Models.Users;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataConfigurations;

public partial class GovConnectDbContext : IdentityDbContext<User, UserRoles, Guid>, ITVF_GetPassedTest
{
    /// <inheritdoc/> 
    public async Task<TestDTO?> GetPassedTest(int LDLApplicationId, int TestTypeId)
    {
        SqlParameter[] parameters = new[]{
        new SqlParameter("@TestTypeId", TestTypeId),
        new SqlParameter("@LocalDrivingLicenseApplicationId", LDLApplicationId) };

        var result = await Database.SqlQueryRaw<TestDTO>(@"SELECT * FROM GetPassedTest(@LocalDrivingLicenseApplicationId,@TestTypeId)", parameters).FirstOrDefaultAsync();

        return result;
    }
}
