using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class OnCreatingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "Countries",
                newName: "Id");

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CountryName" },
                values: new object[,]
                {
                    { 1, "Egypt" },
                    { 2, "Turkey" },
                    { 3, "Saudi Arabia" },
                    { 4, "Sudan" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "BirthDate", "ConcurrencyStamp", "CountryId", "Email", "EmailConfirmed", "FirstName", "Gender", "ImagePath", "LastName", "LockoutEnabled", "LockoutEnd", "NationalNo", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("6b5dce0f-f9bc-46bd-9f20-2f4426463238"), 0, "somewhere on th earth", new DateTime(1998, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "9cb3ed3f-bb13-44a5-8066-e15f5f7dc8b0", 2, "test@gmail.com", false, "Mostafa", 0, "unkown", "Alaa", false, null, "12345678910111213", null, null, null, "22222222222", false, null, false, "Mostafa Alaa" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "CreatedAt", "IsEmployee", "UserId" },
                values: new object[] { new Guid("5c358f65-9bbe-46bc-b572-e0c95de43c3f"), new DateTime(2024, 11, 15, 6, 49, 27, 490, DateTimeKind.Local).AddTicks(6104), true, new Guid("6b5dce0f-f9bc-46bd-9f20-2f4426463238") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("5c358f65-9bbe-46bc-b572-e0c95de43c3f"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6b5dce0f-f9bc-46bd-9f20-2f4426463238"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Countries",
                newName: "CountryId");
        }
    }
}
