using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class InsertServiceFeesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetainedLicenses_LocalDrivingLicenses_LicenseId",
                table: "DetainedLicenses");

            migrationBuilder.DropForeignKey(
                name: "FK_InternationalDrivingLicenseApplications_LocalDrivingLicenses_LicenseId",
                table: "InternationalDrivingLicenseApplications");

            migrationBuilder.DropIndex(
                name: "IX_InternationalDrivingLicenseApplications_LicenseId",
                table: "InternationalDrivingLicenseApplications");

            migrationBuilder.DropIndex(
                name: "IX_DetainedLicenses_LicenseId",
                table: "DetainedLicenses");

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 82);

            migrationBuilder.AddColumn<int>(
                name: "LocalDrivingLicenseId",
                table: "InternationalDrivingLicenseApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocalDrivingLicenseId",
                table: "DetainedLicenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "ServiceCategoryId",
                keyValue: (short)1,
                column: "Category",
                value: "Local Driving License");

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "ServiceCategoryId",
                keyValue: (short)2,
                column: "Category",
                value: "International Driving License");

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "ServiceCategoryId",
                keyValue: (short)4,
                column: "Category",
                value: "National Identity Card");

            migrationBuilder.InsertData(
                table: "ServicesFees",
                columns: new[] { "ServiceCategoryId", "ServicePurposeId", "Fees", "LastUpdate" },
                values: new object[,]
                {
                    { (short)1, (byte)1, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)2, (byte)1, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)3, (byte)1, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)4, (byte)1, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)1, (byte)2, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)2, (byte)2, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)3, (byte)2, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)4, (byte)2, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)1, (byte)3, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)2, (byte)3, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)3, (byte)3, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)4, (byte)3, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)1, (byte)4, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)2, (byte)4, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)3, (byte)4, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)4, (byte)4, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)1, (byte)5, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)1, (byte)6, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.UpdateData(
                table: "ServicesPurposes",
                keyColumn: "ServicePurposeId",
                keyValue: (byte)3,
                column: "Purpose",
                value: "Replacement For Damage");

            migrationBuilder.UpdateData(
                table: "ServicesPurposes",
                keyColumn: "ServicePurposeId",
                keyValue: (byte)4,
                column: "Purpose",
                value: "Replacement For Lost");

            migrationBuilder.UpdateData(
                table: "ServicesPurposes",
                keyColumn: "ServicePurposeId",
                keyValue: (byte)6,
                column: "Purpose",
                value: "Retake Test");

            migrationBuilder.CreateIndex(
                name: "IX_InternationalDrivingLicenseApplications_LocalDrivingLicenseId",
                table: "InternationalDrivingLicenseApplications",
                column: "LocalDrivingLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_DetainedLicenses_LocalDrivingLicenseId",
                table: "DetainedLicenses",
                column: "LocalDrivingLicenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetainedLicenses_LocalDrivingLicenses_LocalDrivingLicenseId",
                table: "DetainedLicenses",
                column: "LocalDrivingLicenseId",
                principalTable: "LocalDrivingLicenses",
                principalColumn: "LocalDrivingLicenseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InternationalDrivingLicenseApplications_LocalDrivingLicenses_LocalDrivingLicenseId",
                table: "InternationalDrivingLicenseApplications",
                column: "LocalDrivingLicenseId",
                principalTable: "LocalDrivingLicenses",
                principalColumn: "LocalDrivingLicenseId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetainedLicenses_LocalDrivingLicenses_LocalDrivingLicenseId",
                table: "DetainedLicenses");

            migrationBuilder.DropForeignKey(
                name: "FK_InternationalDrivingLicenseApplications_LocalDrivingLicenses_LocalDrivingLicenseId",
                table: "InternationalDrivingLicenseApplications");

            migrationBuilder.DropIndex(
                name: "IX_InternationalDrivingLicenseApplications_LocalDrivingLicenseId",
                table: "InternationalDrivingLicenseApplications");

            migrationBuilder.DropIndex(
                name: "IX_DetainedLicenses_LocalDrivingLicenseId",
                table: "DetainedLicenses");

            migrationBuilder.DeleteData(
                table: "ServicesFees",
                keyColumns: new[] { "ServiceCategoryId", "ServicePurposeId" },
                keyValues: new object[] { (short)1, (byte)1 });

            migrationBuilder.DeleteData(
                table: "ServicesFees",
                keyColumns: new[] { "ServiceCategoryId", "ServicePurposeId" },
                keyValues: new object[] { (short)2, (byte)1 });

            migrationBuilder.DeleteData(
                table: "ServicesFees",
                keyColumns: new[] { "ServiceCategoryId", "ServicePurposeId" },
                keyValues: new object[] { (short)3, (byte)1 });

            migrationBuilder.DeleteData(
                table: "ServicesFees",
                keyColumns: new[] { "ServiceCategoryId", "ServicePurposeId" },
                keyValues: new object[] { (short)4, (byte)1 });

            migrationBuilder.DeleteData(
                table: "ServicesFees",
                keyColumns: new[] { "ServiceCategoryId", "ServicePurposeId" },
                keyValues: new object[] { (short)1, (byte)2 });

            migrationBuilder.DeleteData(
                table: "ServicesFees",
                keyColumns: new[] { "ServiceCategoryId", "ServicePurposeId" },
                keyValues: new object[] { (short)2, (byte)2 });

            migrationBuilder.DeleteData(
                table: "ServicesFees",
                keyColumns: new[] { "ServiceCategoryId", "ServicePurposeId" },
                keyValues: new object[] { (short)3, (byte)2 });

            migrationBuilder.DeleteData(
                table: "ServicesFees",
                keyColumns: new[] { "ServiceCategoryId", "ServicePurposeId" },
                keyValues: new object[] { (short)4, (byte)2 });

            migrationBuilder.DeleteData(
                table: "ServicesFees",
                keyColumns: new[] { "ServiceCategoryId", "ServicePurposeId" },
                keyValues: new object[] { (short)1, (byte)3 });

            migrationBuilder.DeleteData(
                table: "ServicesFees",
                keyColumns: new[] { "ServiceCategoryId", "ServicePurposeId" },
                keyValues: new object[] { (short)2, (byte)3 });

            migrationBuilder.DeleteData(
                table: "ServicesFees",
                keyColumns: new[] { "ServiceCategoryId", "ServicePurposeId" },
                keyValues: new object[] { (short)3, (byte)3 });

            migrationBuilder.DeleteData(
                table: "ServicesFees",
                keyColumns: new[] { "ServiceCategoryId", "ServicePurposeId" },
                keyValues: new object[] { (short)4, (byte)3 });

            migrationBuilder.DeleteData(
                table: "ServicesFees",
                keyColumns: new[] { "ServiceCategoryId", "ServicePurposeId" },
                keyValues: new object[] { (short)1, (byte)4 });

            migrationBuilder.DeleteData(
                table: "ServicesFees",
                keyColumns: new[] { "ServiceCategoryId", "ServicePurposeId" },
                keyValues: new object[] { (short)2, (byte)4 });

            migrationBuilder.DeleteData(
                table: "ServicesFees",
                keyColumns: new[] { "ServiceCategoryId", "ServicePurposeId" },
                keyValues: new object[] { (short)3, (byte)4 });

            migrationBuilder.DeleteData(
                table: "ServicesFees",
                keyColumns: new[] { "ServiceCategoryId", "ServicePurposeId" },
                keyValues: new object[] { (short)4, (byte)4 });

            migrationBuilder.DeleteData(
                table: "ServicesFees",
                keyColumns: new[] { "ServiceCategoryId", "ServicePurposeId" },
                keyValues: new object[] { (short)1, (byte)5 });

            migrationBuilder.DeleteData(
                table: "ServicesFees",
                keyColumns: new[] { "ServiceCategoryId", "ServicePurposeId" },
                keyValues: new object[] { (short)1, (byte)6 });

            migrationBuilder.DropColumn(
                name: "LocalDrivingLicenseId",
                table: "InternationalDrivingLicenseApplications");

            migrationBuilder.DropColumn(
                name: "LocalDrivingLicenseId",
                table: "DetainedLicenses");

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "CountryCode", "CountryName" },
                values: new object[] { 82, "ISR", "Israel" });

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "ServiceCategoryId",
                keyValue: (short)1,
                column: "Category",
                value: "Local_Driving_License");

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "ServiceCategoryId",
                keyValue: (short)2,
                column: "Category",
                value: "International_Driving_License");

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "ServiceCategoryId",
                keyValue: (short)4,
                column: "Category",
                value: "International_Driving_License");

            migrationBuilder.UpdateData(
                table: "ServicesPurposes",
                keyColumn: "ServicePurposeId",
                keyValue: (byte)3,
                column: "Purpose",
                value: "Replacement_For_Damage");

            migrationBuilder.UpdateData(
                table: "ServicesPurposes",
                keyColumn: "ServicePurposeId",
                keyValue: (byte)4,
                column: "Purpose",
                value: "Replacement_For_Lost");

            migrationBuilder.UpdateData(
                table: "ServicesPurposes",
                keyColumn: "ServicePurposeId",
                keyValue: (byte)6,
                column: "Purpose",
                value: "Retake_Test");

            migrationBuilder.CreateIndex(
                name: "IX_InternationalDrivingLicenseApplications_LicenseId",
                table: "InternationalDrivingLicenseApplications",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_DetainedLicenses_LicenseId",
                table: "DetainedLicenses",
                column: "LicenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetainedLicenses_LocalDrivingLicenses_LicenseId",
                table: "DetainedLicenses",
                column: "LicenseId",
                principalTable: "LocalDrivingLicenses",
                principalColumn: "LocalDrivingLicenseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InternationalDrivingLicenseApplications_LocalDrivingLicenses_LicenseId",
                table: "InternationalDrivingLicenseApplications",
                column: "LicenseId",
                principalTable: "LocalDrivingLicenses",
                principalColumn: "LocalDrivingLicenseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
