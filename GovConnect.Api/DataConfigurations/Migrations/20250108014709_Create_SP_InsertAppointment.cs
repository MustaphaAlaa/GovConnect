using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class Create_SP_InsertAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            string sql = @" CREATE PROCEDURE SP_InsertAppointment
                                @Day DATE,
                                @TestTypeId INT,
                                @TimeIntervalId INT
                            AS
                            BEGIN
                                IF NOT EXISTS (SELECT 1 FROM Appointments WHERE 
                                                TestTypeId = @TestTypeId AND
                                                AppointmentDay = @Day AND
                                                TimeIntervalId = @TimeIntervalId)
                                BEGIN
                                    INSERT INTO Appointments (TestTypeId, TimeIntervalId, IsAvailable, AppointmentDay)
                                    VALUES (@TestTypeId, @TimeIntervalId, 1, @Day);
                                END
                            END ";

            migrationBuilder.Sql(sql);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE SP_InsertAppointment");

        }
    }
}
