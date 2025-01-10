using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class DROP_SP_GetAvailableDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = "DROP PROCEDURE SP_GetAvailableDays";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sql = @" 
                         CREATE PROCEDURE SP_GetAvailableDays
                        @TestTypeId INT
                    AS
                    BEGIN
                        SELECT DISTINCT AppointmentDay 
                        FROM Appointments
                        WHERE TestTypeId = @TestTypeId;
                    END";

            migrationBuilder.Sql(sql);
        }
    }
}
