using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class Create_GetTestResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string SQLCommand = @"
                    CREATE FUNCTION GetTestResult
                    (
                        @TestId int 
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
                        WHERE t.TestId = @TestId 
                    );";

            migrationBuilder.Sql(SQLCommand);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION GetTestResult");

        }
    }
}
