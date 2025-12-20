using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class rename_LocalDrivingApplicationId_to_LocalDrivingLicenseApplicationId_In_LDLApplicatinosAllowedToRetakeATest_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocalDrivingLicenseApplicationId",
                table: "LDLApplicationsAllowedToRetakeATests",
                newName: "LocalDrivingLicenseApplicationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
               name: "LocalDrivingLicenseApplicationId",
               table: "LDLApplicationsAllowedToRetakeATests",
               newName: "LocalDrivingLicenseApplicationId");
        }
    }
}
