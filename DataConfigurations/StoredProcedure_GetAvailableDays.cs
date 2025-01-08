using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ModelDTO.Appointments;
using Models;
using Models.Tests;
using Models.Users;
namespace DataConfigurations
{

    public partial class GovConnectDbContext : IdentityDbContext<User, UserRoles, Guid>
    {
        public async Task<List<DateOnly>> SP_GetAvailableDays(int TestTypeId)
        {
            SqlParameter parameter = new SqlParameter("@TestTypeId", TestTypeId);

            var exec = await Database.SqlQueryRaw<DateOnly>(@"EXECUTE SP_GetAvailableDays @TestTypeId", parameter).ToListAsync();

            return exec;
        }


    }

    public interface ISP_GetTestTypeDayTimeInterval
    {
        public Task<IQueryable<List<TimeIntervalDTO>>> SP_GetTestTypeDayTimeInterval(int TestTypeId, DateOnly day);

    }
    public partial class GovConnectDbContext : IdentityDbContext<User, UserRoles, Guid>
    {
        public async Task<List<TimeIntervalDTO>> SP_GetTestTypeDayTimeInterval(int TestTypeId, DateOnly day)
        {
            SqlParameter[] parameter = new[]{
                                       new SqlParameter("@TestTypeId", TestTypeId),
                                       new SqlParameter("@Day", day) };

            var exec = await Database
                .SqlQueryRaw<TimeIntervalDTO>(@"EXECUTE SP_GetTestTypeDayTimeInterval @TestTypeId, @Day", parameter)
                .ToListAsync();
            return exec;
        }


    }

}
