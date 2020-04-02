using Microsoft.EntityFrameworkCore.Migrations;

namespace BestService.Data.Migrations
{
    public partial class ModifyCompanyproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoImg",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "LogoImage",
                table: "Companies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoImage",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "LogoImg",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
