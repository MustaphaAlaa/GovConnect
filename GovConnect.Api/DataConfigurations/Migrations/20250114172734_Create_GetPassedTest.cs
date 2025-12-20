using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class Create_GetPassedTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string SQLCommand = @"
                    CREATE FUNCTION GetPassedTest
                    (
                        @LocalDrivingLicenseApplicationId int,
                        @TestTypeId int
                    )
                    RETURNS TABLE
                    AS
                    RETURN
                    (
                        SELECT 
                            t.TestId,
                            b.LocalDrivingLicenseApplicationId,
                            b.BookingId,
                            tt.TestTypeId,
                            tt.TestTypeTitle,
                            t.TestResult,
                            t.Notes 
                        FROM Tests t
                        JOIN Bookings b 
                            ON t.BookingId = b.BookingId
                        JOIN TestTypes tt 
                            ON tt.TestTypeId = b.TestTypeId
                        WHERE t.TestResult = 1
                            AND b.LocalDrivingLicenseApplicationId = @LocalDrivingLicenseApplicationId
                            AND b.TestTypeId = @TestTypeId
                    );";

            migrationBuilder.Sql(SQLCommand);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION GetPassedTest");

        }
    }
}