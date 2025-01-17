using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class Fix_LDLApplicationsAllowedToRetakATest_Foreign_Columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LDLApplicationsAllowedToRetakeATests_LocalDrivingLicenseApplications_LocalDrivingLicenseApplicationId",
                table: "LDLApplicationsAllowedToRetakeATests");

            migrationBuilder.DropIndex(
                name: "IX_LDLApplicationsAllowedToRetakeATests_LocalDrivingLicenseApplicationId",
                table: "LDLApplicationsAllowedToRetakeATests");

            migrationBuilder.DropColumn(
                name: "LocalDrivingLicenseApplicationId",
                table: "LDLApplicationsAllowedToRetakeATests");

            migrationBuilder.CreateIndex(
                name: "IX_LDLApplicationsAllowedToRetakeATests_LocalDrivingApplicationId",
                table: "LDLApplicationsAllowedToRetakeATests",
                column: "LocalDrivingLicenseApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LDLApplicationsAllowedToRetakeATests_LocalDrivingLicenseApplications_LocalDrivingApplicationId",
                table: "LDLApplicationsAllowedToRetakeATests",
                column: "LocalDrivingLicenseApplicationId",
                principalTable: "LocalDrivingLicenseApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LDLApplicationsAllowedToRetakeATests_LocalDrivingLicenseApplications_LocalDrivingApplicationId",
                table: "LDLApplicationsAllowedToRetakeATests");

            migrationBuilder.DropIndex(
                name: "IX_LDLApplicationsAllowedToRetakeATests_LocalDrivingApplicationId",
                table: "LDLApplicationsAllowedToRetakeATests");

            migrationBuilder.AddColumn<int>(
                name: "LocalDrivingLicenseApplicationId",
                table: "LDLApplicationsAllowedToRetakeATests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LDLApplicationsAllowedToRetakeATests_LocalDrivingLicenseApplicationId",
                table: "LDLApplicationsAllowedToRetakeATests",
                column: "LocalDrivingLicenseApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LDLApplicationsAllowedToRetakeATests_LocalDrivingLicenseApplications_LocalDrivingLicenseApplicationId",
                table: "LDLApplicationsAllowedToRetakeATests",
                column: "LocalDrivingLicenseApplicationId",
                principalTable: "LocalDrivingLicenseApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
