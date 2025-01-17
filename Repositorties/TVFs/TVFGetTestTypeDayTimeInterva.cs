using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ModelDTO.Appointments;
using IRepository.ITVFs;
using DataConfigurations;

namespace Repositorties.TVFs;
//public class TVFGetTestTypeDayTimeInterva : TVFBase, ITVF_GetTestTypeDayTimeInterval
public class TVFGetTestTypeDayTimeInterva : ITVF_GetTestTypeDayTimeInterval
{
    private readonly GovConnectDbContext _context;

    public TVFGetTestTypeDayTimeInterva(GovConnectDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<List<TimeIntervalForADayDTO>> GetTestTypeDayTimeInterval(int TestTypeId, DateOnly day)
    {
        try
        {
            SqlParameter[] parameter = new[]
            {
                new SqlParameter("@TestTypeId", TestTypeId),
                new SqlParameter("@Day", day)
        };

            var result = await _context.Database
                .SqlQueryRaw<TimeIntervalForADayDTO>(@"SELECT * FROM GetTestTypeDayTimeInterval(@TestTypeId, @Day)", parameter)
                .ToListAsync();
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }

    }
}
