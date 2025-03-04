﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class Add_ForigenKey_to_Applications_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ServicesPurposes_ServicePurposeId",
                table: "Applications",
                column: "ServicePurposeId",
                principalTable: "ServicesPurposes",
                principalColumn: "ServicePurposeId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ServicesPurposes_ServicePurposeId",
                table: "Applications");
        }
    }
}
