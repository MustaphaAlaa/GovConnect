using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class dropisActiveandcreateLicenseStatusColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "LocalDrivingLicenses");

            migrationBuilder.AddColumn<int>(
                name: "LicenseStatus",
                table: "LocalDrivingLicenses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicenseStatus",
                table: "LocalDrivingLicenses");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "LocalDrivingLicenses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
