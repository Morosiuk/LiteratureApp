using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class UpdateLiterature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Format",
                table: "Literature");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Literature");

            migrationBuilder.CreateIndex(
                name: "IX_Literature_Symbol",
                table: "Literature",
                column: "Symbol",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Literature_Symbol",
                table: "Literature");

            migrationBuilder.AddColumn<string>(
                name: "Format",
                table: "Literature",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Literature",
                type: "TEXT",
                nullable: true);
        }
    }
}
