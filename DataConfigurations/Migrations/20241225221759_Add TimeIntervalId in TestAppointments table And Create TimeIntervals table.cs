using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeIntervalIdinTestAppointmentstableAndCreateTimeIntervalstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AppointmentDate",
                table: "TestAppointments",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "TimeIntervalId",
                table: "TestAppointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TimeInterval",
                columns: table => new
                {
                    TimeIntervalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hour = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeInterval", x => x.TimeIntervalId);
                });

            migrationBuilder.InsertData(
                table: "TimeInterval",
                columns: new[] { "TimeIntervalId", "Hour", "Minute" },
                values: new object[,]
                {
                    { 1, 9, 0 },
                    { 2, 9, 15 },
                    { 3, 9, 30 },
                    { 4, 9, 45 },
                    { 5, 10, 0 },
                    { 6, 10, 15 },
                    { 7, 10, 30 },
                    { 8, 10, 45 },
                    { 9, 11, 0 },
                    { 10, 11, 15 },
                    { 11, 11, 30 },
                    { 12, 11, 45 },
                    { 13, 12, 0 },
                    { 14, 12, 15 },
                    { 15, 12, 30 },
                    { 16, 12, 45 },
                    { 17, 13, 0 },
                    { 18, 13, 15 },
                    { 19, 13, 30 },
                    { 20, 13, 45 },
                    { 21, 14, 0 },
                    { 22, 14, 15 },
                    { 23, 14, 30 },
                    { 24, 14, 45 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestAppointments_TimeIntervalId",
                table: "TestAppointments",
                column: "TimeIntervalId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestAppointments_TimeInterval_TimeIntervalId",
                table: "TestAppointments",
                column: "TimeIntervalId",
                principalTable: "TimeInterval",
                principalColumn: "TimeIntervalId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestAppointments_TimeInterval_TimeIntervalId",
                table: "TestAppointments");

            migrationBuilder.DropTable(
                name: "TimeInterval");

            migrationBuilder.DropIndex(
                name: "IX_TestAppointments_TimeIntervalId",
                table: "TestAppointments");

            migrationBuilder.DropColumn(
                name: "TimeIntervalId",
                table: "TestAppointments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AppointmentDate",
                table: "TestAppointments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");
        }
    }
}
