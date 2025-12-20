using DataConfigurations;
using IRepository.ISPs.IAppointmentProcedures;
using Microsoft.EntityFrameworkCore;

namespace Repositorties.SPs.AppointmentReps;

/// <summary>
/// Represents the database context for the GovConnect application, including identity and custom entities.
/// </summary>
//public class SPInsertAppointment : SPBase, ISP_InsertAppointment
public class SPMarkExpiredAppointmentsAsUnavailable : ISP_MarkExpiredAppointmentsAsUnavailable
{

    private readonly GovConnectDbContext _context;
    public SPMarkExpiredAppointmentsAsUnavailable(GovConnectDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />

    public async Task<int> Exec()
    {
        var exec = await _context.Database
            .ExecuteSqlRawAsync(@"Exec   dbo.SP_MarkExpiredAppointmentsAsUnavailable");
        return exec;
    }


}

