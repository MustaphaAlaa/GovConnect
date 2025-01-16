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

public partial class GovConnectDbContext : IdentityDbContext<User, UserRoles, Guid>, ITVF_GetTestResultForABookingId
{
    /// <inheritdoc/>
    public async Task<TestDTO?> GetTestResultForABookingId(int BookingId)
    {
        SqlParameter parameter = new SqlParameter("@BookingId", BookingId);

        var result = await Database.SqlQueryRaw<TestDTO?>(@"SELECT * FROM GetTestResultForABookingId(@BookingId)", parameter).FirstOrDefaultAsync();

        return result;
    }
}