using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class changeApplicationReasonTypetonvarcharinLocalDrivingLicenseApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                   name: "ApplicationReason",
                   table: "LocalDrivingLicenseApplications",
                   newName: "ReasonForTheApplication"
               );
            migrationBuilder.AlterColumn<string>(
                name: "ReasonForTheApplication",
                table: "LocalDrivingLicenseApplications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                   name: "ReasonForTheApplication",
                   table: "LocalDrivingLicenseApplications",
                   newName: "ApplicationReason"
               );


            migrationBuilder.AlterColumn<int>(
                name: "ApplicationReason",
                table: "LocalDrivingLicenseApplications",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
