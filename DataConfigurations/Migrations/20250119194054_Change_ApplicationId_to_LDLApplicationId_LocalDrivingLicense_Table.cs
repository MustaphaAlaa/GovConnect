using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class Change_ApplicationId_to_LDLApplicationId_LocalDrivingLicense_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocalDrivingLicenses_LocalDrivingLicenses_ApplicationId",
                table: "LocalDrivingLicenses");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "LocalDrivingLicenses",
                newName: "LocalDrivingLicenseApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_LocalDrivingLicenses_ApplicationId",
                table: "LocalDrivingLicenses",
                newName: "IX_LocalDrivingLicenses_LocalDrivingLicenseApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalDrivingLicenses_LocalDrivingLicenseApplications_LocalDrivingLicenseApplicationId",
                table: "LocalDrivingLicenses",
                column: "LocalDrivingLicenseApplicationId",
                principalTable: "LocalDrivingLicenseApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocalDrivingLicenses_LocalDrivingLicenseApplications_LocalDrivingLicenseApplicationId",
                table: "LocalDrivingLicenses");

            migrationBuilder.RenameColumn(
                name: "LocalDrivingLicenseApplicationId",
                table: "LocalDrivingLicenses",
                newName: "ApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_LocalDrivingLicenses_LocalDrivingLicenseApplicationId",
                table: "LocalDrivingLicenses",
                newName: "IX_LocalDrivingLicenses_ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalDrivingLicenses_LocalDrivingLicenses_ApplicationId",
                table: "LocalDrivingLicenses",
                column: "ApplicationId",
                principalTable: "LocalDrivingLicenses",
                principalColumn: "LocalDrivingLicenseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
