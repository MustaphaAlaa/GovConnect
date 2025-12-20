using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("87193e7e-f724-4215-ba54-684007edb662"), "fd89f305-bf4e-45a5-9175-7776551bb622", "User", "USER" },
                    { new Guid("9e29e4b0-0e04-4aff-8631-3d6109b8887b"), "def651c0-cff2-4580-8403-6117a93b7927", "Employee", "EMPLOYEE" },
                    { new Guid("e53bc640-2c03-4d58-8aff-b5799fcca6d4"), "b320e59d-4c38-4119-999b-cd9611e69c4e", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("87193e7e-f724-4215-ba54-684007edb662"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e29e4b0-0e04-4aff-8631-3d6109b8887b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e53bc640-2c03-4d58-8aff-b5799fcca6d4"));
        }
    }
}
