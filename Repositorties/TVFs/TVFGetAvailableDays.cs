using DataConfigurations;
using IRepository.ITVFs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ModelDTO.TestsDTO;

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

            var result = await _context.GetAvailableDays(TestTypeId).Select(t => t.AppointmentDay).ToListAsync();

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}





