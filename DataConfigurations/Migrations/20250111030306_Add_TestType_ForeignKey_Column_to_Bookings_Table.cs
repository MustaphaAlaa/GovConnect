using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class Add_TestType_ForeignKey_Column_to_Bookings_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestTypeId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TestTypeId",
                table: "Bookings",
                column: "TestTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_TestTypes_TestTypeId",
                table: "Bookings",
                column: "TestTypeId",
                principalTable: "TestTypes",
                principalColumn: "TestTypeId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_TestTypes_TestTypeId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_TestTypeId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "TestTypeId",
                table: "Bookings");
        }
    }
}
