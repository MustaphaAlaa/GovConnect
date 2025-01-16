using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class Create_GetLDLAppsAllowedToRetakATest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"
                CREATE FUNCTION GetLDLAppsAllowedToRetakATest (
                    @LDLAppId int,
                    @TestTypeId int
                )
                RETURNS TABLE
                AS
                RETURN
                (
                    SELECT *
                    FROM LDLApplicationsAllowedToRetakeATests R
                    WHERE R.LocalDrivingApplicationId = @LDLAppId
                          AND R.TestTypeId = @TestTypeId
                );";

            migrationBuilder.Sql(sql);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION GetLDLAppsAllowedToRetakATest");
        }
    }
}
