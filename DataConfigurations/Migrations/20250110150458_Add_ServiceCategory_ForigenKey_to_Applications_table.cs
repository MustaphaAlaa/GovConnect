using Microsoft.EntityFrameworkCore.Migrations;

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
                name: "IX_Applicataions_ServiceCategoryId",
                table: "Applicataions",
                column: "ServiceCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicataions_ServiceCategories_ServiceCategoryId",
                table: "Applicataions",
                column: "ServiceCategoryId",
                principalTable: "ServiceCategories",
                principalColumn: "ServiceCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicataions_ServiceCategories_ServiceCategoryId",
                table: "Applicataions");

            migrationBuilder.DropIndex(
                name: "IX_Applicataions_ServiceCategoryId",
                table: "Applicataions");
        }
    }
}
