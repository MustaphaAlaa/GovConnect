using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class InsertuserFakeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "BirthDate", "ConcurrencyStamp", "CountryId", "Email", "EmailConfirmed", "FirstName", "FourthName", "Gender", "ImagePath", "LockoutEnabled", "LockoutEnd", "NationalNo", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecondName", "SecurityStamp", "ThirdName", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 0, "Test Address 1", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "b3e27cb1-7bfb-4722-bc67-0c9c96c51d4e", 1, "test1@test.com", true, "Test", "First", 0, "null", true, null, "1111111111", "TEST1@TEST.COM", "TESTUSER1", "AQAAAAIAAYagAAAAEPahXcemRXGRxKE3LxhXh/jtLc5gQV31AVLNkX2GAh0xArP6aYtL2TFmDaxZHUXKQw==", "0777777771", true, "User", "K2MDKSUEXFG6QCHLCWJLVREVWT7545X2", "One", false, "testuser1" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 0, "Test Address 2", new DateTime(2000, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "44f69173-726f-44d8-b4b0-7695e4bec210", 1, "test2@test.com", true, "Test", "Second", 1, "null", true, null, "2222222222", "TEST2@TEST.COM", "TESTUSER2", "AQAAAAIAAYagAAAAEPahXcemRXGRxKE3LxhXh/jtLc5gQV31AVLNkX2GAh0xArP6aYtL2TFmDaxZHUXKQw==", "0777777772", true, "User", "K2MDKSUEXFG6QCHLCWJLVREVWT7545X3", "Two", false, "testuser2" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 0, "Test Address 3", new DateTime(2000, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "2f511f41-c3a1-4ebe-a55b-377487fac8b8", 1, "test3@test.com", true, "Test", "Third", 0, "null", true, null, "3333333333", "TEST3@TEST.COM", "TESTUSER3", "AQAAAAIAAYagAAAAEPahXcemRXGRxKE3LxhXh/jtLc5gQV31AVLNkX2GAh0xArP6aYtL2TFmDaxZHUXKQw==", "0777777773", true, "User", "K2MDKSUEXFG6QCHLCWJLVREVWT7545X4", "Three", false, "testuser3" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), 0, "Test Address 4", new DateTime(2000, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "a98af5d4-731c-489b-851c-86f23c66b3d1", 1, "test4@test.com", true, "Test", "Fourth", 1, "null", true, null, "4444444444", "TEST4@TEST.COM", "TESTUSER4", "AQAAAAIAAYagAAAAEPahXcemRXGRxKE3LxhXh/jtLc5gQV31AVLNkX2GAh0xArP6aYtL2TFmDaxZHUXKQw==", "0777777774", true, "User", "K2MDKSUEXFG6QCHLCWJLVREVWT7545X5", "Four", false, "testuser4" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), 0, "Test Address 5", new DateTime(2000, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "7c8f2e9b-4c1a-483d-b832-d35cb3f3f287", 1, "test5@test.com", true, "Test", "Fifth", 0, "null", true, null, "5555555555", "TEST5@TEST.COM", "TESTUSER5", "AQAAAAIAAYagAAAAEPahXcemRXGRxKE3LxhXh/jtLc5gQV31AVLNkX2GAh0xArP6aYtL2TFmDaxZHUXKQw==", "0777777775", true, "User", "K2MDKSUEXFG6QCHLCWJLVREVWT7545X6", "Five", false, "testuser5" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));
        }
    }
}
