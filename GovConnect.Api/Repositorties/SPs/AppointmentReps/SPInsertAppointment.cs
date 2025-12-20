using DataConfigurations;
using IRepository.ISPs.IAppointmentProcedures;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Models.Tests;
using Models.Users;

namespace Repositorties.SPs.AppointmentReps;


/// <summary>
/// Represents the database context for the GovConnect application, including identity and custom entities.
/// </summary>
//public class SPInsertAppointment : SPBase, ISP_InsertAppointment
public class SPInsertAppointment : ISP_InsertAppointment
{

    private readonly GovConnectDbContext _context;
    public SPInsertAppointment(GovConnectDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />

    public async Task<int> Exec(Appointment appointment)
    {

        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@Day", appointment.AppointmentDay ),
            new SqlParameter("@TestTypeId", appointment.TestTypeId),
            new SqlParameter("@TimeIntervalId", appointment.TimeIntervalId)
        };

        var exec = await _context.Database.ExecuteSqlRawAsync(@"EXECUTE Exec  
                                                     @Day
                                                    ,@TestTypeId
                                                    ,@TimeIntervalId"
                                                    , parameters);
        return exec;
    }
}

