﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class Create_SP_GetTestTypeDayTimeInterval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sqlCommand = @"
                    CREATE PROCEDURE SP_GetTestTypeDayTimeInterval 
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

            migrationBuilder.Sql(sqlCommand);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE SP_GetTestTypeDayTimeInterval");
        }
    }
}