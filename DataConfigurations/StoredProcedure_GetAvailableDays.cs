using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Tests;
using Models.Users;
namespace DataConfigurations
{
    /// <summary>
    /// Provides methods to create the stored procedure StoredProcedure_GetAvailableDays.
    /// </summary>
    public partial class GovConnectDbContext : IdentityDbContext<User, UserRoles, Guid>
    {
        public async Task<List<DateOnly>> SP_GetAvailableDays(int TestTypeId)
        {
            SqlParameter parameter = new SqlParameter("@TestTypeId", TestTypeId);

            var exec = await Database.SqlQueryRaw<DateOnly>(@"EXECUTE SP_GetAvailableDays @TestTypeId", parameter).ToListAsync();

            return exec;
        }


    }


}
