using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class rename_CreatedByEmployee_to_CreatedByEmployeeId_In_Test_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Employees_CreatedByEmployee",
                table: "Tests");

            migrationBuilder.RenameColumn(
                name: "CreatedByEmployee",
                table: "Tests",
                newName: "CreatedByEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Tests_CreatedByEmployee",
                table: "Tests",
                newName: "IX_Tests_CreatedByEmployeeId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Employees_CreatedByEmployeeId",
                table: "Tests",
                column: "CreatedByEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Employees_CreatedByEmployeeId",
                table: "Tests");

            migrationBuilder.RenameColumn(
                name: "CreatedByEmployeeId",
                table: "Tests",
                newName: "CreatedByEmployee");

            migrationBuilder.RenameIndex(
                name: "IX_Tests_CreatedByEmployeeId",
                table: "Tests",
                newName: "IX_Tests_CreatedByEmployee");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("1b9d6bcd-bbfd-4b2d-9b5d-ab8dfbbd4bed"),
                column: "HiredDate",
                value: new DateTime(2025, 1, 17, 2, 4, 32, 17, DateTimeKind.Local).AddTicks(2121));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("6ba7b810-9dad-11d1-80b4-00c04fd430c8"),
                column: "HiredDate",
                value: new DateTime(2025, 1, 17, 2, 4, 32, 17, DateTimeKind.Local).AddTicks(2118));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                column: "HiredDate",
                value: new DateTime(2025, 1, 17, 2, 4, 32, 17, DateTimeKind.Local).AddTicks(2054));

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Employees_CreatedByEmployee",
                table: "Tests",
                column: "CreatedByEmployee",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
