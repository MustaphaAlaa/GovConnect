using DataConfigurations;
using IRepository.ITVFs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Repositorties.TVFs;


//public class TVFGetAvailableDays : TVFBase, ITVF_GetAvailab/leDays
public class TVFGetAvailableDays : ITVF_GetAvailableDays
{

    private readonly GovConnectDbContext _context;

    public TVFGetAvailableDays(GovConnectDbContext context)
    {
        _context = context;
    }



    /// <inheritdoc/>
    public async Task<List<DateOnly>> GetAvailableDays(int TestTypeId)
    {
        try
        {
            SqlParameter parameter = new SqlParameter("@TestTypeId", TestTypeId);

            var result = await _context.Database.SqlQueryRaw<DateOnly>(@"SELECT * FROM GetAvailableDays(@TestTypeId)", parameter).ToListAsync();

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}





