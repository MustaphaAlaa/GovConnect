using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class Create_SP_MarkExpiredAppointmentsAsUnavailable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"
                  Create Procedure SP_MarkExpiredAppointmentsAsUnavailable 
                  AS BEGIN
                    UPDATE dbo.Appointments 
                    SET IsAvailable = 0
                    WHERE AppointmentDay   <=   Cast(GETDATE() as Date);
                 END";

            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sql = @"DROP Procedure SP_MarkExpiredAppointmentsAsUnavailable;";

            migrationBuilder.Sql(sql);
        }
    }
}
