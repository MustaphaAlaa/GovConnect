using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class ALTER_GetLDLAppsAllowedToRetakATest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"ALTER FUNCTION [dbo].[GetLDLAppsAllowedToRetakATest] (
                    @LDLAppId int,
                    @TestTypeId int
                )
                RETURNS TABLE
                AS
                RETURN
                (
                    SELECT  R.Id,
							R.LocalDrivingLicenseApplicationId,
							R.TestTypeId,
							R.IsAllowedToRetakeATest  
                    FROM LDLApplicationsAllowedToRetakeATests R
                    WHERE R.LocalDrivingLicenseApplicationId = @LDLAppId
                          AND R.TestTypeId = @TestTypeId
                );";

            migrationBuilder.Sql(sql);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sql = @"ALTER FUNCTION [dbo].[GetLDLAppsAllowedToRetakATest] (
                    @LDLAppId int,
                    @TestTypeId int
                )
                RETURNS TABLE
                AS
                RETURN
                (
                    SELECT  * 
                        FROM LDLApplicationsAllowedToRetakeATests R
                        WHERE R.LocalDrivingLicenseApplicationId = @LDLAppId
                                AND R.TestTypeId = @TestTypeId);";

            migrationBuilder.Sql(sql);

        }
    }
}
