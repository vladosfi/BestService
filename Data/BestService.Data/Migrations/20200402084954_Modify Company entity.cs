using Microsoft.EntityFrameworkCore.Migrations;

namespace BestService.Data.Migrations
{
    public partial class ModifyCompanyentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "LogoImg",
                table: "Companies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoImg",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
