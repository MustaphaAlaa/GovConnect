using DataConfigurations.TVFs.ITVFs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Models.Users;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelDTO.TestsDTO;
using Microsoft.Data.SqlClient;
using Models.Tests;
using Microsoft.EntityFrameworkCore;
using IRepository.ITVFs;
using DataConfigurations;
using System.Dynamic;

namespace Repositorties.TVFs;

//public class TVFGetLDLAppsAllowedToRetakATest : TVFBase, ITVF_GetLDLAppsAllowedToRetakATest
public class TVFGetLDLAppsAllowedToRetakATest : ITVF_GetLDLAppsAllowedToRetakATest
{
    private readonly GovConnectDbContext _context;

    public TVFGetLDLAppsAllowedToRetakATest(GovConnectDbContext context)
    {
        _context = context;
    }

    public async Task<LDLApplicationsAllowedToRetakeATestDTO?> GetLDLAppsAllowedToRetakATest(int LDLApplicationId, int TestTypeId)
    {
        try
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@TestTypeId", TestTypeId),
                                                             new SqlParameter("@LDLAppId", LDLApplicationId) };


            var result = await _context.Database.SqlQueryRaw<LDLApplicationsAllowedToRetakeATestDTO?>(@"SELECT * FROM GetLDLAppsAllowedToRetakATest(@LDLAppId,@TestTypeId)", parameters).FirstOrDefaultAsync();


            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
