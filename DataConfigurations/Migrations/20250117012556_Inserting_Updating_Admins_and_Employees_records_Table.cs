using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class Inserting_Updating_Admins_and_Employees_records_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("1b9d6bcd-bbfd-4b2d-9b5d-ab8dfbbd4bed"),
                column: "HiredDate",
                value: new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("6ba7b810-9dad-11d1-80b4-00c04fd430c8"),
                column: "HiredDate",
                value: new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                column: "HiredDate",
                value: new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("1b9d6bcd-bbfd-4b2d-9b5d-ab8dfbbd4bed"),
                column: "HiredDate",
                value: new DateTime(2025, 1, 17, 2, 20, 33, 228, DateTimeKind.Local).AddTicks(8868));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("6ba7b810-9dad-11d1-80b4-00c04fd430c8"),
                column: "HiredDate",
                value: new DateTime(2025, 1, 17, 2, 20, 33, 228, DateTimeKind.Local).AddTicks(8862));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                column: "HiredDate",
                value: new DateTime(2025, 1, 17, 2, 20, 33, 228, DateTimeKind.Local).AddTicks(8805));
        }
    }
}
