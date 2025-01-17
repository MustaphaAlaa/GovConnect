using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using ModelDTO.TestsDTO;
using Models.Users;
using Models;
using Microsoft.EntityFrameworkCore;
using IRepositorys.ITVFs;
using DataConfigurations;

namespace Repositorties.TVFs;

//public class TVFGetPassedTest : TVFBase, ITVF_GetPassedTest
public class TVFGetPassedTest : ITVF_GetPassedTest
{
    private readonly GovConnectDbContext _context;
    public TVFGetPassedTest(GovConnectDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/> 
    public async Task<TestDTO?> GetPassedTest(int LDLApplicationId, int TestTypeId)
    {
        SqlParameter[] parameters = new[]{
        new SqlParameter("@TestTypeId", TestTypeId),
        new SqlParameter("@LocalDrivingLicenseApplicationId", LDLApplicationId) };
        try
        {
            var result = await _context.Database.SqlQueryRaw<TestDTO>(@"SELECT * FROM GetPassedTest(@LocalDrivingLicenseApplicationId,@TestTypeId)", parameters).FirstOrDefaultAsync();

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
