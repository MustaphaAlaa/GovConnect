using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class change_ApplicationStatusDataType_ChangeCreatedByEmp_To_UpdateByEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Employees_CreatedByEmployeeId",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "CreatedByEmployeeId",
                table: "Applications",
                newName: "UpdatedByEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_CreatedByEmployeeId",
                table: "Applications",
                newName: "IX_Applications_UpdatedByEmployeeId");

            migrationBuilder.AlterColumn<byte>(
                name: "ApplicationStatus",
                table: "Applications",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Employees_UpdatedByEmployeeId",
                table: "Applications",
                column: "UpdatedByEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Employees_UpdatedByEmployeeId",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "UpdatedByEmployeeId",
                table: "Applications",
                newName: "CreatedByEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_UpdatedByEmployeeId",
                table: "Applications",
                newName: "IX_Applications_CreatedByEmployeeId");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationStatus",
                table: "Applications",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Employees_CreatedByEmployeeId",
                table: "Applications",
                column: "CreatedByEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

        }
    }
}
