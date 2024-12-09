using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class InternationalDrivingLicenseApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidFees",
                table: "Licenses");

            migrationBuilder.RenameColumn(
                name: "IssueDate",
                table: "Licenses",
                newName: "IssuingDate");

            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "Licenses",
                newName: "ExpiryDate");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "ThirdName");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Licenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Licenses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FourthName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "InternationalDrivingLicenseApplication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    LicenseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternationalDrivingLicenseApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternationalDrivingLicenseApplication_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InternationalDrivingLicenseApplication_Licenses_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Licenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "LicenseTypes",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fees = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InternationalLicenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternationalDrivingLicenseApplication = table.Column<int>(type: "int", nullable: false),
                    LicenseClassId = table.Column<int>(type: "int", nullable: false),
                    LicenseId = table.Column<int>(type: "int", nullable: false),
                    applicationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternationalLicenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternationalLicenses_InternationalDrivingLicenseApplication_applicationId",
                        column: x => x.applicationId,
                        principalTable: "InternationalDrivingLicenseApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InternationalLicenses_Licenses_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Licenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "LicenseTypes",
                columns: new[] { "Id", "Fees", "Title" },
                values: new object[,]
                {
                    { (byte)1, 20m, "Local" },
                    { (byte)2, 100m, "International" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Licenses_CountryId",
                table: "Licenses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_InternationalDrivingLicenseApplication_ApplicationId",
                table: "InternationalDrivingLicenseApplication",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_InternationalDrivingLicenseApplication_LicenseId",
                table: "InternationalDrivingLicenseApplication",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_InternationalLicenses_applicationId",
                table: "InternationalLicenses",
                column: "applicationId");

            migrationBuilder.CreateIndex(
                name: "IX_InternationalLicenses_LicenseId",
                table: "InternationalLicenses",
                column: "LicenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Licenses_Countries_CountryId",
                table: "Licenses",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licenses_Countries_CountryId",
                table: "Licenses");

            migrationBuilder.DropTable(
                name: "InternationalLicenses");

            migrationBuilder.DropTable(
                name: "LicenseTypes");

            migrationBuilder.DropTable(
                name: "InternationalDrivingLicenseApplication");

            migrationBuilder.DropIndex(
                name: "IX_Licenses_CountryId",
                table: "Licenses");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Licenses");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Licenses");

            migrationBuilder.DropColumn(
                name: "FourthName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SecondName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "IssuingDate",
                table: "Licenses",
                newName: "IssueDate");

            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                table: "Licenses",
                newName: "ExpirationDate");

            migrationBuilder.RenameColumn(
                name: "ThirdName",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.AddColumn<decimal>(
                name: "PaidFees",
                table: "Licenses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
