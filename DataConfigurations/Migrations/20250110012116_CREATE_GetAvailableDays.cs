using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class CREATE_GetAvailableDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"
                    CREATE FUNCTION GetAvailableDays
                    (
                        @TestTypeId INT
                    )
                    RETURNS TABLE
                    AS
                    RETURN
                    (
                        SELECT DISTINCT AppointmentDay
                        FROM Appointments
                        WHERE TestTypeId = @TestTypeId
                    );";

            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sql = "DROP FUNCTION GetAvailableDays;";
            migrationBuilder.Sql(sql);
        }
    }
}
