using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class editApplicationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationTypes_ApplicationTypeId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalDrivingLicenseApplications_ApplicationTypes_ApplicationTypeId",
                table: "LocalDrivingLicenseApplications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_ApplicationTypeId",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "ApplicationTypeId",
                table: "LocalDrivingLicenseApplications",
                newName: "ApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_LocalDrivingLicenseApplications_ApplicationTypeId",
                table: "LocalDrivingLicenseApplications",
                newName: "IX_LocalDrivingLicenseApplications_ApplicationId");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationForId",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ApplicationTypeId_ApplicationForId",
                table: "Applications",
                columns: new[] { "ApplicationTypeId", "ApplicationForId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationsFees_ApplicationTypeId_ApplicationForId",
                table: "Applications",
                columns: new[] { "ApplicationTypeId", "ApplicationForId" },
                principalTable: "ApplicationsFees",
                principalColumns: new[] { "ApplicationTypeId", "ApplicationForId" },
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_LocalDrivingLicenseApplications_Applications_ApplicationId",
                table: "LocalDrivingLicenseApplications",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationsFees_ApplicationTypeId_ApplicationForId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalDrivingLicenseApplications_Applications_ApplicationId",
                table: "LocalDrivingLicenseApplications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_ApplicationTypeId_ApplicationForId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ApplicationForId",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "LocalDrivingLicenseApplications",
                newName: "ApplicationTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_LocalDrivingLicenseApplications_ApplicationId",
                table: "LocalDrivingLicenseApplications",
                newName: "IX_LocalDrivingLicenseApplications_ApplicationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ApplicationTypeId",
                table: "Applications",
                column: "ApplicationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationTypes_ApplicationTypeId",
                table: "Applications",
                column: "ApplicationTypeId",
                principalTable: "ApplicationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocalDrivingLicenseApplications_ApplicationTypes_ApplicationTypeId",
                table: "LocalDrivingLicenseApplications",
                column: "ApplicationTypeId",
                principalTable: "ApplicationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
