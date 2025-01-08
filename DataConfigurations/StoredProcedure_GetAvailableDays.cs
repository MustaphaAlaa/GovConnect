using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ModelDTO.Appointments;
using Models;
using Models.Tests;
using Models.Users;

namespace DataConfigurations;

/// <summary>
/// Represents the database context for the GovConnect application, including identity and custom entities.
/// </summary>
public partial class GovConnectDbContext : IdentityDbContext<User, UserRoles, Guid>
{
    /// <summary>
    /// Executes the stored procedure SP_GetAvailableDays to retrieve available days for a given test type.
    /// </summary>
    /// <param name="TestTypeId">The ID of the test type.</param>
    /// <returns>A list of available days as DateOnly objects.</returns>
    public async Task<List<DateOnly>> SP_GetAvailableDays(int TestTypeId)
    {
        SqlParameter parameter = new SqlParameter("@TestTypeId", TestTypeId);

        var exec = await Database.SqlQueryRaw<DateOnly>(@"EXECUTE SP_GetAvailableDays @TestTypeId", parameter).ToListAsync();

        return exec;
    }
}


