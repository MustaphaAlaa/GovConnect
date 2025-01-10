using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class CREATE_GetTestTypeDayTimeInterval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sqlCommand = @"
                    CREATE FUNCTION ITVF_GetTestTypeDayTimeInterval 
                    (
                        @TestTypeId INT, 
                        @Day DATE
                    ) 
                    RETURNS TABLE 
                    AS 
                    RETURN 
                    (
                        SELECT 
                            TimeInterval.TimeIntervalId, 
                            TimeInterval.Hour, 
                            TimeInterval.Minute 
                        FROM Appointments AS Appointment
                        INNER JOIN TimeIntervals AS TimeInterval
                            ON TimeInterval.TimeIntervalId = Appointment.TimeIntervalId
                        WHERE 
                            AppointmentDay = @Day 
                            AND TestTypeId = @TestTypeId
                    );";

            migrationBuilder.Sql(sqlCommand);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION ITVF_GetTestTypeDayTimeInterval");
        }
    }
}
