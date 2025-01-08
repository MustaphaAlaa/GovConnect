using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class update_SP_GetTestTypeDayTimeInterval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sqlCommand = @"
                    ALTER PROCEDURE SP_GetTestTypeDayTimeInterval 
                        @TestTypeId int, 
                        @Day Date 
                    AS 
                    BEGIN
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
                            AND IsAvailable = 1;
                    END";

            migrationBuilder.Sql(sqlCommand);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            string SQL = @"
                    ALTER PROCEDURE SP_GetTestTypeDayTimeInterval 
                        @TestTypeId int, 
                        @Day Date 
                    AS 
                    BEGIN
                        SELECT 
                            TimeInterval.TimeIntervalId, 
                            TimeInterval.Hour, 
                            TimeInterval.Minute 
                        FROM Appointments AS Appointment
                        INNER JOIN TimeIntervals AS TimeInterval
                            ON TimeInterval.TimeIntervalId = Appointment.TimeIntervalId
                        WHERE 
                            AppointmentDay = @Day 
                            AND TestTypeId = @TestTypeId; 
                    END";
            migrationBuilder.Sql(SQL);
        }
    }
}
