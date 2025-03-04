﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class Add_ServiceCategory_ForigenKey_to_Applications_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Applications_ServiceCategoryId",
                table: "Applications",
                column: "ServiceCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ServiceCategories_ServiceCategoryId",
                table: "Applications",
                column: "ServiceCategoryId",
                principalTable: "ServiceCategories",
                principalColumn: "ServiceCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ServiceCategories_ServiceCategoryId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_ServiceCategoryId",
                table: "Applications");
        }
    }
}
