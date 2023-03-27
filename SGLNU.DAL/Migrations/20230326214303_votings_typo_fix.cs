using Microsoft.EntityFrameworkCore.Migrations;

namespace SGLNU.DAL.Migrations
{
    public partial class votings_typo_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tilte",
                table: "Votings",
                newName: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Votings",
                newName: "Tilte");
        }
    }
}
