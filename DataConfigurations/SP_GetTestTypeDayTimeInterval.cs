using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ModelDTO.Appointments;
using Models.Users;
using Models;

namespace DataConfigurations;

/// <summary>
/// Represents the database context for the GovConnect application, including identity and custom entities.
/// </summary>
public partial class GovConnectDbContext : IdentityDbContext<User, UserRoles, Guid>, ISP_GetTestTypeDayTimeInterval
{
    /// <inheritdoc />

    public async Task<List<TimeIntervalDTO>> SP_GetTestTypeDayTimeInterval(int TestTypeId, DateOnly day)
    {
        SqlParameter[] parameter = new[]
        {
                new SqlParameter("@TestTypeId", TestTypeId),
                new SqlParameter("@Day", day)
        };

        var exec = await Database
            .SqlQueryRaw<TimeIntervalDTO>(@"EXECUTE SP_GetTestTypeDayTimeInterval @TestTypeId, @Day", parameter)
            .ToListAsync();
        return exec;
    }
}
