using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class Create_LDLApplicationsAllowedToRetakATest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LDLApplicationsAllowedToRetakeATests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocalDrivingApplicationId = table.Column<int>(type: "int", nullable: false),
                    TestTypeId = table.Column<int>(type: "int", nullable: false),
                    IsAllowedToRetakeATest = table.Column<bool>(type: "bit", nullable: false),
                    LocalDrivingLicenseApplicationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LDLApplicationsAllowedToRetakeATests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LDLApplicationsAllowedToRetakeATests_LocalDrivingLicenseApplications_LocalDrivingLicenseApplicationId",
                        column: x => x.LocalDrivingLicenseApplicationId,
                        principalTable: "LocalDrivingLicenseApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LDLApplicationsAllowedToRetakeATests_TestTypes_TestTypeId",
                        column: x => x.TestTypeId,
                        principalTable: "TestTypes",
                        principalColumn: "TestTypeId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LDLApplicationsAllowedToRetakeATests_LocalDrivingLicenseApplicationId",
                table: "LDLApplicationsAllowedToRetakeATests",
                column: "LocalDrivingLicenseApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_LDLApplicationsAllowedToRetakeATests_TestTypeId",
                table: "LDLApplicationsAllowedToRetakeATests",
                column: "TestTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LDLApplicationsAllowedToRetakeATests");
        }
    }
}
