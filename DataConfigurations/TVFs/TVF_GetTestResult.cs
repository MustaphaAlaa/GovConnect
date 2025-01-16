using DataConfigurations.TVFs.ITVFs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ModelDTO.TestsDTO;
using Models.Users;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConfigurations;
public partial class GovConnectDbContext : IdentityDbContext<User, UserRoles, Guid>, ITVF_GetTestResult
{
    /// <inheritdoc/>
    public async Task<TestDTO?> GetTestResult(int TestId)
    {
        SqlParameter parameter = new SqlParameter("@TestId", TestId);

        var result = await Database.SqlQueryRaw<TestDTO?>(@"SELECT * FROM GetTestResult(@TestId)", parameter).FirstOrDefaultAsync();

        return result;
    }
}
