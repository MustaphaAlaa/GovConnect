using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class InsertTestTypesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TestTypes",
                columns: new[] { "TestTypeId", "TestTypeDescription", "TestTypeFees", "TestTypeTitle" },
                values: new object[,]
                {
                    { 1, "This assesses the applicant's visual acuity to ensure they have sufficient vision to drive safely.", 100m, "Vision" },
                    { 2, "This test assesses the applicant's knowledge of traffic rules, road signs, and driving regulations. It typically consists of multiple-choice questions, and the applicant must select the correct answer(s). The written test aims to ensure that the applicant understands the rules of the road and can apply them in various driving scenarios.", 150m, "Written Therory" },
                    { 3, "This test evaluates the applicant's driving skills and ability to operate a motor vehicle safely on public roads. A licensed examiner accompanies the applicant in the vehicle and observes their driving performance.", 250m, "Practical Street" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TestTypes",
                keyColumn: "TestTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TestTypes",
                keyColumn: "TestTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TestTypes",
                keyColumn: "TestTypeId",
                keyValue: 3);
        }
    }
}
