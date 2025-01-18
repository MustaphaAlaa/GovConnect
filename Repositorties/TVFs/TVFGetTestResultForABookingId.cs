using DataConfigurations.TVFs.ITVFs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ModelDTO.TestsDTO;
using Models.Users;
using DataConfigurations;

namespace Repositorties.TVFs;

//public class TVFGetTestResultForABookingId : TVFBase, ITVF_GetTestResultForABookingId
public class TVFGetTestResultForABookingId : ITVF_GetTestResultForABookingId
{

    private readonly GovConnectDbContext _context;

    public TVFGetTestResultForABookingId(GovConnectDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<TestDTO?> GetTestResultForABookingId(int BookingId)
    {
        try
        {

            //SqlParameter parameter = new SqlParameter("@BookingId", BookingId);
            var result = await _context.GetTestResultForABookingId(BookingId).FirstOrDefaultAsync();  //await _context.Database.SqlQueryRaw<TestDTO?>(@"SELECT * FROM GetTestResultForABookingId(@BookingId)", parameter).FirstOrDefaultAsync();

            return result;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message, ex);

        }
    }
}