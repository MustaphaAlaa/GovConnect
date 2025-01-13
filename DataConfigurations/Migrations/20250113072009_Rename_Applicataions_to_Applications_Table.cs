using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class Rename_Applicataions_to_Applications_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                      name: "Applicataions",
                    newName: "Applications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                         name: "Applications",
                         newName: "Applicataions");
        }
    }
}

