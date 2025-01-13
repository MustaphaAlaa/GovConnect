using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class make_retakeTestAppliationId_is_NUllABLE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Applicataions_RetakeTestApplicationId",
                table: "Bookings");

            migrationBuilder.AlterColumn<int>(
                name: "RetakeTestApplicationId",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Applicataions_RetakeTestApplicationId",
                table: "Bookings",
                column: "RetakeTestApplicationId",
                principalTable: "Applications",
                principalColumn: "ApplicationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Applicataions_RetakeTestApplicationId",
                table: "Bookings");

            migrationBuilder.AlterColumn<int>(
                name: "RetakeTestApplicationId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Applicataions_RetakeTestApplicationId",
                table: "Bookings",
                column: "RetakeTestApplicationId",
                principalTable: "Applications",
                principalColumn: "ApplicationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
