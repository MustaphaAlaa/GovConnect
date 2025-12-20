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
            LDLApplicationsAllowedToRetakeATestDTO? result = await _context.GetLDLAppsAllowedToRetakATest(LDLApplicationId, TestTypeId).FirstOrDefaultAsync();

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
