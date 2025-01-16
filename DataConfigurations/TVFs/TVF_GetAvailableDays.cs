using AutoMapper.QueryableExtensions;
using DataConfigurations.TVFs.ITVFs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ModelDTO.Appointments;
using ModelDTO.TestsDTO;
using Models;
using Models.Tests;
using Models.Users;

namespace DataConfigurations;


public partial class GovConnectDbContext : IdentityDbContext<User, UserRoles, Guid>, ITVF_GetAvailableDays
{
    /// <inheritdoc/>
    public async Task<List<DateOnly>> GetAvailableDays(int TestTypeId)
    {
        SqlParameter parameter = new SqlParameter("@TestTypeId", TestTypeId);

        var result = await Database.SqlQueryRaw<DateOnly>(@"SELECT * FROM GetAvailableDays(@TestTypeId)", parameter).ToListAsync();

        return result;
    }
}





