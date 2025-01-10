using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class Create_RetakeTestApplication_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RetakeTestApplications",
                columns: table => new
                {
                    RetakeTestApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestTypeId = table.Column<int>(type: "int", nullable: false),
                    LocalDrivingLicenseApplicationId = table.Column<int>(type: "int", nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetakeTestApplications", x => x.RetakeTestApplicationId);
                    table.ForeignKey(
                        name: "FK_RetakeTestApplications_Applicataions_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applicataions",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RetakeTestApplications_LocalDrivingLicenseApplications_LocalDrivingLicenseApplicationId",
                        column: x => x.LocalDrivingLicenseApplicationId,
                        principalTable: "LocalDrivingLicenseApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RetakeTestApplications_TestTypes_TestTypeId",
                        column: x => x.TestTypeId,
                        principalTable: "TestTypes",
                        principalColumn: "TestTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RetakeTestApplications_ApplicationId",
                table: "RetakeTestApplications",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_RetakeTestApplications_LocalDrivingLicenseApplicationId",
                table: "RetakeTestApplications",
                column: "LocalDrivingLicenseApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_RetakeTestApplications_TestTypeId",
                table: "RetakeTestApplications",
                column: "TestTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RetakeTestApplications");
        }
    }
}
