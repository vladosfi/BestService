using Microsoft.EntityFrameworkCore.Migrations;

namespace BestService.Data.Migrations
{
    public partial class RemoveLinkFromCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "LogoImage",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoImage",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
