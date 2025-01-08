using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Models.Tests;
using Models.Users;

namespace DataConfigurations;

public partial class GovConnectDbContext : IdentityDbContext<User, UserRoles, Guid>
{

    public async Task<int> SP_InsertAppointment(Appointment appointment)
    {

        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@Day", appointment.AppointmentDay ),
            new SqlParameter("@TestTypeId", appointment.TestTypeId),
            new SqlParameter("@TimeIntervalId", appointment.TimeIntervalId)
        };

        var exec = await Database.ExecuteSqlRawAsync(@"EXECUTE SP_InsertAppointment  
                                                     @Day
                                                    ,@TestTypeId
                                                    ,@TimeIntervalId"
                                                    , parameters);
        return exec;
    }
}

