using DataConfigurations.TVFs.ITVFs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ModelDTO.TestsDTO;
using Models.Users;
using Models;
using System;
using System.Collections.Generic;
using IRepository.ITVFs;
using DataConfigurations;

namespace Repositorties.TVFs;
//public class TVFGetTestResult : TVFBase, ITVF_GetTestResult
public class TVFGetTestResult : TVFBase, ITVF_GetTestResult
{
    private readonly GovConnectDbContext _context;
    public TVFGetTestResult(GovConnectDbContext context) : base(context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<TestDTO?> GetTestResult(int TestId)
    {

        try
        {
            SqlParameter parameter = new SqlParameter("@TestId", TestId);

            var result = await _context.Database.SqlQueryRaw<TestDTO?>(@"SELECT * FROM GetTestResult(@TestId)", parameter).FirstOrDefaultAsync();

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
