using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class inserting_Admins_and_Employees_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "CreatedAt", "IsEmployee", "UserId" },
                values: new object[,]
                {
                    { new Guid("123e4567-e89b-12d3-a456-426614174000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("550e8400-e29b-41d4-a716-446655440000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "HiredByAdmin", "HiredDate", "IsActive", "UserId" },
                values: new object[,]
                {
                    { new Guid("1b9d6bcd-bbfd-4b2d-9b5d-ab8dfbbd4bed"), new Guid("123e4567-e89b-12d3-a456-426614174000"), new DateTime(2025, 1, 17, 2, 4, 32, 17, DateTimeKind.Local).AddTicks(2121), true, new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("6ba7b810-9dad-11d1-80b4-00c04fd430c8"), new Guid("123e4567-e89b-12d3-a456-426614174000"), new DateTime(2025, 1, 17, 2, 4, 32, 17, DateTimeKind.Local).AddTicks(2118), true, new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), new Guid("550e8400-e29b-41d4-a716-446655440000"), new DateTime(2025, 1, 17, 2, 4, 32, 17, DateTimeKind.Local).AddTicks(2054), true, new Guid("11111111-1111-1111-1111-111111111111") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("1b9d6bcd-bbfd-4b2d-9b5d-ab8dfbbd4bed"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("6ba7b810-9dad-11d1-80b4-00c04fd430c8"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"));

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("123e4567-e89b-12d3-a456-426614174000"));

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440000"));
        }
    }
}
