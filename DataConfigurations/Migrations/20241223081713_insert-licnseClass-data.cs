using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class insertlicnseClassdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LicenseClasses",
                columns: new[] { "LicenseClassId", "ClassDescription", "ClassName", "DefaultValidityLengthInMonths", "LicenseClassFees", "MinimumAllowedAge" },
                values: new object[,]
                {
                    { (short)1, "Permits non-professional drivers to operate private vehicles, tourist taxis, agricultural tractors for personal use, and light transport vehicles up to 2000 kg", "Private Driver License", 60, 120.00m, (byte)18 },
                    { (short)2, "For professional drivers to operate taxis and buses up to 17 passengers, in addition to all vehicles permitted under private license", "Third Class License", 60, 150.00m, (byte)21 },
                    { (short)3, "Permits operation of taxis, buses (17-26 passengers), transport vehicles, and heavy equipment. Requires 3 years experience with Third Class License", "Second Class License", 60, 180.00m, (byte)21 },
                    { (short)4, "Permits operation of all vehicle types. Requires 3 years experience with Second Class License", "First Class License", 60, 200.00m, (byte)21 },
                    { (short)5, "Permits operation of single tractors or those with agricultural trailers", "Agricultural Tractor License", 60, 100.00m, (byte)21 },
                    { (short)6, "Permits operation of metro trains and tram vehicles", "Metro/Tram License", 60, 150.00m, (byte)21 },
                    { (short)7, "Permits non-professional operation of motorcycles", "Private Motorcycle License", 60, 80.00m, (byte)18 },
                    { (short)10, "Permits operation of military vehicles, issued exclusively to armed forces personnel", "Military License", 60, 0.00m, (byte)21 },
                    { (short)11, "Permits operation of police vehicles, issued exclusively to police personnel", "Police License", 60, 0.00m, (byte)21 },
                    { (short)12, "Issued to individuals responsible for testing rapid transport vehicles", "Test Driving License", 12, 100.00m, (byte)21 },
                    { (short)13, "Temporary permit for individuals learning to drive vehicles", "Learner Permit", 3, 50.00m, (byte)18 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LicenseClasses",
                keyColumn: "LicenseClassId",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "LicenseClasses",
                keyColumn: "LicenseClassId",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "LicenseClasses",
                keyColumn: "LicenseClassId",
                keyValue: (short)3);

            migrationBuilder.DeleteData(
                table: "LicenseClasses",
                keyColumn: "LicenseClassId",
                keyValue: (short)4);

            migrationBuilder.DeleteData(
                table: "LicenseClasses",
                keyColumn: "LicenseClassId",
                keyValue: (short)5);

            migrationBuilder.DeleteData(
                table: "LicenseClasses",
                keyColumn: "LicenseClassId",
                keyValue: (short)6);

            migrationBuilder.DeleteData(
                table: "LicenseClasses",
                keyColumn: "LicenseClassId",
                keyValue: (short)7);

            migrationBuilder.DeleteData(
                table: "LicenseClasses",
                keyColumn: "LicenseClassId",
                keyValue: (short)10);

            migrationBuilder.DeleteData(
                table: "LicenseClasses",
                keyColumn: "LicenseClassId",
                keyValue: (short)11);

            migrationBuilder.DeleteData(
                table: "LicenseClasses",
                keyColumn: "LicenseClassId",
                keyValue: (short)12);

            migrationBuilder.DeleteData(
                table: "LicenseClasses",
                keyColumn: "LicenseClassId",
                keyValue: (short)13);
        }
    }
}
