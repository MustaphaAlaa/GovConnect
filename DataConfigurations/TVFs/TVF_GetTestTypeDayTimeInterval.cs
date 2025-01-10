using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ModelDTO.Appointments;
using Models.Users;
using Models;
using DataConfigurations.TVFs.ITVFs;

namespace DataConfigurations;

/// <summary>
/// Represents the database context for the GovConnect application, including identity and custom entities.
/// </summary>
public partial class GovConnectDbContext : IdentityDbContext<User, UserRoles, Guid>, ITVF_GetTestTypeDayTimeInterval
{
    /// <inheritdoc />

    public async Task<List<TimeIntervalForADayDTO>> GetTestTypeDayTimeInterval(int TestTypeId, DateOnly day)
    {
        SqlParameter[] parameter = new[]
        {
                new SqlParameter("@TestTypeId", TestTypeId),
                new SqlParameter("@Day", day)
        };

        var result = await Database
            .SqlQueryRaw<TimeIntervalForADayDTO>(@"SELECT * FROM GetTestTypeDayTimeInterval(@TestTypeId, @Day)", parameter)
            .ToListAsync();
        return result;
    }
}
